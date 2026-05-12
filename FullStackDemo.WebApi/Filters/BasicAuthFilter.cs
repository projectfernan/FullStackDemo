using FullStackDemo.ApplicationService.Commands.Interfaces.Authentication;
using FullStackDemo.ApplicationService.Commands.Interfaces.IAuthentication;
using FullStackDemo.ApplicationService.DTOs.Authentication;
using FullStackDemo.Common.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace FullStackDemo.WebApi.Filters
{
    public class BasicAuthFilter : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IBasicAuthCommandHandler _basicAuthCommandHandler;
        public BasicAuthFilter(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IBasicAuthCommandHandler basicAuthCommandHandler)
        : base(options, logger, encoder, clock)
        {
            _basicAuthCommandHandler = basicAuthCommandHandler;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialsBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialsBytes).Split(':', 2);
                var username = credentials[0];
                var password = HashingHelper.Hash(credentials[1]);

                BasicAuthCommand dto = new BasicAuthCommand();
                dto.Username = username;

                var creds = await _basicAuthCommandHandler.HandleGetUserBasicAuthByUsername(dto);

                if (creds.Password != password)
                {
                    var failureMessage = "Authentication failed. The username or password provided is incorrect.";
                    Context.Items["AuthenticationFailureMessage"] = failureMessage; // Store failure message in context
                    return AuthenticateResult.Fail(failureMessage);
                }

                var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Name, username),
            };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch (Exception ex)
            {
                var failureMessage = new
                {
                    Message = ex.Message
                };
                return AuthenticateResult.Fail(failureMessage.Message);
            }
        }
    }
}

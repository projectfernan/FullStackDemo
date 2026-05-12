using FullStackDemo.ApplicationService.DTOs.Authentication;
using FullStackDemo.ApplicationService.Queries.Interfaces.IAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.ApplicationService.Queries.QueryHandlers.Authentication
{
    public class GetJwtTokenHandler : IGetJwtTokenHandler
    {
        private readonly IConfiguration _configuration;

        public GetJwtTokenHandler(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public JwtTokenDto GenerateJwtToken()
        {
            JwtTokenDto jwt = new JwtTokenDto();

            // Retrieve JWT settings from the configuration
            var jwtSettings = _configuration.GetSection("Jwt");
            // Convert the secret key from the configuration to a byte array
            var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);

            // Define the token descriptor with the necessary claims and settings
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Specify the claims that will be included in the token
                Subject = new ClaimsIdentity(new[] 
                    { 
                        new Claim("id", "123"),// Add more claims as needed
                         // Example: new Claim("role", "admin"),
                    }),

                // Set the token expiration time to one hour from now
                Expires = DateTime.UtcNow.AddHours(1),

                // Define the signing credentials using the secret key and HMAC SHA256 algorithm
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

                // Set the issuer and audience from the configuration
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            // Create a token handler to generate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            // Generate the token using the token descriptor
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Write the token to a string and assign it to the JwtTokenDto
            jwt.Token = tokenHandler.WriteToken(token);

            // Return the generated JWT token
            return jwt;
        }
    }
}

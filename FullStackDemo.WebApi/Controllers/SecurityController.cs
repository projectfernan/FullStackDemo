using FullStackDemo.ApplicationService.DTOs.Authentication;
using FullStackDemo.ApplicationService.DTOs.Common;
using FullStackDemo.ApplicationService.Queries.Interfaces.IAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullStackDemo.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Basic")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IGetJwtTokenHandler _jwtTokenHandler;
        public SecurityController(IGetJwtTokenHandler getJwtTokenHandler) { 
            _jwtTokenHandler = getJwtTokenHandler;
        }

        [HttpPost("[action]")]
        public IActionResult GetJwtToken() 
        { 
            ApiBodyDto result = new ApiBodyDto();
            JwtTokenDto token = new JwtTokenDto();

            token = _jwtTokenHandler.GenerateJwtToken();
            result.TotalCount = 1;
            result.Data = token;

            return Ok(result);
        }
    }
}

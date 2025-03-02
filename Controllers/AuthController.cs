using BirFatura.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirFatura.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [HttpGet("gettoken")]
        public async Task<IActionResult> GetToken()
        {
            try
            {
                var tokenResponse = await _tokenService.GetTokenAsync();
                return Ok(tokenResponse);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
         }
    }
}

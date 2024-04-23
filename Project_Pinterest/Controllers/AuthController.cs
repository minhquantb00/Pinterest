using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using Project_Pinterest.Payloads.DataRequests.TokenRequests;
using Project_Pinterest.Payloads.DataRequests.UserRequests;
using Project_Pinterest.Payloads.DataResponses.DataToken;
using Project_Pinterest.Payloads.DataResponses.DataUser;
using Project_Pinterest.Payloads.Responses;
using Project_Pinterest.Services.Interfaces;

namespace Project_Pinterest.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(Request_Register request)
        {
            var result = await _authService.Register(request);
            switch (result.Status)
            {
                case 200: 
                    return Ok(result);
                case 404: 
                    return NotFound(result);
                case 400: 
                    return BadRequest(result);
                case 403: 
                    return Unauthorized(result);
                default:
                    return StatusCode(500, result);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(Request_Login request)
        {
            var result = await _authService.Login(request);
            switch (result.Status)
            {
                case 200:
                    return Ok(result);
                case 404:
                    return NotFound(result);
                case 400:
                    return BadRequest(result);
                case 403:
                    return Unauthorized(result);
                default:
                    return StatusCode(500, result);
            }
        }
        [HttpPost("RenewAccessToken")]
        public IActionResult RenewAccessToken(Request_Token request)
        {
            var result = _authService.RenewAccessToken(request);
            switch (result.Status)
            {
                case 200:
                    return Ok(result);
                case 404:
                    return NotFound(result);
                case 400:
                    return BadRequest(result);
                case 403:
                    return Unauthorized(result);
                default:
                    return StatusCode(500, result);
            }
        }
        [HttpPost("ConfirmCreateNewPassword")]
        public async Task<IActionResult> ConfirmCreateNewPassword(Request_ConfirmNewPassword request)
        {
            var result = await _authService.ConfirmCreateNewPassword(request);
            switch (result.Status)
            {
                case 200:
                    return Ok(result);
                case 404:
                    return NotFound(result);
                case 400:
                    return BadRequest(result);
                case 403:
                    return Unauthorized(result);
                default:
                    return StatusCode(500, result);
            }
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(Request_ForgotPassword request)
        {
            var result = await _authService.ForgotPassword(request);
            switch (result.Status)
            {
                case 200:
                    return Ok(result);
                case 404:
                    return NotFound(result);
                case 400:
                    return BadRequest(result);
                case 403:
                    return Unauthorized(result);
                default:
                    return StatusCode(500, result);
            }
        }
        [HttpPost("ConfirmCreateNewAccount")]
        public async Task<IActionResult> ConfirmCreateNewAccount(Request_ConfirmCreateNewAccount request)
        {
            var result = await _authService.ConfirmCreateNewAccount(request);
            switch (result.Status)
            {
                case 200:
                    return Ok(result);
                case 404:
                    return NotFound(result);
                case 400:
                    return BadRequest(result);
                case 403:
                    return Unauthorized(result);
                default:
                    return StatusCode(500, result);
            }
        }
        [HttpPut("ChangePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword(Request_ChangePassword request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _authService.ChangePassword(id, request);
            switch (result.Status)
            {
                case 200:
                    return Ok(result);
                case 404:
                    return NotFound(result);
                case 400:
                    return BadRequest(result);
                case 403:
                    return Unauthorized(result);
                default:
                    return StatusCode(500, result);
            }
        }
    }
}

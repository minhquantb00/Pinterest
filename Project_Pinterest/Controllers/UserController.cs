using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using Project_Pinterest.Handler.HandlePagination;
using Project_Pinterest.Payloads.DataRequests.UserRequests;
using Project_Pinterest.Payloads.DataResponses.DataUser;
using Project_Pinterest.Payloads.Responses;
using Project_Pinterest.Services.Implements;
using Project_Pinterest.Services.Interfaces;

namespace Project_Pinterest.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPut("DeleteUser/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            var result = await _userService.DeleteUser(userId);
            switch(result)
            {
                case "Người dùng không tồn tại": return NotFound(result);
                case "Xóa thông tin người dùng thành công": return Ok(result);
                default: return StatusCode(500, result);
            }
        }
        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(await _userService.GetAllUsers(pageSize, pageNumber));
        }
        [HttpGet("GetUserByName")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserByName(string? name, int pageSize = 10, int pageNumber = 1)
        {
            return Ok(await _userService.GetUserByName(name, pageSize, pageNumber));
        }
        [HttpPut("UpdateUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Consumes(contentType: "multipart/form-data")]
        public async Task<IActionResult> UpdateUser([FromForm] Request_UpdateUserInfor request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _userService.UpdateUser(id, request);
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
        [HttpPut("LockAccount/{userLockedId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> LockAccount([FromRoute] int userLockedId)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _userService.LockAccount(id, userLockedId);
            switch (result)
            {
                case "Người dùng không tồn tại": return NotFound(result);
                case "Người dùng không được xác thực hoặc không được xác định": return Unauthorized(result);
                case "Tài khoản này đã bị khóa": return BadRequest(result);
                case "Khóa tài khoản người dùng thành công": return Ok(result);
                default: return StatusCode(500, result);
            }
        }

        [HttpPut("UnLockAccount/{userLUnockedId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UnLockAccount([FromRoute] int userLUnockedId)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _userService.LockAccount(id, userLUnockedId);
            switch (result)
            {
                case "Người dùng không tồn tại": return NotFound(result);
                case "Người dùng không được xác thực hoặc không được xác định": return Unauthorized(result);
                case "Tài khoản này chưa bị khóa": return BadRequest(result);
                case "Mở khóa tài khoản người dùng thành công": return Ok(result);
                default: return StatusCode(500, result);
            }
        }
        [HttpPut("ChangeDecentralization")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeDecentralization(Request_ChangeDecentralization request)
        {
            var result = await _userService.ChangeDecentralization(request);
            switch (result)
            {
                case "Không tìm thấy id người dùng": return NotFound(result);
                case "Người dùng không được xác thực hoặc không được xác định": return Unauthorized(result);
                case "Thay đổi quyền người dùng thành công": return Ok(result);
                default: return StatusCode(500, result);
            }
        }
        [HttpGet("GetUserInformation/{userId}")]
        public async Task<IActionResult> GetUserInformation(int userId)
        {
            return Ok(await _userService.GetUserInformation(userId));
        }
        [HttpGet("GetUserByRole/{roleId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserByRole(int roleId, int pageSize = 10, int pageNumber = 1)
        {
            return Ok(await _userService.GetUserByRole(roleId, pageSize, pageNumber));
        }
        [HttpGet("GetUserById/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            return Ok(await _userService.GetUserById(userId));
        }
    }
}

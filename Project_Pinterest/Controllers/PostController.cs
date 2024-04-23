using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using Project_Pinterest.Handler.HandlePagination;
using Project_Pinterest.Payloads.DataRequests.PostRequests;
using Project_Pinterest.Payloads.DataResponses.DataPost;
using Project_Pinterest.Payloads.Responses;
using Project_Pinterest.Services.Implements;
using Project_Pinterest.Services.Interfaces;
using System.ComponentModel.Design;

namespace Project_Pinterest.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpPost("CreatePost")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Consumes(contentType: "multipart/form-data")]
        public async Task<IActionResult> CreatePost([FromForm] Request_CreatePost request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _postService.CreatePost(id, request);
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
        [HttpPut("DeletePost/{postId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePost([FromRoute] int postId)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _postService.DeletePost(id, postId);
            switch (result)
            {
                case "Bài đăng không tồn tại": return NotFound(result);
                case "Xóa thành công!!!!!!": return Ok(result);
                default: return StatusCode(500, result);
            }
        }
        [HttpGet("GetAllPost")]
        public async Task<IActionResult> GetAllPost(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(await _postService.GetAllPost(pageSize, pageNumber));
        }
        [HttpGet("GetPostByUser/{userId}")]
        public async Task<IActionResult> GetPostByUser([FromRoute] int userId, int pageSize = 10, int pageNumber = 1)
        {
            return Ok(await _postService.GetPostByUser(userId, pageSize, pageNumber));
        }
        [HttpPut("UpdatePost")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Consumes(contentType: "multipart/form-data")]
        public async Task<IActionResult> UpdatePost([FromForm] Request_UpdatePost request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _postService.UpdatePost(id, request);
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
        [HttpPost("CreateComment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateComment(Request_CreateComment request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _postService.CreateComment(id, request);
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
        [HttpPut("DeleteComment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteComment(Request_DeleteComment request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _postService.DeleteComment(id, request);
            switch (result)
            {
                case "Không tìm thấy bài viết": return NotFound(result);
                case "Không tìm thấy bình luận này": return NotFound(result);
                case "Xóa bình luận thành công": return Ok(result);
                default: return StatusCode(500, result);
            }
        }
        [HttpGet("GetCommentByUser/{userId}")]
        public async Task<IActionResult> GetCommentByUser([FromRoute] int userId, int pageSize = 10, int pageNumber = 1)
        {
            return Ok(await _postService.GetCommentByUser(userId, pageSize, pageNumber));
        }
        [HttpGet("GetPostById/{postId}")]
        public async Task<IActionResult> GetPostById([FromRoute] int postId)
        {
            return Ok(await _postService.GetPostById(postId));
        }
        [HttpPost("LikePost")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> LikePost(Request_UserLikePost request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _postService.LikePost(id, request);
            switch (result)
            {
                case "Không tìm thấy bài viết": return NotFound(result);
                case "Đã like bài viết": return Ok(result);
                default: return StatusCode(500, result);
            }
        }
        [HttpPut("UpdateComment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateComment(int commentId,Request_UpdateComment request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _postService.UpdateComment(commentId, id, request);
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
        [HttpPost("UserLikeComment/{commentId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UserLikeComment([FromRoute] int commentId)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _postService.UserLikeComment(id, commentId);
            switch (result)
            {
                case "Không tìm thấy bài viết": return NotFound(result);
                case "Like bình luận thành công": return Ok(result);
                default: return StatusCode(500, result);
            }
        }
        [HttpPost("SharePost/{postId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SharePost([FromRoute] int postId)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _postService.SharePost(id, postId);
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
        [HttpPost("DownloadImageForPost/{postId}")]
        public async Task<IActionResult> DownloadImageForPost([FromRoute] int postId, string saveDirectory)
        {
            return Ok(await _postService.DownloadImageForPost(postId, saveDirectory));
        }
        [HttpGet("GetCommentByPost/{postId}")]
        public async Task<IActionResult> GetCommentByPost([FromRoute] int postId, int pageSize = 10, int pageNumber = 1)
        {
            return Ok(await _postService.GetCommentByPost(postId, pageSize, pageNumber));
        }
        [HttpGet("GetPostByTitle")]
        public async Task<IActionResult> GetPostByTitle(string title, int pageSize = 10, int pageNumber = 1)
        {
            return Ok(await _postService.GetPostByTitle(title, pageSize, pageNumber));
        }
    }
}

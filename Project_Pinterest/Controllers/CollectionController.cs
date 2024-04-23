using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Pinterest.Handler.HandlePagination;
using Project_Pinterest.Payloads.DataRequests.CollectionRequests;
using Project_Pinterest.Payloads.DataResponses.DataCollection;
using Project_Pinterest.Payloads.DataResponses.DataPostCollection;
using Project_Pinterest.Payloads.Responses;
using Project_Pinterest.Services.Implements;
using Project_Pinterest.Services.Interfaces;

namespace Project_Pinterest.Controllers
{
    [Route("api/collection")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _collectionService;
        private readonly IPostCollectionService _postCollectionService;
        public CollectionController(ICollectionService collectionService, IPostCollectionService postCollectionService)
        {
            _collectionService = collectionService;
            _postCollectionService = postCollectionService;
        }
        [HttpPost("CreateCollection")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateCollection(Request_CreateCollection request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _collectionService.CreateCollection(id, request);
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
        [HttpDelete("DeleteCollection/{collectionId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteCollection( [FromRoute] int collectionId)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _collectionService.DeleteCollection(id, collectionId);
            switch (result)
            {
                case "Bộ sưu tập không tồn tại": return NotFound(result);
                case "Xóa bộ sưu tập thành công": return Ok(result);
                default: return StatusCode(500, result);
            }
        }
        [HttpGet("GetAllCollections")]
        public async Task<IActionResult> GetAllCollections(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(await _collectionService.GetAllCollections(pageSize, pageNumber));
        }
        [HttpGet("GetCollectionById/{collectionId}")]
        public async Task<IActionResult> GetCollectionById([FromRoute] int collectionId)
        {
            return Ok(await _collectionService.GetCollectionById(collectionId));
        }
        [HttpGet("GetCollectionByName")]
        public async Task<IActionResult> GetCollectionByName(string name, int pageSize = 10, int pageNumber = 1)
        {
            return Ok(await _collectionService.GetCollectionByName(name, pageSize, pageNumber));
        }
        [HttpPut("UpdateCollection")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateCollection(Request_UpdateCollection request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _collectionService.UpdateCollection(id, request);
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
        [HttpPost("CreateListPostCollection/{collectionId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateListPostCollection([FromRoute] int collectionId, List<Request_CreatePostCollection> requests)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _postCollectionService.CreateListPostCollection(id,collectionId, requests));
        }
        [HttpPost("CreatePostCollection")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreatePostCollection(int collectionId, Request_CreatePostCollection request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _postCollectionService.CreatePostCollection(id, collectionId, request);
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

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Pinterest.Entities;
using Project_Pinterest.Handler.HandlePagination;
using Project_Pinterest.Payloads.DataRequests.ReportRequests;
using Project_Pinterest.Payloads.DataResponses.DataReport;
using Project_Pinterest.Payloads.Responses;
using Project_Pinterest.Services.Implements;
using Project_Pinterest.Services.Interfaces;

namespace Project_Pinterest.Controllers
{
    [Route("api/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpPost("CreateReport")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateReport(Request_CreateReport request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _reportService.CreateReport(id, request);
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
        [HttpPost("CreateReportUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateReportUser(Request_CreateReportUser request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var result = await _reportService.CreateReportUser(id, request);
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
        [HttpGet("GetAllReportByPost/{postId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllReportByPost([FromRoute] int postId, int pageSize = 10, int pageNumber = 1)
        {
            return Ok(await _reportService.GetAllReportByPost(postId, pageSize, pageNumber));
        }

        [HttpGet("GetAllReports")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllReports(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(await _reportService.GetAllReports(pageSize, pageNumber));
        }
    }
}

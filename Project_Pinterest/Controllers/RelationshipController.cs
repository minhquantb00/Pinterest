using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Pinterest.Services.Interfaces;

namespace Project_Pinterest.Controllers
{
    [Route("api/relationship")]
    [ApiController]
    public class RelationshipController : ControllerBase
    {
        private readonly IRelationshipService _relationshipService;
        public RelationshipController(IRelationshipService relationshipService)
        {
            _relationshipService = relationshipService;
        }
        [HttpPost]
        [Route("Follow/{partnerId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Follow([FromRoute] int partnerId, string action)
        {
            int ownerId = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _relationshipService.Follow(partnerId, ownerId, action));
        }
    }
}

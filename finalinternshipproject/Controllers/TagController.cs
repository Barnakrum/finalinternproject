using finalinternshipproject.Dtos.Tag;
using finalinternshipproject.Services.Tag;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace finalinternshipproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService tagService;

        public TagController(ITagService tagService)
        {
            this.tagService = tagService;
        }
        [HttpGet("all")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetAllTags()
        {
            var response = await tagService.GetAllTags();
            return response;
        }
        [HttpGet("cloud")]
        public async Task<ActionResult<ServiceResponse<List<TagForCloudDto>>>> GetTagsForCloud()
        {
            var response = await tagService.GetTagsForCloud();
            return response;
        }
    }
}

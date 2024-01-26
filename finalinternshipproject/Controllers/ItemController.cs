using finalinternshipproject.Dtos.Item;
using finalinternshipproject.Services.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace finalinternshipproject.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService itemService;

        public ItemController(IItemService itemService)
        {
            this.itemService = itemService;
        }
        [HttpPost("post/{id:int}")]
        public async Task<ActionResult<ServiceResponse<int>>> PostItem(ItemPostDto request, int id)
        {
            var response = await itemService.PostItem(request, id);
            return Ok(response);
        }
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<ServiceResponse<int>>> DeleteItem(int id)
        {
            var response = await itemService.DeleteItem(id);
            return Ok(response);
        }
        [HttpPut("edit/{id:int}")]
        public async Task<ActionResult<ServiceResponse<int>>> EditItem(ItemEditDto request,int id)
        {
            var response = await itemService.EditItem(request, id);
            return Ok(response);
        }
        [HttpGet("manage/{id:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> CanUserManageItem(int id)
        {
            var response = await itemService.CanUserManageItem(id);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet("get/{id:int}")]
        public async Task<ActionResult<ServiceResponse<ItemGetDto>>> GetItemById(int id)
        {
            var response = await itemService.GetItemById(id);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet("get/collection/{id:int}")]
        public async Task<ActionResult<ServiceResponse<List<ItemGetDto>>>> GetItemByCollectionId(int id)
        {
            var response = await itemService.GetItemByCollectionId(id);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet("latest/{ammount:int}")]
        public async Task<ActionResult<ServiceResponse<List<ItemLatestDto>>>> GetLatest(int ammount)
        {
            var response = await itemService.GetLatest(ammount);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet("list/{id:int}")]
        public async Task<ActionResult<ServiceResponse<List<int>>>> GetCollectionItems(int id)
        {
            var response = await itemService.CollectionItems(id);
            return Ok(response);
        }
    }
}

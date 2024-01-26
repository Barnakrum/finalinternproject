using finalinternshipproject.Dtos.Collection;
using finalinternshipproject.Services.Collection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace finalinternshipproject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService collectionService;
        public CollectionController(ICollectionService collectionService)
        {
            this.collectionService = collectionService;
        }
        [HttpPost("post")]
        public async Task<ActionResult<ServiceResponse<string>>> PostCollection(CollectionPostDto request)
        {
            var response = await collectionService.PostCollection(request);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet("getAll")]
        public async Task<ActionResult<ServiceResponse<List<CollectionGetDto>>>> GetAllCollections()
        {
            var response = await collectionService.GetAllCollections();
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet("get/{id:int}")]
        public async Task<ActionResult<ServiceResponse<CollectionGetDto>>> GetById(int id)
        {
            var response = await collectionService.GetCollectionById(id);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet("user/{id:int}")]
        public async Task<ActionResult<ServiceResponse<CollectionGetDto>>> GetByUserId(int id)
        {
            var response = await collectionService.GetCollectionsByUserId(id);
            return Ok(response);
        }
        [HttpGet("manage/{id:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> CheckIfUserCanManageCollection(int id)
        {
            var response = await collectionService.CheckIfUserCanManageCollection(id);
            return Ok(response);
        }
        [HttpGet("add/{id:int}")]
        public async Task<ActionResult<ServiceResponse<CollectionAddItemDto>>> GetCollectionFields(int id)
        {
            var response = await collectionService.GetCollectionFields(id);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet("anonadd/{id:int}")]
        public async Task<ActionResult<ServiceResponse<CollectionAddItemDto>>> GetCollectionFieldsAnonymous(int id)
        {
            var response = await collectionService.GetCollectionFieldsAnonymous(id);
            return Ok(response);
        }
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteCollection(int id)
        {
            var response = await collectionService.DeleteCollection(id);
            return Ok(response);
        }
        [HttpPut("edit/{id:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> EditCollection(CollectionEditDto request,int id)
        {
            var response = await collectionService.EditCollection(request, id);
            return Ok(response);
        }
    }
}

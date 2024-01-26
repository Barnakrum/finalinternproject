using finalinternshipproject.Dtos.User;
using finalinternshipproject.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace finalinternshipproject.Controllers
{
    [ApiController]
    
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;


        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await authService.Register(request.Username, request.Password);
            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var response = await authService.Login(request.Username, request.Password);
            return Ok(response);
        }
        [AllowAnonymous]
        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<ServiceResponse<string>>> UserSession()
        {
            var response = await authService.UserSession();
            return Ok(response);
        }
        [Authorize]
        [HttpGet("currentId")]
        public async Task<ActionResult<ServiceResponse<int>>> UserId()
        {
            var response = authService.GetUserId();
            return Ok(response);
        }





    }
}

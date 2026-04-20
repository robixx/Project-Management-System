using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;


namespace TaskWorker.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[area]/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {

        private readonly IUserInfo _userinfo;

        public UserInfoController(IUserInfo userinfo)
        {
            _userinfo = userinfo;
        }


        [HttpPost("user-info-create")]
        public async Task<IActionResult> UserInfo([FromForm] AppUserDto appUserDto )
        {
            var result= await _userinfo.SaveUserAsync(appUserDto);
            return Ok(new
            {
                status = result.Status,
                message = result.Message,
            });
        }

        [HttpPost("user-register")]
        public async Task<IActionResult> UserRegister([FromBody] AppSecUserDto secUser)
        {
            var result = await _userinfo.SaveUserRegisterAsync(secUser);
            return Ok(new
            {
                status = result.Status,
                message = result.Message,
            });
        }
    }
}

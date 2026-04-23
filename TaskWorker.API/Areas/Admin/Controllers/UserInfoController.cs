using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;


namespace TaskWorker.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[area]/[controller]")]
    [ApiController]
    [Authorize]
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
            var (status, message)= await _userinfo.SaveUserAsync(appUserDto);
            return Ok(new
            {
                status ,
                message,
            });
        }

        [HttpPost("user-register")]
        public async Task<IActionResult> UserRegister([FromBody] AppSecUserDto secUser)
        {
            var (status, message) = await _userinfo.SaveUserRegisterAsync(secUser);
            return Ok(new
            {
                status,
                message,
            });
        }

        [HttpGet("get-user-list")]
        public async Task<IActionResult> GetUserList()
        {
            var (message, status, data) = await _userinfo.GetUserListAsync();
            return Ok(new
            {
                message,
                status,
                data
            });
        }
    }
}

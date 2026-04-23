using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;
using TaskWorker.Infrastructure.Utility;

namespace TaskWorker.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[area]/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuth _auth;
        private readonly JwtConfig _jwtconfig;
        private readonly IUserInfo _userinfo;
       
        public AuthController(IAuth auth, JwtConfig jwtConfig, IUserInfo userinfo)
        {
            _auth = auth;
            _jwtconfig = jwtConfig;
            _userinfo = userinfo;
        }

        [HttpPost("auth-user")]
        public async Task<IActionResult> Login_User([FromBody] LoginReqquest auth)
        {
            if (auth == null)
            {
                var json = new
                {
                    code = "106",
                    message = "Endpoint parameter required",
                    data = ""
                };

                return BadRequest(json);
            }
            var username = auth.loginName;
            if (string.IsNullOrWhiteSpace(username))
            {
                var jsonData = new
                {
                    code = "108",
                    message = "Invalid username",
                    data = new
                    {
                        token = ""
                    }
                };
                return Unauthorized(jsonData);
            }

            
             var password = auth.password;

            
            LoginResponseDto? response = await _auth.AuthenticateAsync(auth);
            if (response != null && response.UserId > 0)
            {
                JwtUser jwt = new()
                {
                    UserId = response.UserId??0,                   
                    DisplayName = response.DisplayName,
                    RoleId = response.RoleId,
                    RoleName = response.RoleName,
                    UnitId = response.UnitId,
                    TokenExpired = DateTime.Now.AddMinutes(30)
                };


                if (jwt != null)
                {
                    string strToken = _jwtconfig.Generate(jwt);

                    var userProfle = await _userinfo.GetloginUser(response.UserId??0);
                    var jsonData = new
                    {
                        code = "200",
                        message = "Login Successfull",
                        data = userProfle,
                        token = strToken
                    };

                    return Ok(jsonData);
                }
                else
                {
                    var jsonData = new
                    {
                        code = "108",
                        message = "Invalid username/password",
                        token = ""
                    };
                    return Unauthorized(jsonData);
                }
            }
            else
            {
                var jsonData = new
                {
                    code = "108",
                    message = "Invalid username/password",
                    token = ""
                };
                return Unauthorized(jsonData);
            }
        }
    }
}

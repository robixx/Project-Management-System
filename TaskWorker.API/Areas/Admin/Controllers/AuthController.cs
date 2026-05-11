using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;
using TaskWorker.Infrastructure.Utility;

namespace TaskWorker.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class authController : Controller
    {
        private readonly IAuth _auth;
        private readonly JwtConfig _jwtconfig;
        private readonly IUserInfo _userinfo;

        public authController(IAuth auth, JwtConfig jwtConfig, IUserInfo userinfo)
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
                var response = new ApiResponse<object>
                {
                    Code = "106",
                    Message = "Endpoint parameter required",
                    Data = null,
                    Token = string.Empty
                };

                return BadRequest(response);
            }

            var username = auth.loginName;
            if (string.IsNullOrWhiteSpace(username))
            {
                var response = new ApiResponse<object>
                {
                    Code = "108",
                    Message = "Invalid username",
                    Data = null,
                    Token = string.Empty
                };

                return Unauthorized(response);
            }

            try
            {
                LoginResponseDto? response = await _auth.AuthenticateAsync(auth);

                if (response != null && response.UserId > 0)
                {
                    JwtUser jwt = new()
                    {
                        UserId = response.UserId ?? 0,
                        DisplayName = response.DisplayName,
                        RoleId = response.RoleId,
                        RoleName = response.RoleName,
                        UnitId = response.UnitId,
                        TokenExpired = DateTime.Now.AddMinutes(30)
                    };

                    string strToken = _jwtconfig.Generate(jwt);

                    var userProfle = await _userinfo.GetloginUser(response.UserId ?? 0);

                    var successResponse = new ApiResponse<object>
                    {
                        Code = "200",
                        Message = "Login Successful",
                        Data = userProfle,
                        Token = strToken
                    };

                    return Ok(successResponse);
                }

                var failResponse = new ApiResponse<object>
                {
                    Code = "108",
                    Message = "Invalid username/password",
                    Data = null,
                    Token = string.Empty
                };
                return Unauthorized(failResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<object>
                {
                    Code = "500",
                    Message = "An error occurred: " + ex.Message,
                    Data = null,
                    Token = string.Empty
                };
                return StatusCode(500, errorResponse);
            }
        }
    }
}

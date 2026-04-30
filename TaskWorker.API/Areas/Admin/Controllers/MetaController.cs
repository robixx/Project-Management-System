using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskWorker.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class MetaController : ControllerBase
    {

        private readonly IBaseData _metadata;

        public MetaController(IBaseData metadata)
        {
            _metadata = metadata;
        }

        [HttpPost("metadata-create")]
        public async Task<IActionResult> CreateMetaData([FromBody] MetaDataDto metaDataDto)
        {

            var (message, status) = await _metadata.GetBaseDataAsync(metaDataDto);
            if (status)
            {
                return Ok(new { message, status });
            }
            else
            {
                return BadRequest(new { message, status });
            }
        }


        [HttpGet("metadata-list")]
        public async Task<IActionResult> GetMetaDataList()
        {
            var data = await _metadata.GetMetaDataAsync();
            return Ok(data);
        }


        [HttpPost("metadata-element-create")]
        public async Task<IActionResult> CreateMetaDataElement([FromBody] MetaElementDto metaElementDto)
        {

            var (message, status) = await _metadata.GetBaseDataElementAsync(metaElementDto);
            if (status)
            {
                return Ok(new { message, status });
            }
            else
            {
                return BadRequest(new { message, status });
            }
        }




        [HttpGet("all-metadata-list")]
        public async Task<IActionResult> GetAllMetaDataList()
        {
            var (message, status, meta_list) = await _metadata.GetAllDataElementAsync();
            return Ok(new
            {
                status,
                message,
                meta_list
            });
        }


        [HttpGet("role-list")]
        public async Task<IActionResult> GetRoleList()
        {
            var (message, status, role_list) = await _metadata.GetRoleListAsync();
            return Ok(new
            {
                status,
                message,
                role_list
            });
        }


        [HttpPost("role-create")]
        public async Task<IActionResult> CreateRole([FromBody] RoleDto roleDto)
        {
            var (message, status) = await _metadata.RoleCreateAsync(roleDto);
            if (status)
            {
                return Ok(new { message, status });
            }
            else
            {
                return Ok(new { message, status });
            }
        }

        [HttpGet("role-wise-menu-list/{roleid}")]
        public async Task<IActionResult> GetRoleWiseMenuList(int roleid)
        {
            var (status, message, menu_list) = await _metadata.RoleWiseMenuListAsync(roleid);
            return Ok(new
            {
                status,
                message,
                menu_list
            });
        }


        [HttpPost("role-wise-page-permission")]
        public async Task<IActionResult> MenuPermission([FromBody] List<MenuPermissionDto> menudata)
        {
            var (status, message) = await _metadata.RoleWiseMenuPermissionAsync(menudata);
            return Ok(new
            {
                status,
                message,

            });
        }

        [HttpGet("user-role-list")]
        public async Task<IActionResult> GetUserRoleList()
        {
            var (status, message, user_role_list) = await _metadata.GetUserRoleListAsync();
            return Ok(new
            {
                status,
                message,
                user_role_list
            });
        }


        [HttpPost("role-wise-user-permission")]
        public async Task<IActionResult> UserRolePermission([FromBody] List<UserRoleSetDto> userrole)
        {
            var (status, message) = await _metadata.RoleWiseUserPermissionAsync(userrole);
            return Ok(new
            {
                status,
                message,

            });
        }

        [HttpGet("priority-list")]
        public async Task<IActionResult> GetPriorityList()
        {
            var data = await _metadata.GetPriorityListAsync();
            return Ok(data);
           
        }
    }
}

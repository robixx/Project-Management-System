using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;

namespace TaskWorker.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[area]/[controller]")]
    [ApiController]
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
            var data = await _metadata.getMetaDataAsync();
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
            var data = await _metadata.GetAllDataElementAsync();
            return Ok(new
            {
                status = data.Status,
                message = data.Message,
                data = data.meta_list
            });
        }


        [HttpGet("role-list")]
        public async Task<IActionResult> GetRoleList()
        {
            var data = await _metadata.GetRoleListAsync();
            return Ok(new
            {
                status = data.Status,
                message = data.Message,
                data = data.role_list
            });
        }
    }
}

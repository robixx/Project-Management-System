using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.ModelViews;

namespace TaskWorker.Application.Interfaces
{
    public interface IBaseData
    {
        Task<(string Message, bool Status)> GetBaseDataAsync(MetaDataDto metaDataDto);
        Task<(string Message, bool Status)> GetBaseDataElementAsync(MetaElementDto metaElementDto);
        Task<(string Message, bool Status,List<DataElementDto>meta_list)> GetAllDataElementAsync();
        Task<List<DropDownDto>> GetMetaDataAsync();
        Task<(string Message, bool Status, List<RoleDto>role_list)> GetRoleListAsync();
        Task<(string Message, bool Status)> RoleCreateAsync(RoleDto roleDto);
        Task<(string Message, bool Status,List<RoleWiseMenuDto>menu_list)> RoleWiseMenuListAsync(int roleid);       
        Task<(string Message, bool Status)> RoleWiseMenuPermissionAsync(List<MenuPermissionDto> menudata);
        Task<(string Message, bool Status, List<UserRoleDto> user_role_list)> GetUserRoleListAsync();
    }
}

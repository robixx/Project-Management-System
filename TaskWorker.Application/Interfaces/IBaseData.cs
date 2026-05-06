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
        Task<(string Message, bool Status,List<DataElementDto>data)> GetAllDataElementAsync();
        Task<List<DropDownDto>> GetMetaDataAsync();
        Task<(string Message, bool Status, List<RoleDto>data)> GetRoleListAsync();
        Task<(string Message, bool Status)> RoleCreateAsync(RoleDto roleDto);
        Task<(string Message, bool Status,List<RoleWiseMenuDto>data)> RoleWiseMenuListAsync(int roleid);       
        Task<(string Message, bool Status)> RoleWiseMenuPermissionAsync(List<MenuPermissionDto> menudata);
        Task<(string Message, bool Status, List<UserRoleDto>data)> GetUserRoleListAsync();
        Task<(string Message, bool Status)> RoleWiseUserPermissionAsync(List<UserRoleSetDto> userrole);
        Task<List<DropDownDto>> GetTeamListAsync();
        Task<List<DropDownDto>> GetPriorityListAsync();
    }
}

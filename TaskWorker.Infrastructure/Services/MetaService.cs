using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.Interfaces;
using TaskWorker.Application.ModelViews;
using TaskWorker.Domain.Entity;
using TaskWorker.Infrastructure.DBConnection;

namespace TaskWorker.Infrastructure.Services
{
    public class MetaService : IBaseData
    {
        private readonly DatabaseConnection _connection;

        public MetaService(DatabaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<(string Message, bool Status, List<DataElementDto> meta_list)> GetAllDataElementAsync()
        {
            try
            {
                var data= await( from m in _connection.AppMetaElement
                                 join me in _connection.AppMetaData on m.MetaDataId equals me.Id
                                 where me.IsActive==1
                                 select new DataElementDto
                                 {
                                     ElementId= m.ElementId,
                                     MetaDataId= m.MetaDataId,
                                     ElementValue= m.ElementValue,
                                     DataElementLevel=me.DataElementLevel,
                                     IsActive=me.IsActive,
                                 }).ToListAsync();
                if(data != null )
                {
                    return ("Data retrived successfully", true, data);
                }
                return ("No Data Found", false, new List<DataElementDto>());

            }
            catch (Exception ex)
            {
                return ($"Action method->{nameof(GetAllDataElementAsync)} Error->{ex.Message}", false, new List<DataElementDto>());
            }
        }

        public async Task<(string Message, bool Status)> GetBaseDataAsync(MetaDataDto metaDataDto)
        {
            try
            {
                if(metaDataDto == null)
                {
                    return ("MetaDataDto is null", false);
                }
                var query = new AppMetaData
                {
                    DataElementLevel=metaDataDto.DataElementLevel,
                    IsActive=metaDataDto.IsActive
                };

                await _connection.AppMetaData.AddAsync(query);
                await _connection.SaveChangesAsync();

                return($"{metaDataDto.DataElementLevel} Created successfully", true);

            }
            catch (Exception ex)
            {
                return($"Error occurred while retrieving base data: {ex.Message}", false);
            }
        }

        public async Task<(string Message, bool Status)> GetBaseDataElementAsync(MetaElementDto metaElementDto)
        {
            try
            {
                if (metaElementDto == null)
                {
                    return ("metaElementDto is null", false);
                }
                var query = new AppMetaElement
                {
                    ElementValue = metaElementDto.ElementValue,
                    MetaDataId= metaElementDto.MetaDataId,
                    
                };

                await _connection.AppMetaElement.AddAsync(query);
                await _connection.SaveChangesAsync();

                return ($"{metaElementDto.ElementValue} created successfully", true);

            }
            catch (Exception ex)
            {
                return ($"Error occurred while retrieving base data: {ex.Message}", false);
            }
        }

        public async Task<List<DropDownDto>> GetMetaDataAsync()
        {
            try
            {
                var data= await _connection.AppMetaData
                    .Where(x => x.IsActive==1)
                    .Select(x => new DropDownDto
                    {
                        Id = x.Id,
                        Name = x.DataElementLevel
                    })
                    .ToListAsync();
                data.Insert(0, new DropDownDto
                {
                    Id = 0,
                    Name = "Select"
                });
                return data;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while retrieving base data: {ex.Message}");
            }
        }

        public async Task<(string Message, bool Status, List<RoleDto> role_list)> GetRoleListAsync()
        {
            try
            {
                   var data = await _connection.AppRole
                    .Select(x => new RoleDto
                    {
                        RoleId = x.RoleId,
                        RoleName = x.RoleName,
                        IsActive = x.IsActive
                    })
                    .ToListAsync();

                return ("Roles Retrieved successfully", true, data);
            }
            catch (Exception ex)
            {
                return ($"Action method->{nameof(GetRoleListAsync)} Error->{ex.Message}", false, new List<RoleDto>());
            }
        }

        public async Task<(string Message, bool Status)> RoleCreateAsync(RoleDto roleDto)
        {
            try
            {
                if(roleDto == null)
                    return ("RoleDto is null", false);
                var isExist = await _connection.AppRole
                           .AnyAsync(x => x.RoleName == roleDto.RoleName
                                       && x.RoleId != roleDto.RoleId);

                if (isExist)
                    return ($"{roleDto.RoleName} already exists", false);

                string msg = string.Empty;

                if (roleDto.RoleId > 0)
                {
                    var existingRole = await _connection.AppRole.FindAsync(roleDto.RoleId);
                    if (existingRole != null)
                    {
                        existingRole.RoleName = roleDto.RoleName;
                        existingRole.IsActive = roleDto.IsActive;
                        msg = $"{roleDto.RoleName} Update successfully";
                    }
                }
                else
                {
                    var query = new AppRole
                    {
                        RoleName = roleDto.RoleName,
                        IsActive = roleDto.IsActive
                    };
                    await _connection.AppRole.AddAsync(query);
                   
                    msg= $"{roleDto.RoleName} Created successfully";
                }
                await _connection.SaveChangesAsync();
                return (msg, true);
            }
            catch (Exception ex)
            {
                return ($"Action method->{nameof(RoleCreateAsync)} Error->{ex.Message}", false);

            }
        }

        public async Task<(string Message, bool Status, List<RoleWiseMenuDto> menu_list)> RoleWiseMenuListAsync(int roleid)
        {
            try
            {
                var data = await _connection
                       .Set<RoleWiseMenuDto>()
                       .FromSqlRaw("SELECT * FROM get_role_wise_menu({0})", roleid)
                       .ToListAsync();


                return ("MenuData loaded successfully", true, data);
            }
            catch (Exception ex)
            {
                return ($"Error: {ex.Message}", false, new List<RoleWiseMenuDto>());
            }
        }

        public async Task<(string Message, bool Status)> RoleWiseMenuPermissionAsync(List<MenuPermissionDto> menudata)
        {
            try
            {
                if (menudata == null || menudata.Count == 0)
                    return ("No valid menu data", false);

                
                var roleId = menudata.First().RoleId;

                // ================= DELETE OLD PERMISSIONS =================
                var oldPermissions = _connection.AppRoleWiseMenu
                    .Where(x => x.RoleId == roleId);

                _connection.AppRoleWiseMenu.RemoveRange(oldPermissions);

                // ================= INSERT NEW PERMISSIONS =================
                foreach (var item in menudata)
                {
                    if (item.IsActive == 1) // only selected menus
                    {
                        var entity = new AppRoleWiseMenu
                        {
                            RoleId = item.RoleId,
                            MenuId = item.MenuId,
                            IsActive = item.IsActive,
                        };

                        await _connection.AppRoleWiseMenu.AddAsync(entity);
                    }
                }

                await _connection.SaveChangesAsync();

                return ("Permission updated successfully", true);
            }
            catch (Exception ex)
            {
                return ($"Error: {ex.Message}", false);
            }
        }
    }
}

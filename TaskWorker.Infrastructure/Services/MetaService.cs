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

        public async Task<List<DropDownDto>> getMetaDataAsync()
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
    }
}

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
        Task<List<DropDownDto>> getMetaDataAsync();
    }
}

using System.Collections.Generic;
using BH.Code;
using BH.Domain.Entity;
using BH.Application.Dto;
using System.Threading.Tasks;

namespace BH.IApplication
{
    public interface ILogApp
    {
        Task<List<LogDto>> GetList(Pagination pagination, string queryJson);
        Task<int> RemoveLog(string keepTime);
        Task<int> WriteDbLog(bool result, string resultLog);
        Task<int> WriteDbLog(LogDto logInputDto);
    }
}
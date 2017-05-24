using System.Collections.Generic;
using BH.Code;
using BH.Domain.Entity;

namespace BH.IApplication
{
    public interface ILogApp
    {
        List<Sys_Log> GetList(Pagination pagination, string queryJson);
        void RemoveLog(string keepTime);
        void WriteDbLog(bool result, string resultLog);
        void WriteDbLog(Sys_Log Sys_Log);
    }
}
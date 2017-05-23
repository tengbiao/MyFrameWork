using System.Collections.Generic;
using BH.Code;
using BH.Domain.Entity.SystemSecurity;

namespace BH.IApplication
{
    public interface ILogApp
    {
        List<LogEntity> GetList(Pagination pagination, string queryJson);
        void RemoveLog(string keepTime);
        void WriteDbLog(bool result, string resultLog);
        void WriteDbLog(LogEntity logEntity);
    }
}
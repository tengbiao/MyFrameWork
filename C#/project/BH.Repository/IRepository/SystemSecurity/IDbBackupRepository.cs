using BH.Data;
using BH.Domain.Entity;

namespace BH.Repository.IRepository
{
    public interface IDbBackupRepository : IRepository<Sys_DbBackup>
    {
        void DeleteForm(string keyValue);
        void ExecuteDbBackup(Sys_DbBackup Sys_DbBackup);
    }
}

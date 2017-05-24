using BH.Code;
using BH.Data;
using BH.Domain.Entity;
using BH.Repository.IRepository;

namespace BH.Repository.SystemSecurity
{
    public class DbBackupRepository : Repository<Sys_DbBackup>, IDbBackupRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var scope = new System.Transactions.TransactionScope())
            {
                var Sys_DbBackup = FindKey(keyValue);
                if (Sys_DbBackup != null)
                {
                    FileHelper.DeleteFile(Sys_DbBackup.F_FilePath);
                }
                Delete(Sys_DbBackup);
                scope.Complete();
            }
        }
        public async void ExecuteDbBackup(Sys_DbBackup Sys_DbBackup)
        {
            await ExecuteSqlCommandAsync(string.Format("backup database {0} to disk ='{1}'", Sys_DbBackup.F_DbName, Sys_DbBackup.F_FilePath));
            Sys_DbBackup.F_FileSize = FileHelper.ToFileSize(FileHelper.GetFileSize(Sys_DbBackup.F_FilePath));
            Sys_DbBackup.F_FilePath = "/Resource/DbBackup/" + Sys_DbBackup.F_FileName;
            await InsertAsync(Sys_DbBackup);
        }
    }
}

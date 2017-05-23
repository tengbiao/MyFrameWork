/*******************************************************************************
 * Copyright © 2016 BH.Framework 版权所有
 * Author: BH
 * Description: BH快速开发平台
 * Website：http://www.BH.cn
*********************************************************************************/
using BH.Code;
using BH.Data;
using BH.Data.Extensions;
using BH.Domain.Entity.SystemSecurity;
using BH.Repository.IRepository.SystemSecurity;
using BH.Repository.SystemSecurity;

namespace BH.Repository.SystemSecurity
{
    public class DbBackupRepository : Repository<DbBackupEntity>, IDbBackupRepository
    {
        public void DeleteForm(string keyValue)
        {
            //using (var db = new RepositoryBase().BeginTrans())
            //{
            //    var dbBackupEntity = db.FindEntity<DbBackupEntity>(keyValue);
            //    if (dbBackupEntity != null)
            //    {
            //        FileHelper.DeleteFile(dbBackupEntity.F_FilePath);
            //    }
            //    db.Delete<DbBackupEntity>(dbBackupEntity);
            //    db.Commit();
            //}
        }
        public async void ExecuteDbBackup(DbBackupEntity dbBackupEntity)
        {
            await ExecuteSqlCommandAsync(string.Format("backup database {0} to disk ='{1}'", dbBackupEntity.F_DbName, dbBackupEntity.F_FilePath));
            dbBackupEntity.F_FileSize = FileHelper.ToFileSize(FileHelper.GetFileSize(dbBackupEntity.F_FilePath));
            dbBackupEntity.F_FilePath = "/Resource/DbBackup/" + dbBackupEntity.F_FileName;
            await InsertAsync(dbBackupEntity);
        }
    }
}

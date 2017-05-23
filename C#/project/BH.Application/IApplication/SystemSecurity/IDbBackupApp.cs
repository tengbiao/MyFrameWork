using System.Collections.Generic;
using BH.Domain.Entity.SystemSecurity;

namespace BH.IApplication
{
    public interface IDbBackupApp
    {
        void DeleteForm(string keyValue);
        DbBackupEntity GetForm(string keyValue);
        List<DbBackupEntity> GetList(string queryJson);
        void SubmitForm(DbBackupEntity dbBackupEntity);
    }
}
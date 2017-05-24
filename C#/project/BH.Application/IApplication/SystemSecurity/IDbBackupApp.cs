using BH.Domain.Entity;
using System.Collections.Generic;

namespace BH.IApplication
{
    public interface IDbBackupApp
    {
        void DeleteForm(string keyValue);
        Sys_DbBackup GetForm(string keyValue);
        List<Sys_DbBackup> GetList(string queryJson);
        void SubmitForm(Sys_DbBackup Sys_DbBackup);
    }
}
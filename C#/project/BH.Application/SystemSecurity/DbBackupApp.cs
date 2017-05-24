using BH.Code;
using BH.Domain.Entity;
using BH.IApplication;
using BH.Repository.IRepository;
using BH.Repository.SystemSecurity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemSecurity
{
    public class DbBackupApp : IDbBackupApp
    {
        private IDbBackupRepository service = new DbBackupRepository();

        public List<Sys_DbBackup> GetList(string queryJson)
        {
            var expression = ExtLinq.True<Sys_DbBackup>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "DbName":  
                        expression = expression.And(t => t.F_DbName.Contains(keyword));
                        break;
                    case "FileName":
                        expression = expression.And(t => t.F_FileName.Contains(keyword));
                        break;
                }
            }
            return service.IQueryable(expression).OrderByDescending(t => t.F_BackupTime).ToList();
        }
        public Sys_DbBackup GetForm(string keyValue)
        {
            return service.FindKey(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(Sys_DbBackup Sys_DbBackup)
        {
            Sys_DbBackup.F_Id = Common.GuId();
            Sys_DbBackup.F_EnabledMark = true;
            Sys_DbBackup.F_BackupTime = DateTime.Now;
            service.ExecuteDbBackup(Sys_DbBackup);
        }
    }
}

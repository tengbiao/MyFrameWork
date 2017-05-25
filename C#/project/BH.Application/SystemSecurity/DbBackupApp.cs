using BH.Application.Dto;
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

        public List<DbBackupDto> GetList(string queryJson)
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
            return service.IQueryable(expression).OrderByDescending(t => t.F_BackupTime).MapToList<DbBackupDto>();
        }
        public DbBackupDto GetForm(string keyValue)
        {
            return service.FindKey(keyValue).MapTo<DbBackupDto>();
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(DbBackupDto dbBackupInputDto)
        {
            var model = dbBackupInputDto.MapTo<Sys_DbBackup>();
            model.F_Id = Common.GuId();
            model.F_EnabledMark = true;
            model.F_BackupTime = DateTime.Now;
            service.ExecuteDbBackup(model);
        }
    }
}

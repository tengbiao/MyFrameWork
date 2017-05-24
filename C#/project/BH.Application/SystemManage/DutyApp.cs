using BH.Code;
using BH.Data;
using BH.Domain.Entity;
using BH.IApplication;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemManage
{
    public class DutyApp : IDutyApp
    {
        private readonly IRepository<Sys_Role> _repository;
        public DutyApp(IRepository<Sys_Role> repository)
        {
            this._repository = repository;
        }
        public List<Sys_Role> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<Sys_Role>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.F_Category == 2);
            return _repository.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }
        public Sys_Role GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            _repository.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(Sys_Role Sys_Role, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                Sys_Role.Modify(keyValue);
                _repository.Update(Sys_Role);
            }
            else
            {
                Sys_Role.Create();
                Sys_Role.F_Category = 2;
                _repository.Insert(Sys_Role);
            }
        }
    }
}

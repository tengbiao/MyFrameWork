using BH.Code;
using BH.Data;
using BH.Domain.Entity;
using BH.IApplication;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemSecurity
{
    public class FilterIPApp : IFilterIPApp
    {
        private readonly IRepository<Sys_FilterIP> _repository;
        public FilterIPApp(IRepository<Sys_FilterIP> repository)
        {
            this._repository = repository;
        }

        public List<Sys_FilterIP> GetList(string keyword)
        {
            var expression = ExtLinq.True<Sys_FilterIP>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_StartIP.Contains(keyword));
            }
            return _repository.IQueryable(expression).OrderByDescending(t => t.F_DeleteTime).ToList();
        }
        public Sys_FilterIP GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            _repository.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(Sys_FilterIP Sys_FilterIP, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                Sys_FilterIP.Modify(keyValue);
                _repository.Update(Sys_FilterIP);
            }
            else
            {
                Sys_FilterIP.Create();
                _repository.Insert(Sys_FilterIP);
            }
        }
    }
}

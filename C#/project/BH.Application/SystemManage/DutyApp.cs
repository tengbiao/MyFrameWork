using BH.Application.Dto;
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
        public List<DutyDto> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<Sys_Role>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.F_Category == 2);
            return _repository.IQueryable(expression).OrderBy(t => t.F_SortCode).MapToList<DutyDto>();
        }
        public DutyDto GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue).MapTo<DutyDto>();
        }
        public void DeleteForm(string keyValue)
        {
            _repository.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(DutyDto dutyInput, string keyValue)
        {
            var model = dutyInput.MapTo<Sys_Role>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                model.Modify(keyValue);
                _repository.Update(model);
            }
            else
            {
                model.Create();
                model.F_Category = 2;
                _repository.Insert(model);
            }
        }
    }
}

using BH.Application.Dto;
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

        public List<FilterIPDto> GetList(string keyword)
        {
            var expression = ExtLinq.True<Sys_FilterIP>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_StartIP.Contains(keyword));
            }
            return _repository.IQueryable(expression).OrderByDescending(t => t.F_DeleteTime).MapToList<FilterIPDto>();
        }
        public FilterIPDto GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue).MapTo<FilterIPDto>();
        }
        public void DeleteForm(string keyValue)
        {
            _repository.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(FilterIPDto filterIpInputDto, string keyValue)
        {
            var model = filterIpInputDto.MapTo<Sys_FilterIP>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                model.Modify(keyValue);
                _repository.Update(model);
            }
            else
            {
                model.Create();
                _repository.Insert(model);
            }
        }
    }
}

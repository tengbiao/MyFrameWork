using BH.Code;
using BH.Data;
using BH.Domain.Entity.SystemSecurity;
using BH.IApplication;
using BH.Repository.IRepository.SystemSecurity;
using BH.Repository.SystemSecurity;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemSecurity
{
    public class FilterIPApp : IFilterIPApp
    {
        private readonly IRepository<FilterIPEntity> _repository;
        public FilterIPApp(IRepository<FilterIPEntity> repository)
        {
            this._repository = repository;
        }

        public List<FilterIPEntity> GetList(string keyword)
        {
            var expression = ExtLinq.True<FilterIPEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_StartIP.Contains(keyword));
            }
            return _repository.IQueryable(expression).OrderByDescending(t => t.F_DeleteTime).ToList();
        }
        public FilterIPEntity GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            _repository.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(FilterIPEntity filterIPEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                filterIPEntity.Modify(keyValue);
                _repository.Update(filterIPEntity);
            }
            else
            {
                filterIPEntity.Create();
                _repository.Insert(filterIPEntity);
            }
        }
    }
}

using BH.Code;
using BH.Data;
using BH.Domain.Entity.SystemManage;
using BH.IApplication;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemManage
{
    public class DutyApp : IDutyApp
    {
        private readonly IRepository<RoleEntity> _repository;
        public DutyApp(IRepository<RoleEntity> repository)
        {
            this._repository = repository;
        }
        public List<RoleEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<RoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.F_Category == 2);
            return _repository.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }
        public RoleEntity GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            _repository.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(RoleEntity roleEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.Modify(keyValue);
                _repository.Update(roleEntity);
            }
            else
            {
                roleEntity.Create();
                roleEntity.F_Category = 2;
                _repository.Insert(roleEntity);
            }
        }
    }
}

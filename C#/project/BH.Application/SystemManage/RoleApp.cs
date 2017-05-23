using BH.Code;
using BH.Data;
using BH.Domain.Entity.SystemManage;
using BH.IApplication;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemManage
{
    public class RoleApp : IRoleApp
    {
        private readonly IRepository<RoleEntity> _repository;
        private readonly IRepository<RoleAuthorizeEntity> _roleAuthorizeRepository;
        private readonly IRepository<ModuleEntity> _moduleRepository;
        private readonly IRepository<ModuleButtonEntity> _moduleButtonRepository;

        public RoleApp(IRepository<RoleEntity> repository,
            IRepository<RoleAuthorizeEntity> roleAuthorizeRepository,
            IRepository<ModuleEntity> moduleRepository,
            IRepository<ModuleButtonEntity> moduleButtonRepository)
        {
            _repository = repository;
            _roleAuthorizeRepository = roleAuthorizeRepository;
            _moduleRepository = moduleRepository;
            _moduleButtonRepository = moduleButtonRepository;
        }

        public List<RoleEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<RoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.F_Category == 1);
            return _repository.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }

        public RoleEntity GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue);
        }

        public void DeleteForm(string keyValue)
        {
            _repository.LazySaveChanges();
            _roleAuthorizeRepository.LazySaveChanges();
            _repository.Delete(t => t.F_Id == keyValue);
            _roleAuthorizeRepository.Delete(t => t.F_ObjectId == keyValue);
            _repository.SaveChanges(true);
        }

        public void SubmitForm(RoleEntity roleEntity, string[] permissionIds, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.F_Id = keyValue;
            }
            else
            {
                roleEntity.F_Id = Common.GuId();
            }
            var moduledata = _moduleRepository.IQueryable().OrderBy(o => o.F_SortCode).ToList();
            var buttondata = _repository.IQueryable().OrderBy(t => t.F_SortCode).ToList();
            List<RoleAuthorizeEntity> roleAuthorizeEntitys = new List<RoleAuthorizeEntity>();
            foreach (var itemId in permissionIds)
            {
                RoleAuthorizeEntity roleAuthorizeEntity = new RoleAuthorizeEntity();
                roleAuthorizeEntity.F_Id = Common.GuId();
                roleAuthorizeEntity.F_ObjectType = 1;
                roleAuthorizeEntity.F_ObjectId = roleEntity.F_Id;
                roleAuthorizeEntity.F_ItemId = itemId;
                if (moduledata.Find(t => t.F_Id == itemId) != null)
                {
                    roleAuthorizeEntity.F_ItemType = 1;
                }
                if (buttondata.Find(t => t.F_Id == itemId) != null)
                {
                    roleAuthorizeEntity.F_ItemType = 2;
                }
                roleAuthorizeEntitys.Add(roleAuthorizeEntity);
            }

            using (var scope = new System.Transactions.TransactionScope())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    _repository.Update(roleEntity);
                }
                else
                {
                    roleEntity.F_Category = 1;
                    _repository.Insert(roleEntity);
                }
                _roleAuthorizeRepository.Delete(t => t.F_ObjectId == roleEntity.F_Id);
                _roleAuthorizeRepository.Insert(roleAuthorizeEntitys);
                scope.Complete();
            }
        }
    }
}

using BH.Code;
using BH.Data;
using BH.Domain.Entity;
using BH.IApplication;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemManage
{
    public class RoleApp : IRoleApp
    {
        private readonly IRepository<Sys_Role> _repository;
        private readonly IRepository<Sys_RoleAuthorize> _roleAuthorizeRepository;
        private readonly IRepository<Sys_Module> _moduleRepository;
        private readonly IRepository<Sys_ModuleButton> _moduleButtonRepository;

        public RoleApp(IRepository<Sys_Role> repository,
            IRepository<Sys_RoleAuthorize> roleAuthorizeRepository,
            IRepository<Sys_Module> moduleRepository,
            IRepository<Sys_ModuleButton> moduleButtonRepository)
        {
            _repository = repository;
            _roleAuthorizeRepository = roleAuthorizeRepository;
            _moduleRepository = moduleRepository;
            _moduleButtonRepository = moduleButtonRepository;
        }

        public List<Sys_Role> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<Sys_Role>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.F_Category == 1);
            return _repository.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }

        public Sys_Role GetForm(string keyValue)
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

        public void SubmitForm(Sys_Role Sys_Role, string[] permissionIds, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                Sys_Role.F_Id = keyValue;
            }
            else
            {
                Sys_Role.F_Id = Common.GuId();
            }
            var moduledata = _moduleRepository.IQueryable().OrderBy(o => o.F_SortCode).ToList();
            var buttondata = _repository.IQueryable().OrderBy(t => t.F_SortCode).ToList();
            List<Sys_RoleAuthorize> Sys_RoleAuthorizes = new List<Sys_RoleAuthorize>();
            foreach (var itemId in permissionIds)
            {
                Sys_RoleAuthorize Sys_RoleAuthorize = new Sys_RoleAuthorize();
                Sys_RoleAuthorize.F_Id = Common.GuId();
                Sys_RoleAuthorize.F_ObjectType = 1;
                Sys_RoleAuthorize.F_ObjectId = Sys_Role.F_Id;
                Sys_RoleAuthorize.F_ItemId = itemId;
                if (moduledata.Find(t => t.F_Id == itemId) != null)
                {
                    Sys_RoleAuthorize.F_ItemType = 1;
                }
                if (buttondata.Find(t => t.F_Id == itemId) != null)
                {
                    Sys_RoleAuthorize.F_ItemType = 2;
                }
                Sys_RoleAuthorizes.Add(Sys_RoleAuthorize);
            }

            using (var scope = new System.Transactions.TransactionScope())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    _repository.Update(Sys_Role);
                }
                else
                {
                    Sys_Role.F_Category = 1;
                    _repository.Insert(Sys_Role);
                }
                _roleAuthorizeRepository.Delete(t => t.F_ObjectId == Sys_Role.F_Id);
                _roleAuthorizeRepository.Insert(Sys_RoleAuthorizes);
                scope.Complete();
            }
        }
    }
}

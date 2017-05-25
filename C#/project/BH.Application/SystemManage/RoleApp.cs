using BH.Application.Dto;
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

        public List<RoleDto> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<Sys_Role>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.F_Category == 1);
            return _repository.IQueryable(expression).OrderBy(t => t.F_SortCode).MapToList<RoleDto>();
        }

        public RoleDto GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue).MapTo<DutyDto>();
        }

        public void DeleteForm(string keyValue)
        {
            using (var scope = new System.Transactions.TransactionScope())
            {
                _repository.Delete(t => t.F_Id == keyValue);
                _roleAuthorizeRepository.Delete(t => t.F_ObjectId == keyValue);
                _repository.SaveChanges();
                scope.Complete();
            }
        }

        public void SubmitForm(RoleDto roleInputDto, string[] permissionIds, string keyValue)
        {
            var model = roleInputDto.MapTo<Sys_Role>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                model.F_Id = keyValue;
            }
            else
            {
                model.F_Id = Common.GuId();
            }
            var moduledata = _moduleRepository.IQueryable().OrderBy(o => o.F_SortCode).Select(f => f.F_Id).ToList();
            var buttondata = _moduleButtonRepository.IQueryable().OrderBy(t => t.F_SortCode).Select(f => f.F_Id).ToList();
            List<Sys_RoleAuthorize> Sys_RoleAuthorizes = new List<Sys_RoleAuthorize>();
            foreach (var itemId in permissionIds)
            {
                Sys_RoleAuthorize Sys_RoleAuthorize = new Sys_RoleAuthorize();
                Sys_RoleAuthorize.F_Id = Common.GuId();
                Sys_RoleAuthorize.F_ObjectType = 1;
                Sys_RoleAuthorize.F_ObjectId = model.F_Id;
                Sys_RoleAuthorize.F_ItemId = itemId;
                if (moduledata.Contains(itemId))
                {
                    Sys_RoleAuthorize.F_ItemType = 1;
                }
                else if (buttondata.Contains(itemId))
                {
                    Sys_RoleAuthorize.F_ItemType = 2;
                }
                Sys_RoleAuthorizes.Add(Sys_RoleAuthorize);
            }

            using (var scope = new System.Transactions.TransactionScope())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    _repository.Update(model);
                }
                else
                {
                    model.F_Category = 1;
                    _repository.Insert(model);
                }
                _roleAuthorizeRepository.Delete(t => t.F_ObjectId == model.F_Id);
                _roleAuthorizeRepository.Insert(Sys_RoleAuthorizes);
                scope.Complete();
            }
        }
    }
}

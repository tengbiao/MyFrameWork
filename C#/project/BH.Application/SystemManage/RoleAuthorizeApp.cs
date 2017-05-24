using BH.Code;
using BH.Domain.Entity;
using BH.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using BH.IApplication;
using BH.Data;

namespace BH.Application.SystemManage
{
    public class RoleAuthorizeApp : IRoleAuthorizeApp
    {
        private readonly IRepository<Sys_RoleAuthorize> _repository;
        private readonly IRepository<Sys_Module> _moduleRepository;
        private readonly IRepository<Sys_ModuleButton> _moduleButtonRepository;

        public RoleAuthorizeApp(IRepository<Sys_RoleAuthorize> repository,
            IRepository<Sys_Module> moduleRepository,
            IRepository<Sys_ModuleButton> moduleButtonRepository)
        {
            _repository = repository;
            _moduleRepository = moduleRepository;
            _moduleButtonRepository = moduleButtonRepository;
        }

        public List<Sys_RoleAuthorize> GetList(string ObjectId)
        {
            return _repository.IQueryable(t => t.F_ObjectId == ObjectId).ToList();
        }
        public List<Sys_Module> GetMenuList(string roleId)
        {
            var data = new List<Sys_Module>();
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                data = _moduleRepository.IQueryable().OrderBy(o => o.F_SortCode).ToList();
            }
            else
            {
                var moduledata = _moduleRepository.IQueryable().OrderBy(o => o.F_SortCode).ToList();
                var authorizedata = _repository.IQueryable(t => t.F_ObjectId == roleId && t.F_ItemType == 1).ToList();
                foreach (var item in authorizedata)
                {
                    Sys_Module Sys_Module = moduledata.Find(t => t.F_Id == item.F_ItemId);
                    if (Sys_Module != null)
                    {
                        data.Add(Sys_Module);
                    }
                }
            }
            return data.OrderBy(t => t.F_SortCode).ToList();
        }
        public List<Sys_ModuleButton> GetButtonList(string roleId)
        {
            var data = new List<Sys_ModuleButton>();
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                data = _moduleButtonRepository.IQueryable().OrderBy(o => o.F_SortCode).ToList();
            }
            else
            {
                var buttondata = _moduleButtonRepository.IQueryable().OrderBy(o => o.F_SortCode).ToList();
                var authorizedata = _repository.IQueryable(t => t.F_ObjectId == roleId && t.F_ItemType == 2).ToList();
                foreach (var item in authorizedata)
                {
                    Sys_ModuleButton Sys_ModuleButton = buttondata.Find(t => t.F_Id == item.F_ItemId);
                    if (Sys_ModuleButton != null)
                    {
                        data.Add(Sys_ModuleButton);
                    }
                }
            }
            return data.OrderBy(t => t.F_SortCode).ToList();
        }
        public bool ActionValidate(string roleId, string moduleId, string action)
        {
            var authorizeurldata = new List<AuthorizeActionModel>();
            var cachedata = CacheFactory.Cache().GetCache<List<AuthorizeActionModel>>("authorizeurldata_" + roleId);
            if (cachedata == null)
            {
                var moduledata = _moduleRepository.IQueryable().OrderBy(o => o.F_SortCode).ToList();
                var buttondata = _moduleButtonRepository.IQueryable().OrderBy(o => o.F_SortCode).ToList();
                var authorizedata = _repository.IQueryable(t => t.F_ObjectId == roleId).ToList();

                //添加模块权限
                authorizeurldata.AddRange(moduledata.Where(s => authorizedata.Where(a => a.F_ItemType == 1).Select(m => m.F_ItemId).Contains(s.F_Id))
                    .Select(r => new AuthorizeActionModel()
                    {
                        F_Id = r.F_Id,
                        F_UrlAddress = r.F_UrlAddress
                    }));

                //添加按钮权限
                authorizeurldata.AddRange(buttondata.Where(s => authorizedata.Where(a => a.F_ItemType == 2).Select(m => m.F_ItemId).Contains(s.F_Id))
                   .Select(r => new AuthorizeActionModel()
                   {
                       F_Id = r.F_ModuleId,
                       F_UrlAddress = r.F_UrlAddress
                   }));
                CacheFactory.Cache().WriteCache(authorizeurldata, "authorizeurldata_" + roleId, DateTime.Now.AddMinutes(5));
            }
            else
            {
                authorizeurldata = cachedata;
            }
            return authorizeurldata.Count(a => a.F_Id.Equals(moduleId) && a.F_UrlAddress.Equals(action)) > 0;
        }
    }
}

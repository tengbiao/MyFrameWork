using System.Collections.Generic;
using BH.Domain.Entity;

namespace BH.IApplication
{
    public interface IRoleAuthorizeApp
    {
        bool ActionValidate(string roleId, string moduleId, string action);
        List<Sys_ModuleButton> GetButtonList(string roleId);
        List<Sys_RoleAuthorize> GetList(string ObjectId);
        List<Sys_Module> GetMenuList(string roleId);
    }
}
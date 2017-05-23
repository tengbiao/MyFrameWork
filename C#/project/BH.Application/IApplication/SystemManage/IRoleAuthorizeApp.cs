using System.Collections.Generic;
using BH.Domain.Entity.SystemManage;

namespace BH.IApplication
{
    public interface IRoleAuthorizeApp
    {
        bool ActionValidate(string roleId, string moduleId, string action);
        List<ModuleButtonEntity> GetButtonList(string roleId);
        List<RoleAuthorizeEntity> GetList(string ObjectId);
        List<ModuleEntity> GetMenuList(string roleId);
    }
}
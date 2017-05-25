using System.Collections.Generic;
using BH.Domain.Entity;
using BH.Application.Dto;

namespace BH.IApplication
{
    public interface IRoleAuthorizeApp
    {
        bool ActionValidate(string roleId, string moduleId, string action);
        List<ModuleButtonDto> GetButtonList(string roleId);
        List<RoleAuthorizeDto> GetList(string ObjectId);
        List<ModuleDto> GetMenuList(string roleId);
    }
}
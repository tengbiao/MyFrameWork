using System.Collections.Generic;
using BH.Domain.Entity.SystemManage;

namespace BH.IApplication
{
    public interface IRoleApp
    {
        void DeleteForm(string keyValue);
        RoleEntity GetForm(string keyValue);
        List<RoleEntity> GetList(string keyword = "");
        void SubmitForm(RoleEntity roleEntity, string[] permissionIds, string keyValue);
    }
}
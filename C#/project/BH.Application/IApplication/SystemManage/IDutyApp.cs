using System.Collections.Generic;
using BH.Domain.Entity.SystemManage;

namespace BH.Application.IApplication
{
    public interface IDutyApp
    {
        void DeleteForm(string keyValue);
        RoleEntity GetForm(string keyValue);
        List<RoleEntity> GetList(string keyword = "");
        void SubmitForm(RoleEntity roleEntity, string keyValue);
    }
}
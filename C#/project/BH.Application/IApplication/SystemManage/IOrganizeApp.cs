using System.Collections.Generic;
using BH.Domain.Entity.SystemManage;

namespace BH.IApplication
{
    public interface IOrganizeApp
    {
        void DeleteForm(string keyValue);
        OrganizeEntity GetForm(string keyValue);
        List<OrganizeEntity> GetList();
        void SubmitForm(OrganizeEntity organizeEntity, string keyValue);
    }
}
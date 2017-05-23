using System.Collections.Generic;
using BH.Domain.Entity.SystemManage;

namespace BH.IApplication
{
    public interface IModuleApp
    {
        void DeleteForm(string keyValue);
        ModuleEntity GetForm(string keyValue);
        List<ModuleEntity> GetList();
        void SubmitForm(ModuleEntity moduleEntity, string keyValue);
    }
}
using System.Collections.Generic;
using BH.Domain.Entity;
using BH.Application.Dto;

namespace BH.IApplication
{
    public interface IModuleButtonApp
    {
        void DeleteForm(string keyValue);
        ModuleButtonDto GetForm(string keyValue);
        List<ModuleButtonDto> GetList(string moduleId = "");
        void SubmitCloneButton(string moduleId, string Ids);
        void SubmitForm(ModuleButtonDto moduleButtonDto, string keyValue);
    }
}
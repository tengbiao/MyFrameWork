using System.Collections.Generic;
using BH.Domain.Entity;
using BH.Application.Dto;

namespace BH.IApplication
{
    public interface IModuleApp
    {
        void DeleteForm(string keyValue);
        ModuleDto GetForm(string keyValue);
        List<ModuleDto> GetList();
        void SubmitForm(ModuleDto moduleDto, string keyValue);
    }
}
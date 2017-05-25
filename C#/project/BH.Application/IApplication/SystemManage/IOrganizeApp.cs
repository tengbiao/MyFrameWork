using System.Collections.Generic;
using BH.Application.Dto;

namespace BH.IApplication
{
    public interface IOrganizeApp
    {
        void DeleteForm(string keyValue);
        OrganizaDto GetForm(string keyValue);
        List<OrganizaDto> GetList();
        void SubmitForm(OrganizaDto organizaInputDto, string keyValue);
    }
}
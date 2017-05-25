using System.Collections.Generic;
using BH.Domain.Entity;
using BH.Application.Dto;

namespace BH.IApplication
{
    public interface IRoleApp
    {
        void DeleteForm(string keyValue);
        RoleDto GetForm(string keyValue);
        List<RoleDto> GetList(string keyword = "");
        void SubmitForm(RoleDto roleInputDto, string[] permissionIds, string keyValue);
    }
}
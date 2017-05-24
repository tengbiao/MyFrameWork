using System.Collections.Generic;
using BH.Domain.Entity;

namespace BH.IApplication
{
    public interface IFilterIPApp
    {
        void DeleteForm(string keyValue);
        Sys_FilterIP GetForm(string keyValue);
        List<Sys_FilterIP> GetList(string keyword);
        void SubmitForm(Sys_FilterIP Sys_FilterIP, string keyValue);
    }
}
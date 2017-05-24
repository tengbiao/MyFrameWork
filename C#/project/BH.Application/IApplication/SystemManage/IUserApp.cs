using System.Collections.Generic;
using BH.Code;
using BH.Domain.Entity;

namespace BH.IApplication
{
    public interface IUserApp
    {
        Sys_User CheckLogin(string username, string password);
        void DeleteForm(string keyValue);
        Sys_User GetForm(string keyValue);
        List<Sys_User> GetList(Pagination pagination, string keyword);
        void SubmitForm(Sys_User Sys_User, Sys_UserLogOn Sys_UserLogOn, string keyValue);
        void UpdateForm(Sys_User Sys_User);
    }
}
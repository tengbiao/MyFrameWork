using BH.Domain.Entity;

namespace BH.IApplication
{
    public interface IUserLogOnApp
    {
        Sys_UserLogOn GetForm(string keyValue);
        void RevisePassword(string userPassword, string keyValue);
        void UpdateForm(Sys_UserLogOn Sys_UserLogOn);
    }
}
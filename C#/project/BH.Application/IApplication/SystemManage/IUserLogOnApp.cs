using BH.Domain.Entity.SystemManage;

namespace BH.IApplication
{
    public interface IUserLogOnApp
    {
        UserLogOnEntity GetForm(string keyValue);
        void RevisePassword(string userPassword, string keyValue);
        void UpdateForm(UserLogOnEntity userLogOnEntity);
    }
}
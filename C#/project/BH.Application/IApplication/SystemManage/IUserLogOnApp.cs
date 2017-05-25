using BH.Application.Dto;
using BH.Domain.Entity;

namespace BH.IApplication
{
    public interface IUserLogOnApp
    {
        UserLogOnDto GetForm(string keyValue);
        void RevisePassword(string userPassword, string keyValue);
        void UpdateForm(UserLogOnDto userLogOnInputDto);
    }
}
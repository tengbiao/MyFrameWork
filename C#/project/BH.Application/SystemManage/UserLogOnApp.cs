using BH.Code;
using BH.Data;
using BH.Domain.Entity;
using BH.IApplication;

namespace BH.Application.SystemManage
{
    public class UserLogOnApp : IUserLogOnApp
    {
        private readonly IRepository<Sys_UserLogOn> _repository;
        public UserLogOnApp(IRepository<Sys_UserLogOn> repository)
        {
            this._repository = repository;
        }
        public Sys_UserLogOn GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue);
        }
        public void UpdateForm(Sys_UserLogOn Sys_UserLogOn)
        {
            _repository.Update(Sys_UserLogOn);
        }
        public void RevisePassword(string userPassword,string keyValue)
        {
            Sys_UserLogOn Sys_UserLogOn = new Sys_UserLogOn();
            Sys_UserLogOn.F_Id = keyValue;
            Sys_UserLogOn.F_UserSecretkey = Md5.md5(Common.CreateNo(), 16).ToLower();
            Sys_UserLogOn.F_UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userPassword, 32).ToLower(), Sys_UserLogOn.F_UserSecretkey).ToLower(), 32).ToLower();
            _repository.Update(Sys_UserLogOn);
        }
    }
}

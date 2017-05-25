using BH.Application.Dto;
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
        public UserLogOnDto GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue).MapTo<UserLogOnDto>();
        }
        public void UpdateForm(UserLogOnDto userLogOnInputDto)
        {
            var model = userLogOnInputDto.MapTo<Sys_UserLogOn>();
            _repository.Update(model);
        }
        public void RevisePassword(string userPassword, string keyValue)
        {
            Sys_UserLogOn Sys_UserLogOn = new Sys_UserLogOn();
            Sys_UserLogOn.F_Id = keyValue;
            Sys_UserLogOn.F_UserSecretkey = Encryptor.Md5Encryptor16(Common.CreateNo()).ToLower();
            Sys_UserLogOn.F_UserPassword = Encryptor.Md5Encryptor32(Encryptor.DesEncrypt(Encryptor.Md5Encryptor32(userPassword).ToLower(), Sys_UserLogOn.F_UserSecretkey).ToLower()).ToLower();
            _repository.Update(Sys_UserLogOn);
        }
    }
}

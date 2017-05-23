using BH.Code;
using BH.Data;
using BH.Domain.Entity.SystemManage;
using BH.IApplication;

namespace BH.Application.SystemManage
{
    public class UserLogOnApp : IUserLogOnApp
    {
        private readonly IRepository<UserLogOnEntity> _repository;
        public UserLogOnApp(IRepository<UserLogOnEntity> repository)
        {
            this._repository = repository;
        }
        public UserLogOnEntity GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue);
        }
        public void UpdateForm(UserLogOnEntity userLogOnEntity)
        {
            _repository.Update(userLogOnEntity);
        }
        public void RevisePassword(string userPassword,string keyValue)
        {
            UserLogOnEntity userLogOnEntity = new UserLogOnEntity();
            userLogOnEntity.F_Id = keyValue;
            userLogOnEntity.F_UserSecretkey = Md5.md5(Common.CreateNo(), 16).ToLower();
            userLogOnEntity.F_UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userPassword, 32).ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
            _repository.Update(userLogOnEntity);
        }
    }
}

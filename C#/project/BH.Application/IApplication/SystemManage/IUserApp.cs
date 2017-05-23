using System.Collections.Generic;
using BH.Code;
using BH.Domain.Entity.SystemManage;

namespace BH.IApplication
{
    public interface IUserApp
    {
        UserEntity CheckLogin(string username, string password);
        void DeleteForm(string keyValue);
        UserEntity GetForm(string keyValue);
        List<UserEntity> GetList(Pagination pagination, string keyword);
        void SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue);
        void UpdateForm(UserEntity userEntity);
    }
}
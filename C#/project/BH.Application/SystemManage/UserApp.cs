using BH.Application.Dto;
using BH.Code;
using BH.Data;
using BH.Domain.Entity;
using BH.IApplication;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace BH.Application.SystemManage
{
    public class UserApp : IUserApp
    {
        private readonly IRepository<Sys_User> _repository;
        private readonly IRepository<Sys_UserLogOn> _userLogOnRepository;
        public UserApp(IRepository<Sys_User> repository, IRepository<Sys_UserLogOn> Sys_UserLogOnRepository)
        {
            _repository = repository;
            _userLogOnRepository = Sys_UserLogOnRepository;
        }

        public async Task<List<UserDto>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<Sys_User>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Account.Contains(keyword));
                expression = expression.Or(t => t.F_RealName.Contains(keyword));
                expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
            }
            expression = expression.And(t => t.F_Account != "admin");
            return (await _repository.FindListAsync(expression, pagination)).MapToList<UserDto>();
        }
        public async Task<UserDto> GetForm(string keyValue)
        {
            return (await _repository.FindKeyAsync(keyValue)).MapTo<UserDto>();
        }
        public async Task<int> DeleteForm(string keyValue)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _repository.DeleteAsync(d => d.F_Id == keyValue);
                await _userLogOnRepository.DeleteAsync(d => d.F_Id == keyValue);
                scope.Complete();
                return 1;
            }
        }
        public async Task<int> SubmitForm(UserDto userInputDto, UserLogOnDto userLogOnInputDto, string keyValue)
        {
            var userModel = userInputDto.MapTo<Sys_User>();
            var userLogOnModel = userLogOnInputDto.MapTo<Sys_UserLogOn>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                userModel.Modify(keyValue);
            }
            else
            {
                userModel.Create();
            }

            if (!string.IsNullOrEmpty(keyValue))
            {
                await _repository.UpdateAsync(userModel);
            }
            else
            {
                userLogOnModel.F_Id = userModel.F_Id;
                userLogOnModel.F_UserId = userModel.F_Id;
                userLogOnModel.F_UserSecretkey = Encryptor.Md5Encryptor16(Common.CreateNo()).ToLower();
                //密码逻辑，  密码明文md5加密 => 根据生成的令牌Des加密md5加密过的密码 => 再次md5加密
                userLogOnModel.F_UserPassword = Encryptor.Md5Encryptor32(Encryptor.DesEncrypt(
                    Encryptor.Md5Encryptor32(userLogOnModel.F_UserPassword).ToLower(), userLogOnModel.F_UserSecretkey).ToLower()).ToLower();
                await _repository.InsertAsync(userModel);
                await _userLogOnRepository.InsertAsync(userLogOnModel);
            }
            return await _repository.SaveChangesAsync();
        }
        public async Task<int> UpdateForm(UserDto userInputDto)
        {
            var model = userInputDto.MapTo<Sys_User>();
            await _repository.UpdateAsync(model);
            return 1;
        }
        public async Task<UserDto> CheckLogin(string username, string password)
        {
            Sys_User Sys_User = await _repository.FindEntityAsync(t => t.F_Account == username);
            if (Sys_User != null)
            {
                if (Sys_User.F_EnabledMark == true)
                {
                    Sys_UserLogOn Sys_UserLogOn = _userLogOnRepository.FindKey(Sys_User.F_Id);
                    string dbPassword = Encryptor.Md5Encryptor32(Encryptor.DesEncrypt(password, Sys_UserLogOn.F_UserSecretkey).ToLower()).ToLower();
                    if (dbPassword == Sys_UserLogOn.F_UserPassword)
                    {
                        DateTime lastVisitTime = DateTime.Now;
                        int LogOnCount = Sys_UserLogOn.F_LogOnCount.ToInt() + 1;
                        if (Sys_UserLogOn.F_LastVisitTime != null)
                        {
                            Sys_UserLogOn.F_PreviousVisitTime = Sys_UserLogOn.F_LastVisitTime.ToDate();
                        }
                        Sys_UserLogOn.F_LastVisitTime = lastVisitTime;
                        Sys_UserLogOn.F_LogOnCount = LogOnCount;
                        await _userLogOnRepository.UpdateAsync(Sys_UserLogOn);
                        return Sys_User.MapTo<UserDto>();
                    }
                    else
                    {
                        throw new Exception("密码不正确，请重新输入");
                    }
                }
                else
                {
                    throw new Exception("账户被系统锁定,请联系管理员");
                }
            }
            else
            {
                throw new Exception("账户不存在，请重新输入");
            }
        }
    }
}

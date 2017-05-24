using BH.Code;
using BH.Data;
using BH.Domain.Entity;
using BH.IApplication;
using System;
using System.Collections.Generic;

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

        public List<Sys_User> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<Sys_User>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Account.Contains(keyword));
                expression = expression.Or(t => t.F_RealName.Contains(keyword));
                expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
            }
            expression = expression.And(t => t.F_Account != "admin");
            return _repository.FindList(expression, pagination);
        }
        public Sys_User GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            _repository.LazySaveChanges();
            _userLogOnRepository.LazySaveChanges();
            _repository.Delete(t => t.F_Id == keyValue);
            _userLogOnRepository.Delete(t => t.F_UserId == keyValue);
            _repository.SaveChanges(true);
        }
        public void SubmitForm(Sys_User Sys_User, Sys_UserLogOn Sys_UserLogOn, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                Sys_User.Modify(keyValue);
            }
            else
            {
                Sys_User.Create();
            }

            _repository.LazySaveChanges();
            _userLogOnRepository.LazySaveChanges();
            if (!string.IsNullOrEmpty(keyValue))
            {
                _repository.Update(Sys_User);
            }
            else
            {
                Sys_UserLogOn.F_Id = Sys_User.F_Id;
                Sys_UserLogOn.F_UserId = Sys_User.F_Id;
                Sys_UserLogOn.F_UserSecretkey = Md5.md5(Common.CreateNo(), 16).ToLower();
                Sys_UserLogOn.F_UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(Sys_UserLogOn.F_UserPassword, 32).ToLower(), Sys_UserLogOn.F_UserSecretkey).ToLower(), 32).ToLower();
                _repository.Insert(Sys_User);
                _userLogOnRepository.Insert(Sys_UserLogOn);
            }
            _repository.SaveChanges(true);
        }
        public void UpdateForm(Sys_User Sys_User)
        {
            _repository.Update(Sys_User);
        }
        public Sys_User CheckLogin(string username, string password)
        {
            Sys_User Sys_User = _repository.FindEntity(t => t.F_Account == username);
            if (Sys_User != null)
            {
                if (Sys_User.F_EnabledMark == true)
                {
                    Sys_UserLogOn Sys_UserLogOn = _userLogOnRepository.FindKey(Sys_User.F_Id);
                    string dbPassword = Md5.md5(DESEncrypt.Encrypt(password.ToLower(), Sys_UserLogOn.F_UserSecretkey).ToLower(), 32).ToLower();
                    if (dbPassword == Sys_UserLogOn.F_UserPassword)
                    {
                        DateTime lastVisitTime = DateTime.Now;
                        int LogOnCount = (Sys_UserLogOn.F_LogOnCount).ToInt() + 1;
                        if (Sys_UserLogOn.F_LastVisitTime != null)
                        {
                            Sys_UserLogOn.F_PreviousVisitTime = Sys_UserLogOn.F_LastVisitTime.ToDate();
                        }
                        Sys_UserLogOn.F_LastVisitTime = lastVisitTime;
                        Sys_UserLogOn.F_LogOnCount = LogOnCount;
                        _userLogOnRepository.Update(Sys_UserLogOn);
                        return Sys_User;
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

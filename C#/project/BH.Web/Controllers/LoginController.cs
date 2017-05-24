using System;
using System.Web.Mvc;
using BH.Code;
using BH.IApplication;
using BH.Application;
using BH.Domain.Entity;

namespace BH.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogApp _logApp;
        private readonly IUserApp _userApp;

        public LoginController(ILogApp logApp, IUserApp userApp)
        {
            this._logApp = logApp;
            this._userApp = userApp;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            var test = string.Format("{0:E2}", 1);
            return View();
        }
        [HttpGet]
        public ActionResult GetAuthCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }
        [HttpGet]
        public ActionResult OutLogin()
        {
            _logApp.WriteDbLog(new Sys_Log
            {
                F_ModuleName = "系统登录",
                F_Type = DbLogType.Exit.ToString(),
                F_Account = OperatorProvider.Provider.GetCurrent().UserCode,
                F_NickName = OperatorProvider.Provider.GetCurrent().UserName,
                F_Result = true,
                F_Description = "安全退出系统",
            });
            Session.Abandon();
            Session.Clear();
            OperatorProvider.Provider.RemoveCurrent();
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult CheckLogin(string username, string password, string code)
        {
            Sys_Log Sys_Log = new Sys_Log();
            Sys_Log.F_ModuleName = "系统登录";
            Sys_Log.F_Type = DbLogType.Login.ToString();
            try
            {
                if (Session["BH_session_verifycode"].IsEmpty() || Md5.md5(code.ToLower(), 16) != Session["BH_session_verifycode"].ToString())
                {
                    throw new Exception("验证码错误，请重新输入");
                }

                Sys_User Sys_User = _userApp.CheckLogin(username, password);
                if (Sys_User != null)
                {
                    OperatorModel operatorModel = new OperatorModel();
                    operatorModel.UserId = Sys_User.F_Id;
                    operatorModel.UserCode = Sys_User.F_Account;
                    operatorModel.UserName = Sys_User.F_RealName;
                    operatorModel.CompanyId = Sys_User.F_OrganizeId;
                    operatorModel.DepartmentId = Sys_User.F_DepartmentId;
                    operatorModel.RoleId = Sys_User.F_RoleId;
                    operatorModel.LoginIPAddress = Net.Ip;
                    operatorModel.LoginIPAddressName = Net.GetLocation(operatorModel.LoginIPAddress);
                    operatorModel.LoginTime = DateTime.Now;
                    operatorModel.LoginToken = DESEncrypt.Encrypt(Guid.NewGuid().ToString());
                    if (Sys_User.F_Account == "admin")
                    {
                        operatorModel.IsSystem = true;
                    }
                    else
                    {
                        operatorModel.IsSystem = false;
                    }
                    OperatorProvider.Provider.AddCurrent(operatorModel);
                    Sys_Log.F_Account = Sys_User.F_Account;
                    Sys_Log.F_NickName = Sys_User.F_RealName;
                    Sys_Log.F_Result = true;
                    Sys_Log.F_Description = "登录成功";
                    _logApp.WriteDbLog(Sys_Log);
                }
                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
            }
            catch (Exception ex)
            {
                Sys_Log.F_Account = username;
                Sys_Log.F_NickName = username;
                Sys_Log.F_Result = false;
                Sys_Log.F_Description = "登录失败，" + ex.Message;
                _logApp.WriteDbLog(Sys_Log);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }
    }
}

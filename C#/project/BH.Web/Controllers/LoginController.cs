using System;
using System.Web.Mvc;
using BH.Code;
using BH.IApplication;
using BH.Application;
using BH.Domain.Entity;
using BH.Code.Autofac;
using BH.Application.Dto;
using System.Threading.Tasks;

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
            return View();
        }
        [HttpGet]
        public ActionResult GetAuthCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }
        [HttpGet]
        public async Task<ActionResult> OutLogin()
        {
            await _logApp.WriteDbLog(new LogDto
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
        public async Task<ActionResult> CheckLogin(string username, string password, string code)
        {
            var logInputDto = new LogDto();
            logInputDto.F_ModuleName = "系统登录";
            logInputDto.F_Type = DbLogType.Login.ToString();
            try
            {
                if (Session["BH_session_verifycode"].IsEmpty() || Encryptor.Md5Encryptor16(code.ToLower()) != Session["BH_session_verifycode"].ToString())
                {
                    throw new Exception("验证码错误，请重新输入");
                }

                var userDto = await _userApp.CheckLogin(username, password);
                if (userDto != null)
                {
                    OperatorModel operatorModel = new OperatorModel();
                    operatorModel.UserId = userDto.F_Id;
                    operatorModel.UserCode = userDto.F_Account;
                    operatorModel.UserName = userDto.F_RealName;
                    operatorModel.CompanyId = userDto.F_OrganizeId;
                    operatorModel.DepartmentId = userDto.F_DepartmentId;
                    operatorModel.RoleId = userDto.F_RoleId;
                    operatorModel.LoginIPAddress = Net.Ip;
                    operatorModel.LoginIPAddressName = Net.GetLocation(operatorModel.LoginIPAddress);
                    operatorModel.LoginTime = DateTime.Now;
                    operatorModel.LoginToken = Encryptor.DesEncrypt(Guid.NewGuid().ToString());
                    if (userDto.F_Account == "admin")
                    {
                        operatorModel.IsSystem = true;
                    }
                    else
                    {
                        operatorModel.IsSystem = false;
                    }
                    OperatorProvider.Provider.AddCurrent(operatorModel);
                    logInputDto.F_Account = userDto.F_Account;
                    logInputDto.F_NickName = userDto.F_RealName;
                    logInputDto.F_Result = true;
                    logInputDto.F_Description = "登录成功";
                    await _logApp.WriteDbLog(logInputDto);
                }
                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
            }
            catch (Exception ex)
            {
                logInputDto.F_Account = username;
                logInputDto.F_NickName = username;
                logInputDto.F_Result = false;
                logInputDto.F_Description = "登录失败，" + ex.Message;
                await _logApp.WriteDbLog(logInputDto);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }
    }
}

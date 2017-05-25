using BH.Application.Dto;
using BH.Application.SystemManage;
using BH.Code;
using BH.Domain.Entity;
using BH.IApplication;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace BH.Web.Areas.SystemManage.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserApp _userApp;
        private readonly IUserLogOnApp _userLogOnApp;
        public UserController(IUserApp userApp, IUserLogOnApp userLogOnApp)
        {
            _userApp = userApp;
            _userLogOnApp = userLogOnApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _userApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetFormJson(string keyValue)
        {
            var data = await _userApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubmitForm(UserDto userInputDto, UserLogOnDto userLogOnInputDto, string keyValue)
        {
            await _userApp.SubmitForm(userInputDto, userLogOnInputDto, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteForm(string keyValue)
        {
            await _userApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        [HttpGet]
        public ActionResult RevisePassword()
        {
            return View();
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitRevisePassword(string userPassword, string keyValue)
        {
            _userLogOnApp.RevisePassword(userPassword, keyValue);
            return Success("重置密码成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisabledAccount(string keyValue)
        {
            var user = new UserDto
            {
                F_Id = keyValue,
                F_EnabledMark = false
            };
            await _userApp.UpdateForm(user);
            return Success("账户禁用成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnabledAccount(string keyValue)
        {
            var user = new UserDto
            {
                F_Id = keyValue,
                F_EnabledMark = true
            };
            await _userApp.UpdateForm(user);
            return Success("账户启用成功。");
        }

        [HttpGet]
        public ActionResult Info()
        {
            return View();
        }
    }
}

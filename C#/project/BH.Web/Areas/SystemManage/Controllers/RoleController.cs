using BH.Code;
using BH.Domain.Entity;
using BH.IApplication;
using System.Linq;
using System.Web.Mvc;

namespace BH.Web.Areas.SystemManage.Controllers
{
    public class RoleController : ControllerBase
    {
        private readonly IRoleApp roleApp;
        private readonly IRoleAuthorizeApp roleAuthorizeApp;
        private readonly IModuleApp moduleApp;
        private readonly IModuleButtonApp moduleButtonApp;
        public RoleController(IRoleApp roleApp, IRoleAuthorizeApp roleAuthorizeApp,
            IModuleApp moduleApp, IModuleButtonApp moduleButtonApp)
        {
            this.roleApp = roleApp;
            this.roleAuthorizeApp = roleAuthorizeApp;
            this.moduleApp = moduleApp;
            this.moduleButtonApp = moduleButtonApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string keyword)
        {
            var data = roleApp.GetList(keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = roleApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Sys_Role Sys_Role, string permissionIds, string keyValue)
        {
            roleApp.SubmitForm(Sys_Role, permissionIds.Split(','), keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            roleApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}

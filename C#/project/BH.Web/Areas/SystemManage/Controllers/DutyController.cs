/*******************************************************************************
 * Copyright © 2016 BH.Framework 版权所有
 * Author: BH
 * Description: BH快速开发平台
 * Website：http://www.BH.cn
*********************************************************************************/
using BH.Application.SystemManage;
using BH.Code;
using BH.Domain.Entity;
using BH.IApplication;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BH.Web.Areas.SystemManage.Controllers
{
    public class DutyController : ControllerBase
    {
        private readonly IDutyApp _dutyApp;
        public DutyController(IDutyApp dutyApp)
        {
            _dutyApp = dutyApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string keyword)
        {
            var data = _dutyApp.GetList(keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = _dutyApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Sys_Role Sys_Role, string keyValue)
        {
            _dutyApp.SubmitForm(Sys_Role, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            _dutyApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}

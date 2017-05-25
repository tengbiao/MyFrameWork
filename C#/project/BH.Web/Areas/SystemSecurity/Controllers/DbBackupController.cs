using BH.Application.Dto;
using BH.Application.SystemSecurity;
using BH.Code;
using BH.Domain.Entity;
using BH.IApplication;
using System.Web.Mvc;

namespace BH.Web.Areas.SystemSecurity.Controllers
{
    public class DbBackupController : ControllerBase
    {
        private readonly IDbBackupApp _dbBackupApp;
        public DbBackupController(IDbBackupApp dbBackupApp)
        {
            _dbBackupApp = dbBackupApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string queryJson)
        {
            var data = _dbBackupApp.GetList(queryJson);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(DbBackupDto dbBackupInputDto)
        {
            dbBackupInputDto.F_FilePath = Server.MapPath("~/Resource/DbBackup/" + dbBackupInputDto.F_FileName + ".bak");
            dbBackupInputDto.F_FileName = dbBackupInputDto.F_FileName + ".bak";
            _dbBackupApp.SubmitForm(dbBackupInputDto);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            _dbBackupApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        [HttpPost]
        [HandlerAuthorize]
        public void DownloadBackup(string keyValue)
        {
            var data = _dbBackupApp.GetForm(keyValue);
            string filename = Server.UrlDecode(data.F_FileName);
            string filepath = Server.MapPath(data.F_FilePath);
            if (FileDownHelper.FileExists(filepath))
            {
                FileDownHelper.DownLoadold(filepath, filename);
            }
        }
    }
}

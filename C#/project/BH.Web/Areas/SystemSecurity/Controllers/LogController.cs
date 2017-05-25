using BH.Code;
using BH.IApplication;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BH.Web.Areas.SystemSecurity.Controllers
{
    public class LogController : ControllerBase
    {
        private readonly ILogApp logApp;
        public LogController(ILogApp logApp)
        {
            this.logApp = logApp;
        }

        [HttpGet]
        public ActionResult RemoveLog()
        {
            return View();
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = await logApp.GetList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubmitRemoveLog(string keepTime)
        {
            await logApp.RemoveLog(keepTime);
            return Success("清空成功。");
        }
    }
}

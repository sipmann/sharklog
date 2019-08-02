using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sharklog.Models;
using sharklog.Services;

namespace sharklog.Controllers
{
    public class LogController : Controller
    {
        private ILogService service;

        public LogController(ILogService logService)
        {
            this.service = logService;
        }

        [HttpGet("/log/{appname}")]
        public IActionResult Index(string appname, [FromQueryAttribute] string token = "")
        {
            var logs = this.service.GetLogs(appname, token);

            if (HttpContext.Request.IsAjaxRequest()) 
            {
                return Json(logs);
            }

            ViewData["AppName"] = appname;
            ViewData["AppToken"] = token;
            return View(logs);
        }

        [HttpPost("/log/{appname}")]
        public async Task<IActionResult> Post(string appname, [FromBody] LogDto log)
        {
            await this.service.AddLog(appname, log);
            return Ok();
        }

        [HttpGet("/log/detail/{logid}")]
        public IActionResult Detail(string logid, [FromQueryAttribute] string token = "")
        {
            var log = this.service.Get(logid, token);
            ViewData["AppToken"] = token;
            return View(log);
        }
    }
}

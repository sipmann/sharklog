using System.Diagnostics;
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
        public IActionResult Index(string appname)
        {
            var logs = this.service.GetLogs(appname, "");

            if (HttpContext.Request.IsAjaxRequest()) 
            {
                return Json(logs);
            }

            return View(logs);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost("/log/{appname}")]
        public IActionResult Post(string appname, [FromBody] LogModel log)
        {
            this.service.AddLog(appname, log, "");
            return Ok();
        }
    }
}

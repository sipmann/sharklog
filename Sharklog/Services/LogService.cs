using System;
using System.Collections.Generic;
using System.Linq;
using sharklog.Models;

namespace sharklog.Services
{
    public class LogService : ILogService
    {

        private SharkContext _context;
        private IAppService _appService;

        public LogService(SharkContext context, IAppService appService)
        {
            this._appService = appService;
            this._context = context;
        }

        public List<LogModel> GetLogs(string appname, String token = "")
        {
            var app = this._appService.GetOrCreateApp(appname, token);
            return this.GetLogs(app);
        }

        public List<LogModel> GetLogs(ApplicationModel app)
        {
            var logs = this._context.Logs.Where(l => l.Application.Name == app.Name).ToList();
            return logs;
        }

        public List<LogModel> AddLog(string appname, LogModel log, String token = "")
        {
            var uuid = Guid.NewGuid().ToString();
            
            var app = this._appService.GetOrCreateApp(appname, token);
            var logs = this.GetLogs(app);
            
            if (string.IsNullOrEmpty(log.LogType))
            {
                log.LogType = "bug";
            }

            log.Id = uuid;
            log.LogDate = DateTime.Now;
            log.Application = app;

            this._context.Logs.Add(log);
            this._context.SaveChanges();

            this._appService.UpdateLast(app);

            logs.Add(log);

            return logs;
        }

    }
}
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
            var app = this._appService.Get(appname, token);

            if (app == null)
            {
                // TODO: application exception
                throw new ApplicationException("Application not found");
            }

            return this.GetLogs(app);
        }

        public List<LogModel> GetLogs(ApplicationModel app)
        {
            var logs = this._context.Logs
                .Where(l => l.Application.Name == app.Name)
                .OrderByDescending(l => l.LogDate)
                .ToList();
            return logs;
        }

        public List<LogModel> AddLog(string appname, LogDto logDto)
        {
            var uuid = Guid.NewGuid().ToString();
            
            var app = this._appService.GetOrCreateApp(appname, logDto.Token);
            var logs = this.GetLogs(app);
            var log = new LogModel()
            {
                Title = logDto.Title,
                Body = logDto.Body,
                LogType = logDto.LogType
            };
            
            if (string.IsNullOrEmpty(log.LogType))
            {
                log.LogType = "bug";
            }

            log.Id = uuid;
            log.LogDate = DateTime.Now;
            log.Application = app;

            this._context.Logs.Add(log);
            this._context.SaveChanges();
            logs.Add(log);

            return logs;
        }

    }
}
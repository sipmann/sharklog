using System.Collections.Generic;
using sharklog.Models;

namespace sharklog.Services
{
    public interface ILogService
    {

        List<LogModel> GetLogs(string appname, string token = "");

        List<LogModel> GetLogs(ApplicationModel app);

        List<LogModel> AddLog(string appname, LogDto log);

    }
}
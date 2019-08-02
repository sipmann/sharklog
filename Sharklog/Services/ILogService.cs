using System.Collections.Generic;
using System.Threading.Tasks;
using sharklog.Models;

namespace sharklog.Services
{
    public interface ILogService
    {

        List<LogModel> GetLogs(string appname, string token = "");

        List<LogModel> GetLogs(ApplicationModel app);

        Task<LogModel> AddLog(string appname, LogDto log);
        
        LogModel Get(string logid, string token = "");
    }
}
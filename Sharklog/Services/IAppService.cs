using sharklog.Models;

namespace sharklog.Services
{
    public interface IAppService
    {

        ApplicationModel GetOrCreateApp(string appname, string token = "");

        ApplicationModel AddApp(string appname, string token = "");
        ApplicationModel Get(string appname, string token = "");
    }
}
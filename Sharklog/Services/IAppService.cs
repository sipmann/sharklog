using System.Threading.Tasks;
using sharklog.Models;

namespace sharklog.Services
{
    public interface IAppService
    {

        Task<ApplicationModel> GetOrCreateApp(string appname, string token = "");

        Task<ApplicationModel> AddApp(string appname, string token = "");
        ApplicationModel Get(string appname, string token = "");
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using sharklog.Models;

namespace sharklog.Services
{
    public class AppService : IAppService
    {
        private SharkContext _context;

        public AppService(SharkContext context)
        {
            this._context = context;
        }

        public async Task<ApplicationModel> GetOrCreateApp(string appname, string token = "")
        {
            var app = this.Get(appname, token);

            if (app == null)
            {
                app = await this.AddApp(appname, token);
            }

            return app;
        }

        public async Task<ApplicationModel> AddApp(string appname, string token = "")
        {
            var app = new ApplicationModel()
            {
                Name = appname,
                Token = token
            };

            await this._context.Applications.AddAsync(app);
            await this._context.SaveChangesAsync();

            return app;
        }

        public ApplicationModel Get(string appname, string token = "")
        {
            var app = this._context.Applications.Where(a => a.Name == appname).FirstOrDefault();

            if (app != null && !String.IsNullOrEmpty(app.Token) && app.Token != token)
            {
                throw new ApplicationException("Unauthorized");
            }

            return app;
        }

    }
}
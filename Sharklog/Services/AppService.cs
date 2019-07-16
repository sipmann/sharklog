using System;
using System.Linq;
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

        public ApplicationModel GetOrCreateApp(string appname, string token = "")
        {
            var app = this.Get(appname, token);

            if (app == null)
            {
                app = this.AddApp(appname, token);
            }

            return app;
        }

        public ApplicationModel AddApp(string appname, string token = "")
        {
            var app = new ApplicationModel()
            {
                Name = appname,
                Token = token
            };

            this._context.Applications.Add(app);
            this._context.SaveChanges();

            return app;
        }


        public ApplicationModel Get(string appname, string token = "")
        {
            // TODO: validate token
            return this._context.Applications.Where(a => a.Name == appname).FirstOrDefault();
        }

    }
}
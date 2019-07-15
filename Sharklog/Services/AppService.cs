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
            // TODO: Check for token
            var app = this._context.Applications.Where(a => a.Name == appname).FirstOrDefault();

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

        public void UpdateLast(ApplicationModel app)
        {
            app.LastUpdate = DateTime.Now;
            this._context.Update(app);
            this._context.SaveChanges();
        }

    }
}
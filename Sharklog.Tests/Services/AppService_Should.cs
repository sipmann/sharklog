/*
    Learn how to do a better test with qts
 */
using Microsoft.EntityFrameworkCore;
using sharklog.Models;
using sharklog.Services;
using Xunit;
using System.Threading.Tasks;
using System;

namespace Tests.Services
{
    public class AppService_Should
    {
        private DbContextOptions<SharkContext> options;

        public AppService_Should()
        {
            this.options = new DbContextOptionsBuilder<SharkContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;
        }

        [Fact]
        public async Task AddAppShouldCreatApp()
        {
            using (var context = new SharkContext(options))
            {
                var qtd = await context.Applications.CountAsync();
                var service = new AppService(context);
                service.AddApp("SharkLog");
                Assert.Equal(qtd + 1, await context.Applications.CountAsync());
            }
        }

        [Fact]
        public async Task GetOrCreateShouldCreatAppWhenDoesntExists()
        {
            var appName = "SharkLog2";

            using (var context = new SharkContext(options))
            {
                var service = new AppService(context);
                var qtd = await context.Applications.CountAsync();
                
                service.GetOrCreateApp(appName);
                Assert.Equal(qtd + 1, await context.Applications.CountAsync());
            }
        }

        [Fact]
        public async Task GetOrCreateShouldNotCreatAppWhenAppExists()
        {
            var appName = "SharkLog3";

            using (var context = new SharkContext(options))
            {
                var service = new AppService(context);
                
                // force creation
                service.GetOrCreateApp(appName);
                var qtd = await context.Applications.CountAsync();
                
                service.GetOrCreateApp(appName);
                Assert.Equal(qtd, await context.Applications.CountAsync());
            }
        }
    
        [Fact]
        public void GetShouldReturnAppWhenAppExists()
        {
            var appName = "SharkLog4";

            using (var context = new SharkContext(options))
            {
                var service = new AppService(context);
                
                // force creation
                service.GetOrCreateApp(appName);
                
                var app = service.Get(appName);
                Assert.NotNull(app);
            }
        }

        [Fact]
        public void GetShouldReturnNullWhenNotExists()
        {
            var appName = "SharkLog5";
            
            using (var context = new SharkContext(options))
            {
                var service = new AppService(context);
                
                var app = service.Get(appName);
                Assert.Null(app);
            }
        }

        [Fact]
        public void GetShouldRaiseWhenWrogToken()
        {
            var appName = "SharkLog6";
            
            using (var context = new SharkContext(options))
            {
                var service = new AppService(context);
                var app = new ApplicationModel()
                {
                    Name = appName,
                    Token = "abc123"
                };
                context.Applications.Add(app);
                context.SaveChanges();
                
                Assert.Throws<ApplicationException>(() => service.Get(app.Name, "123"));
            }
        }

        [Fact]
        public void GetShouldRaiseWhenEmptyTokenButExpectedOne()
        {
            var appName = "SharkLog7";
            
            using (var context = new SharkContext(options))
            {
                var service = new AppService(context);
                var app = new ApplicationModel()
                {
                    Name = appName,
                    Token = "abc123"
                };
                context.Applications.Add(app);
                context.SaveChanges();
                
                Assert.Throws<ApplicationException>(() => service.Get(app.Name));
            }
        }

        [Fact]
        public void GetShouldReturnAppWhenRightToken()
        {
            var appName = "SharkLog8";
            
            using (var context = new SharkContext(options))
            {
                var service = new AppService(context);
                var app = new ApplicationModel()
                {
                    Name = appName,
                    Token = "abc123"
                };
                context.Applications.Add(app);
                context.SaveChanges();
                
                var appRet = service.Get(app.Name, app.Token);
                Assert.NotNull(appRet);
                Assert.Equal(app, appRet);
            }
        }

    }
}
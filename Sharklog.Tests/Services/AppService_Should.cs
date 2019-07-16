/*
    Learn how to do a better test with qts
 */
using Microsoft.EntityFrameworkCore;
using sharklog.Models;
using sharklog.Services;
using Xunit;
using System.Threading.Tasks;

namespace Tests.Services
{
    public class AppService_Should
    {
        [Fact]
        public async Task AddAppShouldCreatApp()
        {
            var options = new DbContextOptionsBuilder<SharkContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

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
            var options = new DbContextOptionsBuilder<SharkContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

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
            var options = new DbContextOptionsBuilder<SharkContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

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
    }
}
using System;
using Microsoft.EntityFrameworkCore;
using sharklog.Models;
using sharklog.Services;
using Xunit;

namespace Tests.Services
{
    public class LogService_Should
    {
        private DbContextOptions<SharkContext> options;

        public LogService_Should()
        {
            this.options = new DbContextOptionsBuilder<SharkContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;
        }

        [Fact]
        public void GetLogsGivinAppNameShouldReturnWhenExists()
        {
            using(var context = new SharkContext(options))
            {
                var appService = new AppService(context);
                var app = new ApplicationModel(){
                    Name = "abc"
                };
                context.Applications.Add(app);
                context.SaveChanges();
                //context.Logs.Add()

                var service = new LogService(context, appService);
                var logs = service.GetLogs(app.Name);

                Assert.NotEqual(logs, null);
            }
        }

        [Fact]
        public void GetLogsGivinAppNameShouldThrowWhenNotExists()
        {
            using(var context = new SharkContext(options))
            {
                var appService = new AppService(context);
                var service = new LogService(context, appService);
                Assert.Throws<ApplicationException>(() => service.GetLogs("AbcUniqueNotExists"));
            }
        }

        [Fact]
        public void GetLogsGivinAppShouldReturnLogs()
        {
            using(var context = new SharkContext(options))
            {
                var appService = new AppService(context);
                var app = new ApplicationModel(){
                    Name = "abc"
                };
                context.Applications.Add(app);
                context.SaveChanges();

                var service = new LogService(context, appService);
                var logs = service.GetLogs(app);

                Assert.NotEqual(logs, null);
                Assert.Empty(logs);
            }
        }

        [Fact]
        public void AddLogShouldAddWhenAppNotExists()
        {
            using(var context = new SharkContext(options))
            {
                var appService = new AppService(context);
                var log = new LogDto()
                {
                    Title = "Log Test",
                    LogType = "bug"
                };

                var service = new LogService(context, appService);
                var logs = service.AddLog("AbcUnique", log);

                Assert.NotEqual(logs, null);
                Assert.NotEmpty(logs);
            }
        }
        
        [Fact]
        public void AddLogShouldAddWhenAppExists()
        {
            using(var context = new SharkContext(options))
            {
                var appService = new AppService(context);
                var app = new ApplicationModel();
                app.Name = "abc";

                context.Applications.Add(app);
                context.SaveChanges();

                var log = new LogDto()
                {
                    Title = "Log Test",
                    LogType = "bug"
                };

                var service = new LogService(context, appService);
                var logs = service.AddLog(app.Name, log);

                Assert.NotEqual(logs, null);
                Assert.NotEmpty(logs);
            }
        }

        [Fact]
        public void AddLogShouldAddWhenAppExistsWithRightToken()
        {
            using(var context = new SharkContext(options))
            {
                var appService = new AppService(context);
                var app = new ApplicationModel();
                app.Name = "abc-unique-with-token";
                app.Token = "abc123";

                context.Applications.Add(app);
                context.SaveChanges();

                var log = new LogDto()
                {
                    Title = "Log Test",
                    LogType = "bug",
                    Token = app.Token
                };

                var service = new LogService(context, appService);
                var logs = service.AddLog(app.Name, log);

                Assert.NotNull(logs);
                Assert.NotEmpty(logs);
            }
        }

        [Fact]
        public void AddLogShouldAddWhenAppExistsWithWrongToken()
        {
            using(var context = new SharkContext(options))
            {
                var appService = new AppService(context);
                var app = new ApplicationModel();
                app.Name = "abc-unique-with-token";
                app.Token = "abc123";

                context.Applications.Add(app);
                context.SaveChanges();

                var log = new LogDto()
                {
                    Title = "Log Test",
                    LogType = "bug",
                    Token = "abc"
                };

                var service = new LogService(context, appService);
                Assert.Throws<ApplicationException>(() => service.AddLog(app.Name, log));
            }
        }

        public void AddLogShouldAddWhenAppExistsWithEmptyTokenButExpected()
        {
            using(var context = new SharkContext(options))
            {
                var appService = new AppService(context);
                var app = new ApplicationModel();
                app.Name = "abc-unique-with-token";
                app.Token = "abc123";

                context.Applications.Add(app);
                context.SaveChanges();

                var log = new LogDto()
                {
                    Title = "Log Test",
                    LogType = "bug"
                };

                var service = new LogService(context, appService);
                Assert.Throws<ApplicationException>(() => service.AddLog(app.Name, log));
            }
        }
    }
}
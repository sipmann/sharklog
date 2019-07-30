using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using sharklog.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace sharklog.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
    
        public ExceptionMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this._next = next;
            this._configuration= configuration;
        }
    
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                SendLog(ex);

                if (httpContext.Request.IsAjaxRequest()) 
                {
                    await HandleExceptionAsync(httpContext, ex);
                } else
                {
                    httpContext.Response.Redirect("/Home/Error");
                }
            }
        }
    
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    
            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }

        private Task SendLog(Exception exception)
        {
            var client = new HttpClient();
            
            var data = JsonConvert.SerializeObject(new LogDto() 
            {
                Title = exception.Message,
                Body = exception.StackTrace.ToString(),
                Token = this._configuration.GetValue<string>("SharkToken")
            });
            return client.PostAsync("https://sharklog.sipmann.com/log/SharkLog", new StringContent(data, Encoding.UTF8, "application/json"));
        }
    }
}
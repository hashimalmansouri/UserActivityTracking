using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserActivityTracking.Data;

namespace UserActivityTracking.Filters
{
    public class UserFilterAttribute : IActionFilter
    {
        private readonly ApplicationDbContext context;

        public UserFilterAttribute(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string data = "";
            var routeData = context.RouteData;
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];
            var url = $"{controller}/{action}";
            if (!string.IsNullOrEmpty(context.HttpContext.Request.QueryString.Value))
            {
                data = context.HttpContext.Request.QueryString.Value;
            }
            else
            {
                var arguments = context.ActionArguments;
                var value = arguments.FirstOrDefault().Value;
                var convertedValue = JsonConvert.SerializeObject(value);
                data = convertedValue;
            }
            var userName = context.HttpContext.User.Identity.Name;
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            StoreUserActivity(data, url, userName, ipAddress);
        }

        public void StoreUserActivity(string data, string url, string userName, string ipAddress)
        {
            var userActivity = new UserActivity
            {
                Data = data,
                Url = url,
                UserName = userName,
                IpAddress = ipAddress
            };

            context.UserActivities.Add(userActivity);
            context.SaveChanges();
        }
    }
}

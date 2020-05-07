using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using shaker.crosscutting.Configuration;
using shaker.data;
using shaker.data.core;
using shaker.data.entity.Movements;
using shaker.data.entity.Planning;
using shaker.data.entity.Users;

namespace shaker
{
    public partial class Startup
    {
        public void CreateDefaultData(IServiceProvider services, UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("aze").Result == null)
            {
                User user = new User
                {
                    UserName = "aze",
                    Email = "aze@shaker.com"
                };
                _ = userManager.CreateAsync(user, "&aZ(erato123").Result;

                IUnitOfWork uow = services.GetService<IUnitOfWork>();

                CalendarEventType eventTypePR = new CalendarEventType()
                {
                    Title = "PR"
                };
                CalendarEventType eventTypeWeight = new CalendarEventType()
                {
                    Title = "Weight"
                };
                CalendarEventType eventTypeDefault = new CalendarEventType()
                {
                    Title = "Default"
                };

                uow.CalendarEventTypes.Add(eventTypePR);

                uow.CalendarEventTypes.Add(eventTypeWeight);

                uow.CalendarEvents.Add(new CalendarEvent()
                {
                    Title = "PR - Snatch 102,2kg",
                    AllDay = true,
                    Start = DateTime.Now,
                    User = user,
                    Type = eventTypePR
                });

                uow.CalendarEvents.Add(new CalendarEvent()
                {
                    Title = "Body mesure Weight - 81kg",
                    AllDay = false,
                    Start = DateTime.Now.AddHours(-1),
                    User = user,
                    Type = eventTypeWeight
                });

                uow.CalendarEvents.Add(new CalendarEvent()
                {
                    Title = "Web seminar - Mobility",
                    AllDay = true,
                    Start = DateTime.Now.AddDays(-5),
                    End = DateTime.Now.AddDays(+2),
                    User = user,
                    Type = eventTypeDefault
                });

                uow.CalendarEvents.Add(new CalendarEvent()
                {
                    Title = "Home Wod",
                    AllDay = false,
                    Start = DateTime.Now.AddDays(-2).AddHours(-3),
                    User = user
                });
            }
        }   
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using UnityPlatformerService.DataObjects;
using UnityPlatformerService.Models;
using Owin;

namespace UnityPlatformerService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new UnityPlatformerInitializer());

            // To prevent Entity Framework from modifying your database schema, use a null database initializer
            // Database.SetInitializer<unityplatformerContext>(null);

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // This middleware is intended to be used locally for debugging. By default, HostName will
                // only have a value when running in an App Service application.
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }
            app.UseWebApi(config);
        }
    }

    public class UnityPlatformerInitializer : CreateDatabaseIfNotExists<UnityPlatformerContext>
    {
        protected override void Seed(UnityPlatformerContext context)
        {
            List<Player> Players = new List<Player>
            {
                new Player { Id = Guid.NewGuid().ToString(), Name = "GYEEK", Coins = 2 },
                new Player { Id = Guid.NewGuid().ToString(), Name = "Mario", Coins = 30 },
            };

            foreach (var player in Players)
            {
                context.Set<Player>().Add(player);
            }

            base.Seed(context);
        }
    }
}


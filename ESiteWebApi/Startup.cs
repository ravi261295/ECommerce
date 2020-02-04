﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using ESiteWebApi.Authentication;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(ESiteWebApi.Startup))]

namespace ESiteWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Enable CORS (cross origin resource sharing) for making request using browser from different domains
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                //The Path For generating the Toekn
                TokenEndpointPath = new PathString("/token"),
                //Setting the Token Expired Time (24 hours)
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                //MyAuthorizationServerProvider class will validate the user credentials
                Provider = new MyAuthorizationServerProvider()
            };
            //Token Generations
            //app.UseOAuthBearerTokens(options);
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            
        }

        //public void Configuration(IAppBuilder app)
        //{

        //    ConfigureAuth(app);
        //    GlobalConfiguration.Configure(WebApiConfig.Register);
        //}
    }
}
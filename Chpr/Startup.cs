using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(Chpr.Startup))]

namespace Chpr
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      app.UseCors(CorsOptions.AllowAll);

      OAuthAuthorizationServerOptions option = new OAuthAuthorizationServerOptions
      {
        TokenEndpointPath = new PathString("/token"),
        Provider = new ApplicationOAuthProvider(),
        AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
        AllowInsecureHttp = true
      };
      app.UseOAuthAuthorizationServer(option);
      app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
    }
  }
}

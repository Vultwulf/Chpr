using Chpr.DataLayer;
using Chpr.DataLayer.Entities;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Chpr
{
  public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
  {
    public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
    {
      context.Validated();
    }

    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    {
      UsersService usersService = new UsersService();
      User user = await usersService.GetUser(context.UserName, context.Password);

      // If the user was found
      if (user != null)
      {
        var identity = new ClaimsIdentity(context.Options.AuthenticationType);
        identity.AddClaim(new Claim("Username", user.UserName));
        context.Validated(identity);
      }
      else
      {
        return;
      }
    }
  }
}

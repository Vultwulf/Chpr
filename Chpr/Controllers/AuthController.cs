using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using Chpr.DataLayer.Entities;

namespace Chpr.Controllers
{
  public class AuthController : ApiController
  {
    // GET: api/Auth
    [Authorize]
    public User Get()
    {
      var identityClaims = (ClaimsIdentity)User.Identity;
      IEnumerable<Claim> claims = identityClaims.Claims;
      User model = new User()
      {
        UserName = identityClaims.FindFirst("Username").Value
      };
      return model;
    }

    // GET: api/Auth
    [Authorize]
    public User Post()
    {
      var identityClaims = (ClaimsIdentity)User.Identity;
      IEnumerable<Claim> claims = identityClaims.Claims;
      User model = new User()
      {
        UserName = identityClaims.FindFirst("Username").Value
      };
      return model;
    }

  }
}

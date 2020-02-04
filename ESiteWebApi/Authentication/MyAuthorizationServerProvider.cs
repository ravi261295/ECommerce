using BusinessLayer.BusinessOperations;
using Microsoft.Owin.Security.OAuth;
using SharedLayerDTO.ModelsDTO;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace ESiteWebApi.Authentication
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MyAuthorizationServerProvider: OAuthAuthorizationServerProvider
    {
        LoginAndSignup loginAndSignup = new LoginAndSignup();

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            LoginDTO loginDTO = new LoginDTO()
            {
                Email = context.UserName,
                Password = context.Password
            };

            if (loginAndSignup.isLogin(loginDTO))
            {
                var authorizedUser = loginAndSignup.UserAccess(loginDTO.Email);
                
                identity.AddClaim(new Claim(ClaimTypes.Email, authorizedUser.Email));
                identity.AddClaim(new Claim(ClaimTypes.Name, authorizedUser.FullName));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Provided username or password is incorrect");
                return;
            }
        }
    }
}
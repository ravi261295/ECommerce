using BusinessLayer.BusinessOperations;
using SharedLayerDTO.ModelsDTO;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace ESiteWebApi.Controllers
{
    public class AccountController : ApiController
    {
        LoginAndSignup loginAndSignup = new LoginAndSignup();

        [HttpPost]
        [Route("api/Login")]
        public IHttpActionResult Post([FromBody]LoginDTO loginDTO)
        {
            if (loginAndSignup.isLogin(loginDTO))
            {
                return Ok(loginDTO);
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }

        [HttpPost]
        [Route("api/Register")]
        public IHttpActionResult Post([FromBody]RegisterDTO register)
        {
            if (loginAndSignup.RegisterUser(register))
            {
                return Ok("User added Successfully!!");
            }
            else
            {
                return BadRequest("Please Check All fields again!!");
            }
        }

        [HttpPost]
        [Authorize]
        [Route("api/GetUser")]
        public IHttpActionResult PostUser([FromBody]LoginDTO loginDTO)
        {
            var identity = (ClaimsIdentity)User.Identity;

            var Email = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

            return Ok("Email Is : " + Email + " and Name is : " + identity.Name);
        }
    }
}

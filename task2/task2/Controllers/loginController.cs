using Microsoft.Owin.Security.OAuth;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using task2.Models;

namespace task2.Controllers
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {


        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null)
            {
                var authToken = actionContext.Request.Headers
                    .Authorization.Parameter;

                // decoding authToken we get decode value in 'Username:Password' format  
                var decodeauthToken = System.Text.Encoding.UTF8.GetString(
                    Convert.FromBase64String(authToken));

                // spliting decodeauthToken using ':'   
                var arrUserNameandPassword = decodeauthToken.Split(':');
            
                // at 0th postion of array we get username and at 1st we get password  
               
                if (token(arrUserNameandPassword[0], arrUserNameandPassword[1]))
                {
                    // setting current principle  
                    Thread.CurrentPrincipal = new GenericPrincipal(
                           new GenericIdentity(arrUserNameandPassword[0]), null);

                }
                else
                {
                    actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                actionContext.Response = actionContext.Request
                         .CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
        public dynamic token(string Username, string Password)
        {
            using (StudentEntities db = new StudentEntities())
            {

                var auth = from st in db.staffs
                           join lo in db.logindetails on st.ID equals lo.staff_id
                           where st.Username == Username && st.Password == Password
                           select new
                           {
                               lo.token
                           };
                if (auth != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public dynamic token(string token)
        {
            using (StudentEntities db = new StudentEntities())
            {
                var auth = from lo in db.logindetails where lo.token == token select lo.token;
                 if (auth != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public dynamic Authentication(string Username, string Password)
        {
            try
            {
                using (StudentEntities db = new StudentEntities())
                {
                    var auth = db.staffs.Where(s => s.Username == Username && s.Password == Password).Select(x => x.ID).FirstOrDefault();
                    
                    if (auth != null)
                    {
                        var auth1 = db.logindetails.Where(s => s.staff_id == auth).FirstOrDefault();
                        if (auth1 != null)
                        {

                            return true;

                        }
                        else
                        {

                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex;
            }

        }
    }



    public class APIAUTHORIZATIONSERVERPROVIDER : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); //   
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (context.UserName == "admin" && context.Password == "admin")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim("username", "admin"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Hi Admin"));
                context.Validated(identity);
            }
            else if (context.UserName == "user" && context.Password == "user")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim("username", "user"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Hi User"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
        }
    }


    public class loginController : ApiController
    {
        DateTime DT = new DateTime();

        public dynamic login(string Username, string Password)
        {
            try
            {
                using (StudentEntities db = new StudentEntities())
                {
                    var auth = db.staffs.Where(s => s.Username == Username && s.Password == Password).Select(x => x.ID).FirstOrDefault();
                    if (auth != null)
                    {
                        var auth1 = db.logindetails.Where(s => s.staff_id == auth).FirstOrDefault();
                        if (auth1 != null)
                        {
                            auth1.staff_id = auth;
                            auth1.is_deleted = false;
                            auth1.login_time = DateTime.Now;
                            db.Entry(auth1).State = EntityState.Modified;
                            db.SaveChanges();
                            return true;

                        }
                        else
                        {
                            System.Guid guid = System.Guid.NewGuid();
                            logindetail auth2 = new logindetail();
                            auth2.id = 0;
                            auth2.staff_id = auth;
                            auth2.is_deleted = false;
                            auth2.login_time = DateTime.Now;
                            auth2.token = guid.ToString();
                            //   auth2.token.Substring(0, 19);
                            db.logindetails.Add(auth2);
                            db.SaveChanges();
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex;
            }

        }


    }
}

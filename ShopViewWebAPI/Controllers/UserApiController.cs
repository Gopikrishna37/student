using DBcontext.ServiceModels;
using ShopViewWebAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopViewWebAPI.Controllers
{
    public class UserApiController : ApiController
    {
        IUser_Service U = new User_method();
        [Route("api/UserApi/Login")]
        [HttpPost]
        public dynamic Post(UserLogin userLogin)
        {

            try
            {

                if (userLogin != null)
                {

                   var result= U.UserLogin(userLogin);
                    return result;
                }
                else
                {
                    return "error";
                }
            }
            catch (Exception e)
            {
                return e;
            }
        }


        [Route("api/UserApi/Signin")]
        [HttpPost]
        public dynamic Post(UserSignIn userSignIn)
        {
            try
            {
                if (userSignIn != null)
                {
                    U.UserSignIn(userSignIn);
                    return userSignIn;
                }
                else
                {
                    return "error";
                }
            }
            catch (Exception e)
            {
                return e;
            }
        }
    }

}
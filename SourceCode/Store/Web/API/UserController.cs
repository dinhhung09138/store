using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Filters;

namespace Web.API
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {

        [Route("login")]
        [HttpPost]
        public HttpResponseMessage Login(string UserName, string PassWord)
        {
            if(UserName == "admin" && PassWord == "pass")
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Tên đăng nhập hoặc mật khẩu không đúng");
        }

        [Route("profile")]
        [HttpGet]
        public HttpResponseMessage Profile()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Profile");
        }

        [Route("token")]
        [APIAuthorizationRequiredFilters]
        public HttpResponseMessage Token()
        {
            var header = Request.Headers;
            string token = "";
            if (header.Contains("token"))
            {
                token = header.GetValues("token").First();
            }
            return Request.CreateResponse(HttpStatusCode.OK, token);
        }
    }
    
}

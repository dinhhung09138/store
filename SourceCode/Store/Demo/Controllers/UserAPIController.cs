using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Demo.Controllers
{
    [RoutePrefix("api/user")]
    public class UserAPIController : ApiController
    {
        [Route("login")]
        [HttpGet]
        public HttpResponseMessage Login()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "OK");
        }
    }
}

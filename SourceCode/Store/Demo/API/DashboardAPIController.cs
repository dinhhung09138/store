using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Demo.API
{
    [RoutePrefix("api/dashboard")]
    public class DashboardAPIController : ApiController
    {
        [Route("login")]
        [HttpGet]
        public HttpResponseMessage Login()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Dashboard");
        }
    }
}

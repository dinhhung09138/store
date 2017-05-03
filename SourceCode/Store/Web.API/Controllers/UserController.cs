using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.API.Controllers
{
    
    public class UserController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Login()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "OK");
        }
    }
}

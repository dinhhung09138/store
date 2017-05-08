using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    /// <summary>
    /// Error controller
    /// </summary>
    /// 
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        /// <summary>
        /// Nots the found.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult NotFound()
        {
            return View();
        }

        /// <summary>
        /// Accesses the denied.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.User;

namespace Web.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private string RedirectTo;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if(filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!AuthorizeCore(filterContext.HttpContext))
            {
                filterContext.HttpContext.Response.StatusCode = 530;//User Access Denied
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                //If authorization results i HttpUnauthorizedResult, redirect to error page instead of logon page
                filterContext.Result = new RedirectResult(RedirectTo);
                return;
            }

            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                || filterContext.HttpContext.Request.IsAjaxRequest()
                || filterContext.ActionDescriptor.GetCustomAttributes(typeof(SkipAuthorizeFilterAttribute), inherit: true).Any();
            if (skipAuthorization)
            {
                return;
            }

            //Check if the requesting user has the permission to run the controller's action
            //if (!Authorization.HasPermission(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName, Convert.ToString(filterContext.HttpContext.Request.RequestContext.RouteData.DataTokens["area"])))
            //{
            //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", AppConstant.UnauthorizedAction }, { "controller", AppConstant.UserController }, { "area", AppConstant.UserArea } });
            //}

            //base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(httpContext == null)
            {
                throw new ArgumentNullException();
            }
            if(httpContext.Session != null && httpContext.Session["user"] == null)
            {
                SetRedirect(httpContext);
                return false;
            }
            var user = httpContext.Session["user"] as UserLogin;
            if(user != null && user.ID != null)
            {
                SetRedirect(httpContext);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Set the redirection path
        /// </summary>
        /// <param name="httpContext">http context</param>
        private void SetRedirect(HttpContextBase httpContext)
        {
            RedirectTo = "~/home/login";
            if (!string.IsNullOrEmpty(httpContext.Request.RawUrl))
            {
                RedirectTo = string.Format(RedirectTo + "?returnUrl={0}", httpContext.Request.RawUrl);
            }
        }
    }
}
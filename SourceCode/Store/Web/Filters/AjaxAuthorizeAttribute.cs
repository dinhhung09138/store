using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.User;

namespace Web.Filters
{
    public class AjaxAuthorizeAttribute : AuthorizeAttribute
    {
        private string RedirectTo = "~/home/requiredlogin";

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var url = new UrlHelper(filterContext.RequestContext);
            RedirectTo = url.Action("RequiredLogin", "Home");
            //
            if (filterContext == null)
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
            if (httpContext == null)
            {
                throw new ArgumentNullException();
            }
            if (httpContext.Session != null && httpContext.Session["user"] == null)
            {
                return false;
            }
            var user = httpContext.Session["user"] as UserLoginModel;
            if (user != null && user.ID != null)
            {
                return true;
            }
            return false;
        }

    }
}
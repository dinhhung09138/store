﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.User;
using DataAccess.User;

namespace Web.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private string RedirectTo;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
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
                || filterContext.HttpContext.Request.IsAjaxRequest();
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
                if (httpContext.Request.Cookies.Get("userName") != null)
                {
                    AccountSrv _srvAccount = new AccountSrv();
                    UserLoginModel _return = _srvAccount.Login(httpContext.Request.Cookies.Get("userName").Value, httpContext.Request.Cookies.Get("password").Value);
                    if (_return != null && _return.UserName.Length > 0)
                    {
                        httpContext.Session["user"] = _return;
                        return true;
                    }
                }
                SetRedirect(httpContext);
                return false;
            }
            var user = httpContext.Session["user"] as UserLoginModel;
            if (user != null && user.ID != null)
            {
                SetRedirect(httpContext);
                return true;
            }
            return false;
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
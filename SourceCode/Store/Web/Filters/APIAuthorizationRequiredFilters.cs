﻿using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Common.Security;

namespace Web.Filters
{
    /// <summary>
    /// Check token authorization
    /// </summary>
    public class APIAuthorizationRequiredFilters : ActionFilterAttribute
    {
        private const string Token = "token";

        /// <summary>
        /// Call when action executing
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Contains(Token))
            {
                var tokenValue = actionContext.Request.Headers.GetValues(Token).First();
                if(!TokenSecurity.IsTokenValid(tokenValue))
                {
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.MethodNotAllowed) { ReasonPhrase = "Yêu cầu không hợp lệ" };
                }
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            base.OnActionExecuting(actionContext);
        }
    }
}
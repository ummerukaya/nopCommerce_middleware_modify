using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nop.Core;
using Nop.Data;

namespace Nop.Services.Installation
{
    
    public class CustomInterceptorMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;

        #endregion

        #region Ctor

        public CustomInterceptorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoke middleware actions
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <param name="webHelper">Web helper</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task InvokeAsync(HttpContext context, IWebHelper webHelper)
        {
            if (!CustomSettingsManager.Test())
            {
                var defaultUrl = $"{webHelper.GetStoreLocation()}{NopInstallationDefaults.CustomPath}";
                if (!webHelper.GetThisPageUrl(false).StartsWith(defaultUrl, StringComparison.InvariantCultureIgnoreCase))
                {
                    //redirect
                    context.Response.Redirect(defaultUrl);
                    return;
                }
            }

            //whether database is installed
            if (!await DataSettingsManager.IsDatabaseInstalledAsync())
            {
                var installUrl = $"{webHelper.GetStoreLocation()}{NopInstallationDefaults.InstallPath}";
                if (!webHelper.GetThisPageUrl(false).StartsWith(installUrl, StringComparison.InvariantCultureIgnoreCase))
                {
                    //redirect
                    context.Response.Redirect(installUrl);
                    return;
                }
            }

            //or call the next middleware in the request pipeline
            await _next(context);
        }

        #endregion
    }
}
﻿//-------------------------------------------------------------------------------
// <copyright file="ClearCacheOnSuccessAttribute.cs" company="bbv Software Services AG">
//   Copyright (c) 2010 bbv Software Services AG
//   Author: Remo Gloor (remo.gloor@gmail.com)
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

using SampleApplication.Services.DistributedCacheService;
using System.Web.Http.Filters;
using Ninject;

namespace SampleApplication.Controllers.FilterInjectionExample
{
    /// <summary>
    /// Specifies that on successful execution the cached result of the action specified is cleared. 
    /// </summary>
    public class ClearCacheOnSuccessAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The name of the action that is cleared.
        /// </summary>
        private readonly string actionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClearCacheOnSuccessAttribute"/> class.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        public ClearCacheOnSuccessAttribute(string actionName)
        {
            this.actionName = actionName;
        }

        /// <summary>
        /// Gets or sets the cache service.
        /// </summary>
        /// <value>The cache service.</value>
        [Inject]
        public IDistributedCacheService CacheService
        {
            get; set;
        }

        /// <summary>
        /// Called by the MVC framework after the action method executes.
        /// </summary>
        /// <param name="actionExecutedContext">The action executed context.</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.ActionContext.Response.IsSuccessStatusCode)
            {
                this.CacheService.ClearEntry(string.Format(
                    "{0}.{1}",
                    actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    this.actionName));
            }

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
//-------------------------------------------------------------------------------
// <copyright file="NinjectScope.cs" company="bbv Software Services AG">
//   Copyright (c) 2012 bbv Software Services AG
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


namespace Ninject.Web.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Dependencies;
    using Ninject.Syntax;
    using Ninject.Activation;
    using Ninject.Parameters;

    /// <summary>
    /// This class is taken from the following:
    /// https://github.com/filipw/Ninject-resolver-for-ASP.NET-Web-API/ and modified based on comments on 
    /// http://www.strathweb.com/2012/05/using-ninject-with-the-latest-asp-net-web-api-source/
    /// </summary>
    public class NinjectScope : IDependencyScope
    {
        private IResolutionRoot _resolutionRoot;

        public NinjectScope(IResolutionRoot kernel)
        {
            _resolutionRoot = kernel;
        }

        public object GetService(Type serviceType)
        {
            IRequest request = _resolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return _resolutionRoot.Resolve(request).SingleOrDefault();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            IRequest request = _resolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return _resolutionRoot.Resolve(request).ToList();
        }
        /// <summary>
        /// Do Nothing...
        /// Do not want to ASP.NET MVC to handle per-request scoping 
        /// Ninject already handles this for us.
        /// </summary>
        public void Dispose()
        {
        }
    }

}
#region Copyright

//+ Jampad Technology, Inc. 2007-2013 Pro 3.0 - Core Module
//+ Copyright © Jampad Technology, Inc. 2008-2013

#endregion

using System;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Nalarium.ServiceModel.Behavior
{
    /// <summary>
    /// Sets the HTTP code to 200 for faults.
    /// </summary>
    public class HttpStatusCode200ErrorHandler : IErrorHandler
    {
        //- @ServiceType -//

        //+
        //- @Ctor -//
        public HttpStatusCode200ErrorHandler(Type serviceType)
        {
            ServiceType = serviceType;
        }

        public Type ServiceType { get; set; }

        //+
        //- @HandleError -//

        #region IErrorHandler Members

        public bool HandleError(Exception error)
        {
            return false;
        }

        //- @ProvideFault -//
        public virtual void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            fault.Properties[HttpResponseMessageProperty.Name] = new HttpResponseMessageProperty
                                                                 {
                                                                     StatusCode = HttpStatusCode.OK
                                                                 };
        }

        #endregion
    }
}
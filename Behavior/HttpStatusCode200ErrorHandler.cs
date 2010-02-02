#region Copyright
//+ Nalarium Pro 3.0 - Core Module
//+ Copyright © Jampad Technology, Inc. 2008-2010
#endregion
using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
//+
namespace Nalarium.ServiceModel.Behavior
{
    /// <summary>
    /// Sets the HTTP code to 200 for faults.
    /// </summary>
    public class HttpStatusCode200ErrorHandler : IErrorHandler
    {
        //- @ServiceType -//
        public Type ServiceType { get; set; }

        //+
        //- @Ctor -//
        public HttpStatusCode200ErrorHandler(Type serviceType)
        {
            ServiceType = serviceType;
        }

        //+
        //- @HandleError -//
        public bool HandleError(Exception error)
        {
            return false;
        }

        //- @ProvideFault -//
        public virtual void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            fault.Properties[HttpResponseMessageProperty.Name] = new HttpResponseMessageProperty
            {
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
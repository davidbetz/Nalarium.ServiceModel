#region Copyright
//+ Nalarium Pro 3.0 - Core Module
//+ Copyright © Jampad Technology, Inc. 2008-2010
#endregion
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
//+
namespace Nalarium.ServiceModel.Behavior
{
    /// <summary>
    /// Applies HttpStatusCode200ErrorHandler.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class HttpStatusCode200Behavior : Attribute, IServiceBehavior
    {
        //- @AddBindingParameters -//
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            //+ blank
        }

        //- @ApplyDispatchBehavior -//
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                dispatcher.ErrorHandlers.Add(new HttpStatusCode200ErrorHandler(serviceDescription.ServiceType));
            }
        }

        //- @Validate -//
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            //+ blank
        }
    }
}
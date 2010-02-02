#region Copyright
//+ Nalarium Pro 3.0 - Core Module
//+ Copyright © Jampad Technology, Inc. 2008-2010
#endregion
using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
//+
namespace Nalarium.ServiceModel.Behavior
{
    /// <summary>
    /// Used to do simple WCF benchmarking.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BenchmarkBehavior : Attribute, IOperationBehavior, IServiceBehavior
    {
        //+ IOperationBehavior
        //- @AddBindingParameters -//
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
            //+ blank
        }

        //- @ApplyClientBehavior -//
        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            //+ blank
        }

        //- @ApplyDispatchBehavior -//
        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.Invoker = new BenchmarkOperationInvoker(dispatchOperation.Invoker);
        }

        //- @Validate -//
        public void Validate(OperationDescription operationDescription)
        {
            //+ blank
        }

        //+ IServiceBehavior
        //- @AddBindingParameters -//
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            //+ blank
        }

        //- @ApplyDispatchBehavior -//
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ServiceEndpoint endpoint in serviceDescription.Endpoints)
            {
                foreach (OperationDescription operation in endpoint.Contract.Operations)
                {
                    operation.Behaviors.Add(new BenchmarkBehavior());
                }
            }
        }

        //- @Validate -//
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            //+ blank
        }
    }
}
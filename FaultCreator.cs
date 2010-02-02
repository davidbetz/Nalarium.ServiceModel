#region Copyright
//+ Nalarium Pro 3.0 - Core Module
//+ Copyright © Jampad Technology, Inc. 2007-2009
#endregion
using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
//+
namespace Nalarium.ServiceModel
{
    /// <summary>
    /// Used by error handlers to create FaultException objects from exceptions.
    /// </summary>
    public static class FaultCreator
    {
        //- ~Throw -//
        public static void CreateFault(Type type, Exception error, MessageVersion version, ref Message fault)
        {
            if (error.GetType().IsGenericType && error is FaultException)
            {
                return;
            }
            if (IsExceptionInContract(type, error))
            {
                CreateFaultMessage(type, error, version, ref fault);
            }
            else
            {
                CreateFaultMessage(type, new UnhandledException(error.Message), version, ref fault);
            }
        }

        //- $CreateFaultMessage -//
        private static void CreateFaultMessage(Type type, Exception error, MessageVersion version, ref Message fault)
        {
            try
            {
                Type errorType = error.GetType();
                Type faultClosedType = typeof(FaultException<>).MakeGenericType(errorType);
                Exception exception = (Exception)Activator.CreateInstance(errorType, error.Message);
                FaultException faultException = (FaultException)Activator.CreateInstance(faultClosedType, exception, error.Message);
                fault = Message.CreateMessage(version, faultException.CreateMessageFault(), faultException.Action);
                //+
                fault.Headers.Add(MessageHeader.CreateHeader("DetailType", "", errorType.FullName));
                fault.Headers.Add(MessageHeader.CreateHeader("DetailPackage", "", errorType.Assembly.FullName));
            }
            catch
            {
            }
        }

        //- $IsExceptionInContract -//
        private static Boolean IsExceptionInContract(Type serviceType, Exception error)
        {
            OperationContext context = OperationContext.Current;
            if (context == null)
            {
                return false;
            }
            ServiceEndpoint endpoint = context.Host.Description.Endpoints.Find(context.EndpointDispatcher.EndpointAddress.Uri);
            DispatchOperation dispatchOperation = context.EndpointDispatcher.DispatchRuntime.Operations.Where(op => op.Action == context.IncomingMessageHeaders.Action).First();
            OperationDescription operationDesc = endpoint.Contract.Operations.Find(dispatchOperation.Name);
            //+
            return operationDesc.Faults.Any(p => p.DetailType == error.GetType());
        }
    }
}
#region Copyright
//+ Nalarium Pro 3.0 - Core Module
//+ Copyright © Jampad Technology, Inc. 2007-2009
#endregion
using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
//+
namespace Nalarium.ServiceModel.Behavior
{
    public class ErrorHandler : HttpStatusCode200ErrorHandler
    {
        //- @Ctor -//
        public ErrorHandler(Type serviceType)
            : base(serviceType)
        {
        }

        //+
        //- @ProvideFault -//
        public override void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            FaultCreator.CreateFault(this.ServiceType, error, version, ref fault);
            //+ set status code to 200
            base.ProvideFault(error, version, ref fault);
        }
    }
}
#region Copyright

//+ Jampad Technology, Inc. 2007-2013 Pro 3.0 - Core Module
//+ Copyright © Jampad Technology, Inc. 2008-2013

#endregion

using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Nalarium.ServiceModel.Behavior.STA
{
    public class STAOperationBehavior : Attribute, IOperationBehavior
    {
        //- @AddBindingParameters -//

        #region IOperationBehavior Members

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
            dispatchOperation.Invoker = new STAInvoker(dispatchOperation.Invoker);
        }

        //- @Validate -//
        public void Validate(OperationDescription operationDescription)
        {
            //+ blank
        }

        #endregion
    }
}
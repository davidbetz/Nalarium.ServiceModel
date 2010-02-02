#region Copyright
//+ Nalarium Pro 3.0 - Core Module
//+ Copyright © Jampad Technology, Inc. 2008-2010
#endregion
using System;
using System.Security;
using System.ServiceModel.Dispatcher;
//+
namespace Nalarium.ServiceModel.Behavior
{
    public class STAInvoker : IOperationInvoker
    {
        //- $InnerOperationInvoker -//
        private IOperationInvoker InnerOperationInvoker { get; set; }

        //+
        //- @Ctor -//
        public STAInvoker(IOperationInvoker operationInvoker)
        {
            this.InnerOperationInvoker = operationInvoker;
        }

        //+
        //- @AllocateInputs -//
        public Object[] AllocateInputs()
        {
            return InnerOperationInvoker.AllocateInputs();
        }

        //- @Invoke -//
        public Object Invoke(Object instance, Object[] inputs, out Object[] outputs)
        {
            Object result = null;
            Object[] staOutputs = null;
            System.ServiceModel.OperationContext context = System.ServiceModel.OperationContext.Current;
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                using (System.ServiceModel.OperationContextScope scope = new System.ServiceModel.OperationContextScope(context))
                {
                    result = InnerOperationInvoker.Invoke(instance, inputs, out staOutputs);
                }
            }));
            thread.SetApartmentState(System.Threading.ApartmentState.STA);
            thread.Start();
            thread.Join();
            //+
            outputs = staOutputs;
            //+
            return result;
        }

        //- @InvokeBegin -//
        public IAsyncResult InvokeBegin(Object instance, Object[] inputs, AsyncCallback callback, Object state)
        {
            return InnerOperationInvoker.InvokeBegin(instance, inputs, callback, state);
        }

        //- @InvokeEnd -//
        public Object InvokeEnd(Object instance, out Object[] outputs, IAsyncResult result)
        {
            return InnerOperationInvoker.InvokeEnd(instance, out outputs, result);
        }

        //- @IsSynchronous -//
        public bool IsSynchronous
        {
            get { return InnerOperationInvoker.IsSynchronous; }
        }
    }
}
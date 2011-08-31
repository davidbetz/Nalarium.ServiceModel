#region Copyright

//+ Nalarium Pro 3.0 - Core Module
//+ Copyright © Jampad Technology, Inc. 2008-2010

#endregion

using System;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace Nalarium.ServiceModel.Behavior
{
    public class STAInvoker : IOperationInvoker
    {
        //- $InnerOperationInvoker -//

        //+
        //- @Ctor -//
        public STAInvoker(IOperationInvoker operationInvoker)
        {
            InnerOperationInvoker = operationInvoker;
        }

        private IOperationInvoker InnerOperationInvoker { get; set; }

        //+
        //- @AllocateInputs -//

        #region IOperationInvoker Members

        public Object[] AllocateInputs()
        {
            return InnerOperationInvoker.AllocateInputs();
        }

        //- @Invoke -//
        public Object Invoke(Object instance, Object[] inputs, out Object[] outputs)
        {
            Object result = null;
            Object[] staOutputs = null;
            OperationContext context = OperationContext.Current;
            var thread = new Thread(new ThreadStart(delegate
                                                    {
                                                        using (var scope = new OperationContextScope(context))
                                                        {
                                                            result = InnerOperationInvoker.Invoke(instance, inputs, out staOutputs);
                                                        }
                                                    }));
            thread.SetApartmentState(ApartmentState.STA);
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
            get
            {
                return InnerOperationInvoker.IsSynchronous;
            }
        }

        #endregion
    }
}
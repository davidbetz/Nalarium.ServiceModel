#region Copyright
//+ Nalarium Pro 3.0 - Core Module
//+ Copyright © Jampad Technology, Inc. 2008-2010
#endregion
using System;
using System.ServiceModel.Dispatcher;
//+
namespace Nalarium.ServiceModel.Behavior
{
    /// <summary>
    /// For use with BenchmarkBehavior.
    /// </summary>
    public class BenchmarkOperationInvoker : IOperationInvoker
    {
        //- $InnerOperationInvoker -//
        private IOperationInvoker InnerOperationInvoker { get; set; }

        //+
        //- @Ctor -//
        public BenchmarkOperationInvoker(IOperationInvoker operationInvoker)
        {
            this.InnerOperationInvoker = operationInvoker;
        }

        //+
        //- @AllocateInputs -//
        public Object[] AllocateInputs()
        {
            return InnerOperationInvoker.AllocateInputs();
        }

        //- @AllocateInputs -//
        public Object Invoke(Object instance, Object[] inputs, out Object[] outputs)
        {
            DateTime before = DateTime.Now;
            //+
            Object result = InnerOperationInvoker.Invoke(instance, inputs, out outputs);
            System.Diagnostics.Debug.WriteLine(String.Format("{0:0,0} ({1}:{2})", (DateTime.Now - before).TotalMilliseconds, instance.GetType().FullName, GetAction()));
            //+
            return result;
        }

        //- @InvokeBegin -//
        public IAsyncResult InvokeBegin(Object instance, Object[] inputs, AsyncCallback callback, Object state)
        {
            IProvidesAsyncBenchmark benchmark = instance as IProvidesAsyncBenchmark;
            if (benchmark != null)
            {
                benchmark.StartTime = DateTime.Now;
            }
            //+
            return InnerOperationInvoker.InvokeBegin(instance, inputs, callback, state);
        }

        //- @InvokeEnd -//
        public Object InvokeEnd(Object instance, out Object[] outputs, IAsyncResult result)
        {
            Object returnResult = InnerOperationInvoker.InvokeEnd(instance, out outputs, result);
            IProvidesAsyncBenchmark benchmark = instance as IProvidesAsyncBenchmark;
            if (benchmark != null)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("{0:0,0} ({1}:{2})", (DateTime.Now - benchmark.StartTime).TotalMilliseconds, instance.GetType().FullName, GetAction()));
            }
            //+
            return returnResult;
        }

        //- @GetAction -//
        public String GetAction()
        {
            return System.ServiceModel.OperationContext.Current.IncomingMessageHeaders.GetHeader<String>("Action", "http://schemas.microsoft.com/ws/2005/05/addressing/none");
        }

        //- @IsSynchronous -//
        public bool IsSynchronous
        {
            get { return InnerOperationInvoker.IsSynchronous; }
        }
    }
}
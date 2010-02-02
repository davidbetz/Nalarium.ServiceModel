#region Copyright
//+ Nalarium Pro 3.0 - Core Module
//+ Copyright © Jampad Technology, Inc. 2007-2009
#endregion
using System;
using System.Runtime.Serialization;
//+
namespace Nalarium.ServiceModel
{
    /// <summary>
    /// Represents an unhandled exception.
    /// </summary>
    [Serializable]
    public class UnhandledException : Exception
    {
        //- @Ctor -//
        public UnhandledException() { }
        public UnhandledException(string message) : base(message) { }
        public UnhandledException(string message, Exception inner) : base(message, inner) { }
        protected UnhandledException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
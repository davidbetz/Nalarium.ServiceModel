#region Copyright
//+ Nalarium Pro 3.0 - Core Module
//+ Copyright © Jampad Technology, Inc. 2007-2009
#endregion
using System;
using System.ServiceModel;
//+
namespace Nalarium.ServiceModel
{
    public static class FaultThrower
    {
        //- ~Throw -//
        /// <summary>
        /// Throws a generic FaultException exception of type T.
        /// </summary>
        /// <typeparam name="T">Type of exception to throw.</typeparam>
        /// <param name="exception">Exception to throw.</param>
        public static void Throw<T>(T exception) where T : Exception
        {
            throw new FaultException<T>(exception, exception.Message);
        }
    }
}
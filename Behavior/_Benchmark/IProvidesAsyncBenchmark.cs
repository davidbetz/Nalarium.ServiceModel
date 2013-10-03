#region Copyright

//+ Jampad Technology, Inc. 2007-2013 Pro 3.0 - Core Module
//+ Copyright © Jampad Technology, Inc. 2008-2013

#endregion

using System;

namespace Nalarium.ServiceModel.Behavior
{
    /// <summary>
    /// Used to assist in the benchmarking of async operations.
    /// </summary>
    public interface IProvidesAsyncBenchmark
    {
        DateTime StartTime { get; set; }
    }
}
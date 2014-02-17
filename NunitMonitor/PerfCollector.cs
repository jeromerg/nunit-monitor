using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NunitMonitor
{
    public class PerfCollector
    {
        private readonly List<PerformanceCounter> mPerformanceCounters;
        private IPerfLogger mLogger;

        public PerfCollector(IPerfLogger logger, IEnumerable<PerfCounterBuilder> perfCounterBuilders)
        {
            mLogger = logger;
            mPerformanceCounters = new List<PerformanceCounter>();
            foreach (var perfCounterBuilder in perfCounterBuilders)
                mPerformanceCounters.Add(perfCounterBuilder.ProducePerfCounter());
        }

        private static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.WaitForFullGCComplete();
            GC.Collect();
        }

        public void Collect(bool isEndOfTest, string testName)
        {
            ClearMemory();

            var values = new List<double>();
            foreach (var performanceCounter in mPerformanceCounters)
            {
                values.Add(performanceCounter.NextValue());
            }
            mLogger.Log(isEndOfTest, testName, values);
        }
    }
}

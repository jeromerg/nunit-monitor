using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Core;
using NUnit.Core.Extensibility;

namespace NunitMonitor
{
    [NUnitAddin(Description = "Event Timestamp Logger")]
    class NunitMonitorAddin
    {
        private readonly PerformanceCounter[] mPerformanceCounters;

        public NunitMonitorAddin()
        {
            string processName = Process.GetCurrentProcess().ProcessName;
            mPerformanceCounters = new[]
            {
                new PerformanceCounter("Process", "Private Bytes", processName), 
                new PerformanceCounter("Process", "Virtual Bytes", processName), 
                new PerformanceCounter("Process", "Handle Count", processName), 
                new PerformanceCounter("Process", "Thread Count", processName), 
                new PerformanceCounter(".NET CLR Memory", "# Bytes in all Heaps", processName),                 
                new PerformanceCounter(".NET CLR Loading", "Current appdomains", processName), 
            };
        }

        public bool Install(IExtensionHost host)
        {
            if (host == null)
                throw new ArgumentNullException("host");

            IExtensionPoint listeners = host.GetExtensionPoint("EventListeners");
            if (listeners == null)
                return false;

            listeners.Install(this);
            return true;
        }

        #region EventListener Members

        public void RunStarted(string name, int testCount)
        {
        }

        public void RunFinished(Exception exception)
        {
        }

        public void RunFinished(TestResult result)
        {
        }

        public void SuiteStarted(TestName testName)
        {
        }

        public void SuiteFinished(TestResult result)
        {
        }

        public void TestStarted(TestName testName)
        {
            ClearMemory();
            CollectPerformanceCounters(testName);
        }

        public void TestFinished(TestResult result)
        {
            ClearMemory();
            CollectPerformanceCounters(result.Test.TestName);
        }

        public void TestOutput(TestOutput testOutput)
        {
        }

        public void UnhandledException(Exception exception)
        {
        }
        #endregion

        #region private helpers
        private static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.WaitForFullGCComplete();
            GC.Collect();
        }

        private void CollectPerformanceCounters(TestName testName)
        {
            
        }
        #endregion

    }
}

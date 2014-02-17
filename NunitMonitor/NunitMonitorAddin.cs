using System;
using System.Diagnostics;
using System.IO;
using NUnit.Core;
using NUnit.Core.Extensibility;

namespace NunitMonitor
{
    [NUnitAddin(Description = "Event Timestamp Logger", Type = ExtensionType.Core)]
    public class NunitMonitorAddin : IAddin, EventListener
    {
        private PerfCollector mPerfCollector;

        public bool Install(IExtensionHost host)
        {
            //Debugger.Break();

            if (host == null)
                throw new ArgumentNullException("host");

            IExtensionPoint listeners = host.GetExtensionPoint("EventListeners");
            if (listeners == null)
                return false;

            listeners.Install(this);


            // NiceToHave: inject configuration from configuration file
            var processName = Process.GetCurrentProcess().ProcessName;
            var perfCounterBuilders = new[]
            {
                new PerfCounterBuilder("Process", "Private Bytes", processName), 
                new PerfCounterBuilder("Process", "Virtual Bytes", processName), 
                new PerfCounterBuilder("Process", "Handle Count", processName), 
                new PerfCounterBuilder("Process", "Thread Count", processName), 
                new PerfCounterBuilder(".NET CLR Memory", "# Bytes in all Heaps", processName),
                new PerfCounterBuilder(".NET CLR Loading", "Current appdomains", processName)
            };
            mPerfCollector = new PerfCollector(new CsvPerfLogger(@"c:\PerfLogs", @"nunit.csv", perfCounterBuilders), perfCounterBuilders);

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
            mPerfCollector.Collect(false, testName.FullName);
        }

        public void TestFinished(TestResult result)
        {
            mPerfCollector.Collect(true, result.FullName);
        }

        public void TestOutput(TestOutput testOutput)
        {
        }

        public void UnhandledException(Exception exception)
        {
        }
        #endregion

    }
}

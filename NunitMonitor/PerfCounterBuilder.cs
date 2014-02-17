using System.Diagnostics;

namespace NunitMonitor
{
    public class PerfCounterBuilder
    {
        private readonly string mCategoryName;
        private readonly string mCounterName;
        private readonly string mInstanceName;

        public PerfCounterBuilder(string categoryName, string counterName, string instanceName)
        {
            mCategoryName = categoryName;
            mCounterName = counterName;
            mInstanceName = instanceName;
        }

        public string CategoryName
        {
            get { return mCategoryName; }
        }

        public string CounterName
        {
            get { return mCounterName; }
        }

        public string InstanceName
        {
            get { return mInstanceName; }
        }

        public string FullName
        {
            get { return "" + CategoryName + " > " + CounterName + " > " + InstanceName; }
        }

        public PerformanceCounter ProducePerfCounter()
        {
            return new PerformanceCounter(CategoryName, CounterName, InstanceName);
        }
    }
}

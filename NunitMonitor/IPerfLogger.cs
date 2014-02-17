using System.Collections.Generic;

namespace NunitMonitor
{
    public interface IPerfLogger
    {
        void Log(bool isEndOfTest, string testName, List<double> values);
    }
}

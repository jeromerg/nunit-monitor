using System;
using System.Collections.Generic;
using System.IO;

namespace NunitMonitor
{
    public class CsvPerfLogger : IPerfLogger
    {
        private readonly string mPerformanceLogPath;
        private readonly string mPerformanceLogFilePath;
        private readonly string mLogTitleLine;

        public CsvPerfLogger(string performanceLogPath, string sPerformanceLogFileName, PerfCounterBuilder[] perfColumnTitles)
        {
            mPerformanceLogPath = performanceLogPath;
            mPerformanceLogFilePath = Path.Combine(mPerformanceLogPath, sPerformanceLogFileName);

            mLogTitleLine = "Time;TestName;IsEndOfTest";
            for (int i = 0; i < perfColumnTitles.Length; i++)
            {
                mLogTitleLine += ";" + perfColumnTitles[i].FullName;
            }
        }

        public void Log(bool isEndOfTest, string testName, List<double> values)
        {
            Directory.CreateDirectory(mPerformanceLogPath);
            bool fileAlreadyExist = File.Exists(mPerformanceLogFilePath);

            StreamWriter streamWriter = File.AppendText(mPerformanceLogFilePath);
            try
            {
                if (!fileAlreadyExist)
                    streamWriter.WriteLine(mLogTitleLine);

                streamWriter.Write(DateTime.UtcNow.ToString("O"));
                streamWriter.Write(";");
                streamWriter.Write(testName);
                streamWriter.Write(";");
                streamWriter.Write(isEndOfTest);
                streamWriter.Write(";");
                foreach (var value in values)
                {
                    streamWriter.Write(value);
                    streamWriter.Write(";");
                }                
                streamWriter.WriteLine();
            }
            finally
            {
                streamWriter.Close();
            }
        }

    }
}

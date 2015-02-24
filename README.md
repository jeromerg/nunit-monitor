nunit-monitor
=============

monitor memory, threads and other performance counters during test execution.

Install
=======
- Open solution
- Choose the correct platform x86 or x64 depending on your test environment.
- eventually replace the nunit.core.dll and nunit.core.interfaces.dll with the dll of your current NUnit version in use.
- Compile
- Copy the produced dll to your Nunit\Addins directory
- Run Nunit GUI and check that the plugin has been correctly installed in Tools>Addins Dialog.
- If not, check the NUnit internal logs

Run
===
Once the addin has been installed, run your unit tests as usual. 
Performance counters are written C:\PerfLogs\nunit.csv file.

Limitations
===========
It is a one-shot development. But The code has been structured to be easily extended with new performance counters, new target types.

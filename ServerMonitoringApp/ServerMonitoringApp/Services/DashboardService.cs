using ServerMonitoringApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ServerMonitoringApp.Services
{
    internal class DashboardService
    {
        public static double GetCpuUsage(AppDomain hostDomain)
        {
            if (Process.GetCurrentProcess().TotalProcessorTime.TotalMilliseconds > 0)
                return hostDomain.MonitoringTotalProcessorTime.TotalMilliseconds * 100 / Process.GetCurrentProcess().TotalProcessorTime.TotalMilliseconds;
            return 0;
        }

        public static double GetMemoryUsage(AppDomain hostDomain)
        {
            if (AppDomain.MonitoringSurvivedProcessMemorySize > 0)
                return (double)hostDomain.MonitoringSurvivedMemorySize * 100 / (double)AppDomain.MonitoringSurvivedProcessMemorySize;
            return 0;
        }

        public static double GetDiskUsage(AppDomain hostDomain)
        {
            return 50;
        }

        public static double GetNetworkUsage(AppDomain hostDomain)
        {
            return 50;
        }

        

        public static IEnumerable<EventDetail> GetEventLogs(string eventtype = "Application")
        {
            EventLog log = new EventLog(eventtype);
            var eventDetail = log.Entries.Cast<EventLogEntry>()
                .Where(x => x.EntryType == EventLogEntryType.Error).Take(10)
                .Select(x => new EventDetail
                {
                    Source = x.Source,
                    Message = x.Message,
                    EventTime = x.TimeGenerated,
                    Level = (EventLevel)x.EntryType
                }).ToList();
            return eventDetail;
        }
    }
}
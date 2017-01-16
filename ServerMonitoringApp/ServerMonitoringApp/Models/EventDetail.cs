using System;

namespace ServerMonitoringApp.Models
{
    public class EventDetail
    {
        public DateTime EventTime { get; set; }

        public string Message { get; set; }

        public string Source { get; set; }

        public EventLevel Level { get; set; }
    }

    public enum EventLevel
    {
        Error = 1,
        Warning = 2,
        Information = 4,
        SuccessAudit = 8,
        FailureAudit = 16
    }
}
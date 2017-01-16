using System;
using System.Collections.Generic;

namespace ServerMonitoringApp.Models
{
    public class SystemInfo
    {
        public string MachineName { get; set; }

        public double Cpu { get; set; }

        public float TotalMemory { get; set; }

        public double Memory { get; set; }

        public double Network { get; set; }

        public double Disk { get; set; }

        public string ServerTime { get; set; }

        public int ClientConnected { get; set; }

        public int TotalProcess { get; set; }

        public Tuple<IEnumerable<ProcessDetails>, int> Process { get; set; }
    }
}
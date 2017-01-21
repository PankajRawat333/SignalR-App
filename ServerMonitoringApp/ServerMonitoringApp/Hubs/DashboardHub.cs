using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using ServerMonitoringApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;

namespace ServerMonitoringApp.Hubs
{
    [HubName("Dashboard")]
    public class DashboardHub : Hub
    {
        private static List<string> _clients = new List<string>();
        private static string _machineName = string.Empty;
        private static float _totalMemory = 0;
        private static PerformanceCounter _cpuUsage;
        private static PerformanceCounter _ramUsage;
        private static PerformanceCounter _ram;

        public void GetConnectedClient()
        {
            if (_cpuUsage == null)
                _cpuUsage = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
            if (_ramUsage == null)
                _ramUsage = new PerformanceCounter("Memory", "Available MBytes", true);
            if (_ram == null)
                _ram = new PerformanceCounter("Memory", "% Committed Bytes In Use", true);
            if (_totalMemory == 0)
                _totalMemory = GetTotalMemory().Result;
            if (string.IsNullOrWhiteSpace(_machineName = Environment.MachineName))
                _machineName = Environment.MachineName;

            GetSystemInfo();
        }

        public override Task OnConnected()
        {
            if (!_clients.Any(u => u == Context.ConnectionId))
                _clients.Add(Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _clients.Remove(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public void GetSystemInfo()
        {
            try
            {
                while (_clients.Count > 0)
                {
                    var systemInfo = new SystemInfo
                    {
                        MachineName = _machineName,
                        TotalMemory = _totalMemory,
                        ClientConnected = _clients.Count,
                        ServerTime = DateTime.Now.ToString("hh:mm:ss tt"),
                        Process = GetProcess(),
                        Cpu = GetCpuUsage(),
                        Memory = GetMemoryUsage()
                    };
                    this.Clients.All.onHitRecorded(JsonConvert.SerializeObject(systemInfo));
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                DisposePerformanceCounter();
            }
        }

        public void DisposePerformanceCounter()
        {
            _cpuUsage.Dispose();
            _ram.Dispose();
            _ramUsage.Dispose();

        }
        private float GetCpuUsage()
        {
            return Convert.ToInt32(_cpuUsage.NextValue());
        }

        private double GetMemoryUsage()
        {
            var memoryAvailable = _ramUsage.NextValue() / 1024;
            return Math.Round(100 - ((memoryAvailable * 100) / _totalMemory), 0);
        }

        private Task<float> GetTotalMemory()
        {
            var task = Task<float>.Run(() =>
            {
                float SizeinGB = 0;
                string Query = "SELECT MaxCapacity FROM Win32_PhysicalMemoryArray";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(Query);
                foreach (ManagementObject WniPART in searcher.Get())
                {
                    UInt32 SizeinKB = Convert.ToUInt32(WniPART.Properties["MaxCapacity"].Value);
                    if (SizeinKB > 0)
                    {
                        UInt32 SizeinMB = SizeinKB / 1024;
                        SizeinGB = SizeinMB / 1024;
                    }
                }
                return SizeinGB;
            });
            return task;
        }

        public Tuple<IEnumerable<ProcessDetails>, int> GetProcess()
        {
            var processdetails = new List<ProcessDetails>();
            Process[] processlist = Process.GetProcesses();
            foreach (Process process in processlist)
            {
                var processDetail = new ProcessDetails
                {
                    Id = process.Id,
                    Name = process.ProcessName,
                    MemorySize = process.PrivateMemorySize64,
                    VirtualMemory = process.VirtualMemorySize64
                };
                processdetails.Add(processDetail);
            }

            return Tuple.Create(processdetails.OrderByDescending(x => x.MemorySize).Take(10), processdetails.Count());
        }
    }
}
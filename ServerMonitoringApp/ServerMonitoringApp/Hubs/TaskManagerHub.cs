using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Diagnostics;

namespace ServerMonitoringApp.Hubs
{
    [HubName("TaskManager")]
    public class TaskManagerHub : Hub
    {
        
    }

    public class ServerEventListener : TraceListener
    {
        private void WriteToSignalR(string message)
        {
            var traceHub = GlobalHost.ConnectionManager.GetHubContext<TaskManagerHub>();
            traceHub.Clients.All.traceReceived(message);
        }

        public override void Write(string message)
        {
            WriteToSignalR(message);
        }

        public override void WriteLine(string message)
        {
            WriteToSignalR(message);
        }
    }
}
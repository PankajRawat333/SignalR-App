using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using ServerMonitoringApp.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServerMonitoringApp.Hubs
{
    [HubName("Event")]
    public class EventHub : Hub
    {
        private short _clientConnected = 0;

        public override Task OnConnected()
        {
            _clientConnected += 1;
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _clientConnected -= 1;
            this.Clients.All.onHitRecorded(_clientConnected);
            return base.OnDisconnected(stopCalled);
        }

        public void getApplicationEvent()
        {
            while (true)
            {
                var events = GetEventLogs();
                this.Clients.All.onHitRecorded(JsonConvert.SerializeObject(events));
                Thread.Sleep(1000 * 30);
            }
        }

        private IEnumerable<EventDetail> GetEventLogs(string eventtype = "Application")
        {
            EventLog log = new EventLog(eventtype);
            var eventDetail = log.Entries.Cast<EventLogEntry>()
                .Where(x => x.EntryType == EventLogEntryType.Error).Take(20)
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
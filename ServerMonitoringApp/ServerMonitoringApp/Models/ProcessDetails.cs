namespace ServerMonitoringApp.Models
{
    public class ProcessDetails
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public long VirtualMemory { get; set; }

        public long MemorySize { get; set; }
    }
}
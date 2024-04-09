namespace ForwardProxy
{
    internal class ProxyServerLog
    {
        private List<LogEntry> _log;

        public ProxyServerLog()
        {
            _log = new List<LogEntry>();
        }

        public void AddLogEntry(LogEntry logEntry) 
        {
            _log.Add(logEntry);
        }

        public List<LogEntry> GetLog()
        {
            return _log;
        }

        public void Clear()
        {
            _log.Clear();
        }
    }
}

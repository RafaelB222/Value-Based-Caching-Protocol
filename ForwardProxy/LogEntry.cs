namespace ForwardProxy
{
    internal class LogEntry
    {
        private string _fileName;
        private DateTime _timeStamp;
        private string _requestMessage;
        private string _responseMessage;
        private double _percentOfFileUsed;

        public LogEntry(string filename, double percent)
        {
            _fileName = filename;
            _timeStamp = DateTime.Now;
            _percentOfFileUsed = percent;
            _requestMessage = "User requested file " + filename + " at " + _timeStamp;
            _responseMessage = "Response: " + _percentOfFileUsed + "% of file " + filename + " was constructed with the cached data and sent to user";

        }
       
        public string GetRequestMessage()
        {
            return _requestMessage;
        }

        public string GetResponseMessage()
        {
            return _responseMessage;
        }

        public string GetFileName()
        {
            return _fileName;
        }

        public DateTime GetTimeStamp()
        {
            return _timeStamp;
        }

        
        public override string ToString()
        {
            return _requestMessage + Environment.NewLine + _responseMessage;
        }
    }
}

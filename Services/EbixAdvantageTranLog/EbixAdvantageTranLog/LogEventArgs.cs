using System;


namespace EbixAdvantageTranLog
{
    /// <summary>
    /// Summary description for LogEventArgs.
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        public enum LogStatus { SUCCESS, ERROR };
        public LogStatus status;
        public Exception ActualException = null;
        public LogEventArgs(LogStatus status)
            : base()
        {
            this.status = status;
        }
        public LogEventArgs(LogStatus status, Exception ActualException)
            : this(status)
        {
            this.status = status;
            this.ActualException = ActualException;
        }
    }
}

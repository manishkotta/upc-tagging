namespace Common.CommonEntities
{
    public class ExceptionLoggerEntity
    {
        public  string StackTrace { get; set; }

        public string ExceptionType { get; set; }

        public  string Source { get; set; }

        public  string Message { get; set; }

        public int HResult { get;  set; }

        public  string HelpLink { get; set; }
    }
}

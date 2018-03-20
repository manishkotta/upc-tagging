using Common.CommonEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.CommonUtilities
{
    public class LogException
    {

        public List<ExceptionLoggerEntity> ExceptionGroup { get; set; }

        public LogException()
        {
            ExceptionGroup = new List<ExceptionLoggerEntity>();
        }

        public void ExceptionLogger(Exception ex)
        {
            ExceptionGroup.Add(new ExceptionLoggerEntity
            {
                HelpLink = ex.HelpLink,
                HResult = ex.HResult,
                Message = ex.Message,
                Source = ex.Source,
                StackTrace = ex.StackTrace,
                ExceptionType = ex.GetType()?.ToString()
            });

            if (ex.InnerException != null)
                return;

            ExceptionLogger(ex.InnerException);
        }
    }
}

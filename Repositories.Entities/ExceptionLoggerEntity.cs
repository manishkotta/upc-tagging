using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repositories.Entities
{
    public class ExceptionLoggerEntity
    {
        [Column("exceptionid")]
        public int ExceptionID { get; set; }

        [Column("stacktrace")]
        public string StackTrace { get; set; }

        [Column("exceptiontype")]
        public string ExceptionType { get; set; }

        [Column("source")]
        public string Source { get; set; }

        [Column("message")]
        public string Message { get; set; }

        [Column("hresult")]
        public int HResult { get; set; }

        [Column("helplink")]
        public string HelpLink { get; set; }

        [Column("exceptionparentid")]
        public int? ExceptionParentID { get; set; }

    }
}

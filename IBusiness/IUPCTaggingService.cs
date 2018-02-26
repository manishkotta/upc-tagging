using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace IBusiness
{
    public interface IUPCTaggingService
    {
        void SaveFileToTable(DataTable dt,string seperator);
       
    }
}

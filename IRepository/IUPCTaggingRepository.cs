using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IUPCTaggingRepository
    {
        bool BulkCopyToDB(IEnumerable<string> strArray);

        bool ExecuteStoreProc(string cmdTxt, IDictionary<string,object> parameters);
    }
}

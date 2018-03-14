using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Common.CommonUtilities;
using IBusiness;
using IRepository;

namespace BusinessProvider
{
    public class UPCTaggingService : IUPCTaggingService
    {
        IUPCTaggingRepository _upcRepo;
        public UPCTaggingService(IUPCTaggingRepository upcRepo)
        {
            _upcRepo = upcRepo;
        }
        public void SaveFileToTable(DataTable dt, string seperator)
        {
            var value = Utilities.DataTableToCSV(dt, seperator);
            _upcRepo.BulkCopyToDB(value);
        }

        public bool CaptureUntaggedUPC()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("userid", 1764);
            _upcRepo.ExecuteStoreProc("captureuntaggedupc", dict);
            return true;
        }
    }
}

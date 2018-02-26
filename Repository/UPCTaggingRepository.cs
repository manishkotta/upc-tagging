using IRepository;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Repository
{
    public class UPCTaggingRepository : IUPCTaggingRepository
    {

        public bool BulkCopyToDB(IEnumerable<string> strArray)
        {
            try
            {
                var connString = "Host=localhost;Username=postgres;Password=Welcome2ggk;Database=nasgw_upc_tagging";

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (var writer = conn.BeginTextImport($@"copy temp_upc (""upc"",""description"",""product_type"",""product_category"",""product_subcategory"",""product_sizing"") from STDIN"))
                    {
                        //select tmp.desc_id,upc,tmp.description from temp_upc tmp Inner JOIN upc_lookup upclookup on upc_code != upc
                        foreach (var s in strArray)
                        {
                            writer.WriteLine(s);
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                var f = ex;
            }
            return false;
        }

    }
}

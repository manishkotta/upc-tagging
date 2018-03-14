using IRepository;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using Microsoft.Extensions.Configuration;
using Common.CommonUtilities;

namespace Repository
{
    public class UPCTaggingRepository : IUPCTaggingRepository
    {
        public IConfiguration Configuration { get; }
        public UPCTaggingRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public bool BulkCopyToDB(IEnumerable<string> strArray)
        {
               var connString = Configuration[Constants.PostgresqlConnStr];

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (var writer = conn.BeginTextImport($@"copy TempUPC (""descriptionid"",""upccode"",""description"") from STDIN"))
                    {
                        foreach (var s in strArray)
                        {
                            writer.WriteLine(s);
                        }
                    }

                }
            return true;
        }

        public bool ExecuteStoreProc(string cmdText,IDictionary<string,object> parameters)
        {
                var connString = Configuration[Constants.PostgresqlConnStr];

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                   
                        using (var command = conn.CreateCommand())
                        {
                            command.CommandText = cmdText; //"SELECT capture_untagged_upcs";
                            command.CommandType = CommandType.StoredProcedure;

                            foreach (var p in parameters)
                            {
                                var param = command.CreateParameter();
                                param.ParameterName = p.Key;
                                param.Value = p.Value;
                                command.Parameters.Add(param);
                            }

                            var upc = command.ExecuteScalar();
                        }
                }
            return true;
        }

    }
}

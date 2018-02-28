using IRepository;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using Microsoft.Extensions.Configuration;
using CommonEntities;

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
            try
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
            }
            catch(Exception ex)
            {
                var f = ex;
                return false;
            }
            return true;
        }

        public bool ExecuteStoreProc(string cmdText,IDictionary<string,object> parameters)
        {
            try
            {
                var connString = Configuration[Constants.PostgresqlConnStr];

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
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

                            command.ExecuteScalar();
                        }
                        tran.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using ExcelDataReader;
using Common.CommonEntities;

namespace Common.CommonUtilities
{
    public static class Utilities
    {
        public static IEnumerable<string> DataTableToCSV(DataTable datatable, string seperator)
        {
            StringBuilder sb = new StringBuilder();

            foreach (DataRow dr in datatable.Rows)
            {
                for (int i = 0; i < 3; i++)
                {
                    sb.Append(dr[i].ToString().Replace('\t', ' '));

                    if (i < 3 - 1)
                        sb.Append(seperator);
                }
                sb.Append("\n");
            }
            var splittedRows = sb.ToString().Split("\n");
            return splittedRows.SkipLast(1);
        }

        public static DataTable ExcelToDataTable(string extension, System.IO.Stream stream)
        {
            IExcelDataReader reader;

            if (extension.Equals(".xls"))
                reader = ExcelReaderFactory.CreateBinaryReader(stream);
            else if (extension.Equals(".xlsx"))
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            else
                return null;

            var conf = new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true
                }
            };

            var dataSet = reader.AsDataSet(conf);
            if (dataSet.Tables.Count <= 0) return null;
            var dataTable = dataSet.Tables[0];
            if (dataTable.Rows.Count <= 0) return null;

            return dataTable;
        }

        public static string AppendWhereOrAnd(bool isWhereAppended) => isWhereAppended ? "AND" : "WHERE";

        public static string GetRoleName(int roleID)
        {
            if (roleID == (int)Role.Admin) return "Admin";
            else if (roleID == (int)Role.User) return "User";
            else return string.Empty;
        }
    }




}

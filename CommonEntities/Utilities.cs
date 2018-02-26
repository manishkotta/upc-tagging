using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace CommonEntities
{
    public static class Utilities
    {
        public static IEnumerable<string> DataTableToCSV(DataTable datatable, string seperator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in datatable.Rows)
            {
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    sb.Append(dr[i].ToString().Replace('\t',' '));

                    if (i < datatable.Columns.Count - 1)
                        sb.Append(seperator);
                }
                sb.Append("\n");
            }
            var splittedRows = sb.ToString().Split("\n");
            return splittedRows.SkipLast(1);
        }
    }
}

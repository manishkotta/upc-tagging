using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using IRepository;
using ExcelDataReader;
using System.IO;

namespace Repository
{
    public class ExcelDataProvider : IDataProvider
    {
        /// <summary>
        /// Method to retirve data from Excel file
        /// </summary>
        /// <returns></returns>
        public DataTable Retrive(string filePath)
        {
            var file = new FileInfo(filePath);
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader reader;

                if (file.Extension.Equals(".xls"))
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                else if (file.Extension.Equals(".xlsx"))
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                else
                    throw new Exception("Invalid FileName");

                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };

                
                var dataSet = reader.AsDataSet(conf);
                var dataTable = dataSet.Tables[0];


            }
            return null;
        }
    }
}

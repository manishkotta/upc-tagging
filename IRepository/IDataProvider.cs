using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IRepository
{
    public interface IDataProvider
    {
        /// <summary>
        /// Method to retrive data from data source.
        /// </summary>
        /// <returns>Datatable</returns>
        DataTable Retrive(string filePath);

    }
}

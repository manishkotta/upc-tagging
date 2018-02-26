using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Net;
using System.Net.Http;
using System.IO;
using IBusiness;
using Microsoft.AspNetCore.Cors;
using ExcelDataReader;
using System.Data;
using System.Diagnostics;

namespace UPCTaggingInterface.Controllers
{
    [Produces("application/json")]
    [Route("api/upload-document")]
    public class UploadDocumentController : Controller
    {
        IUPCTaggingService _upcTaggingService;
        public UploadDocumentController(IUPCTaggingService upcTaggingService)
        {
            _upcTaggingService = upcTaggingService;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> UploadDocument()
        {
            try
            {
                //Stopwatch stopWatch = new Stopwatch();
                //stopWatch.Start();

                var files = Request.Form.Files;
                if (files.Count() <= 0) return new HttpResponseMessage(HttpStatusCode.NoContent);
                var rootFolder = Directory.GetCurrentDirectory();
                var file= files[0];
                var stream = file.OpenReadStream();

                var extension = Path.GetExtension(file.FileName);

                IExcelDataReader reader;

                if (extension.Equals(".xls"))
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                else if (extension.Equals(".xlsx"))
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                else
                    return new HttpResponseMessage(HttpStatusCode.NotAcceptable);

                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };

                var dataSet = reader.AsDataSet(conf);
                if(dataSet.Tables.Count <= 0 ) return new HttpResponseMessage(HttpStatusCode.NoContent);
                var dataTable = dataSet.Tables[0];
                if (dataTable.Rows.Count <= 0) return new HttpResponseMessage(HttpStatusCode.NoContent);

                _upcTaggingService.SaveFileToTable(dataTable,"\t");


                //stopWatch.Stop();
                
                //TimeSpan ts = stopWatch.Elapsed;

                //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                //    ts.Hours, ts.Minutes, ts.Seconds,
                //    ts.Milliseconds / 10);
            }
            catch(Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
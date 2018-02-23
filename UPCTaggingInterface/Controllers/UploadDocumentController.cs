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

namespace UPCTaggingInterface.Controllers
{
    [Produces("application/json")]
    [Route("api/upload-document")]
    public class UploadDocumentController : Controller
    {
        IFileService _saveFileService;
        public UploadDocumentController(IFileService saveFileService)
        {
            _saveFileService = saveFileService;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> UploadDocument()
        {
            var files = Request.Form.Files;
            if (files.Count() <= 0) return new HttpResponseMessage(HttpStatusCode.NoContent);
            var rootFolder = Directory.GetCurrentDirectory();
            var filePath = rootFolder + "\\TemporaryFiles\\" + files[0].FileName;
            var stream = files[0].OpenReadStream();
            _saveFileService.SaveFileToTempFolder(stream,filePath);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
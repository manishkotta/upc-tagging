﻿using System;
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
using System.Data;
using System.Diagnostics;
using Common.CommonUtilities;
using Microsoft.AspNetCore.Authorization;

namespace UPCTaggingInterface.Controllers
{
    [Produces("application/json")]
    [Route("api/upload-document")]
    [Authorize]
    public class UploadDocumentController : Controller
    {
        IUPCTaggingService _upcTaggingService;
        public UploadDocumentController(IUPCTaggingService upcTaggingService)
        {
            _upcTaggingService = upcTaggingService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadDocument()
        {
                //Stopwatch stopWatch = new Stopwatch();
                //stopWatch.Start();

                var role = HttpContext.User.Claims.Where(s => s.Type == Constants.AuthConstants.UserRole).FirstOrDefault();
                var user = HttpContext.User.Claims.Where(s => s.Type == Constants.AuthConstants.UserId).FirstOrDefault();
                var roleID  = Convert.ToInt32(role?.Value);
                var userID = Convert.ToInt32(user?.Value);

                if (roleID != (int)Role.Admin) return Unauthorized();
                        
                var files = Request.Form.Files;
                if (files.Count() <= 0) return BadRequest(Constants.BadRequestErrorMessage);
                var rootFolder = Directory.GetCurrentDirectory();
                var file= files[0];
                var stream = file.OpenReadStream();

                var extension = Path.GetExtension(file.FileName);
                var dataTable = Utilities.ExcelToDataTable(extension, stream);

                if (dataTable.IsNullOrEmpty()) return BadRequest(Constants.BadRequestErrorMessage);

                _upcTaggingService.SaveFileToTable(dataTable,"\t");
                return Ok(_upcTaggingService.CaptureUntaggedUPC(userID));

                //stopWatch.Stop();

                //TimeSpan ts = stopWatch.Elapsed;

                //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                //    ts.Hours, ts.Minutes, ts.Seconds,
                //    ts.Milliseconds / 10);
        }
    }
}
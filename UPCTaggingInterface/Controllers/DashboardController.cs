using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IBusiness;
using Repositories.Entities;

namespace UPCTaggingInterface.Controllers
{
    [Produces("application/json")]
    [Route("api/dashboard")]
    public class DashboardController : Controller
    {
        protected IUntaggedUPCService _untaggedUPCService;
        public DashboardController(IUntaggedUPCService untaggedUPCService)
        {
            _untaggedUPCService = untaggedUPCService;
        }

        [HttpGet]
        [Route("get-untagged-upc")]
        public async Task<List<UntaggedUPC>> GetUntaggedUPCList()
        {
            var result = await _untaggedUPCService.GetUPCList();
            return result.Value;
        }
    }
}
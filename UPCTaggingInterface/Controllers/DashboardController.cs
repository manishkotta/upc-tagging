using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IBusiness;
using Business.Entities;
using Repositories.Entities;

namespace UPCTaggingInterface.Controllers
{
    [Produces("application/json")]
    [Route("api/dashboard")]
    public class DashboardController : Controller
    {
        protected IUntaggedUPCService _untaggedUPCService;
        protected ICommonService _commonService;
        public DashboardController(IUntaggedUPCService untaggedUPCService,ICommonService commonService)
        {
            _untaggedUPCService = untaggedUPCService;
            _commonService = commonService;
        }

        [HttpPost]
        [Route("get-untagged-upc")]
        public async Task<List<UntaggedUPCBusinessModal>> GetUntaggedUPCList([FromBody] UPCSearchFilter filter)
        {
            return  (await _untaggedUPCService.GetUPCList()).Value;
        }

        [HttpGet]
        [Route("get-producttype")]
        public async Task<List<ProductType>> GetTypeGroup() => (await _commonService.GetTypeGroup()).Value;
 
        [HttpGet]
        [Route("get-productcategory")]
        public async Task<List<ProductCategory>> GetCategoryGroup() => (await _commonService.GetProductCategoryGroup()).Value;
     
        [HttpGet]
        [Route("get-productsubcategory")]
        public async Task<List<ProductSubCategory>> GetSubCategoryGroup() => (await _commonService.GetProductSubCategoryGroup()).Value;

    }
}
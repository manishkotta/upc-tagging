using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IBusiness;
using Business.Entities;
using Common.CommonEntities;
using Common.CommonUtilities;

namespace UPCTaggingInterface.Controllers
{
    [Produces("application/json")]
    [Route("api/dashboard")]
    public class DashboardController : Controller
    {
        protected IUntaggedUPCService _untaggedUPCService;
        protected ICommonService _commonService;
        protected ITaggedUPCService _taggedUPCService;
        public DashboardController(IUntaggedUPCService untaggedUPCService,ICommonService commonService,ITaggedUPCService taggedUPCService)
        {
            _untaggedUPCService = untaggedUPCService;
            _commonService = commonService;
            _taggedUPCService = taggedUPCService;
        }

        [HttpPost]
        [Route("untagged-upc")]
        public async Task<IActionResult> GetUntaggedUPCList([FromBody] UPCSearchFilter filter)
        {
            var result = (await _untaggedUPCService.GetUPCList(filter));
            if (!result.IsSuccessed) return BadRequest(Constants.BadRequestErrorMessage);
                return Ok(result.Value);
        }


        [HttpPost]
        [Route("tagged-upc")]
        public async Task<IActionResult> GetTaggedUPCList([FromBody] UPCSearchFilter filter)
        {
            var result = (await _taggedUPCService.GetUPCList(filter));
            if (!result.IsSuccessed) return BadRequest(Constants.BadRequestErrorMessage);
            return Ok(result.Value);
        }

        [HttpPost]
        [Route("update-untagged-upc")]
        public async Task<IActionResult> UpdateUntaggedUPC([FromBody] UntaggedUPCBusinessModal untaggedUPCBusinessModal)
        {
            var result = (await _untaggedUPCService.UpdateUntaggedUPC(untaggedUPCBusinessModal, 1764));
            if (!result.IsSuccessed) return BadRequest(result.GetErrorString());
            return Ok(result.Value);
        }


        [HttpGet]
        [Route("product-type")]
        public async Task<IActionResult> GetTypeGroup() => Ok((await _commonService.GetTypeGroup()).Value);
 
        [HttpGet]
        [Route("product-category")]
        public async Task<IActionResult> GetCategoryGroup() => Ok((await _commonService.GetProductCategoryGroup()).Value);
     
        [HttpGet]
        [Route("product-subcategory")]
        public async Task<IActionResult> GetSubCategoryGroup() => Ok((await _commonService.GetProductSubCategoryGroup()).Value);

        [HttpGet]
        [Route("non-admins")]
        public async Task<IActionResult> GetUsers()
        {
           var result =  await _commonService.GetUsersWhoCanTag();
            if (result.IsSuccessed) return Ok(result.Value.Select(s=>new { s.Name, s.UserID }));
            return BadRequest(result.GetErrorString());
        }

    }
}
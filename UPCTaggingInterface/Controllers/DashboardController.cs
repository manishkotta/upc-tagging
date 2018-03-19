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
using ViewModel.Entities;
using Microsoft.AspNetCore.Authorization;

namespace UPCTaggingInterface.Controllers
{
    [Produces("application/json")]
    [Route("api/dashboard")]
    [Authorize]
    public class DashboardController : Controller
    {
        protected IUntaggedUPCService _untaggedUPCService;
        protected ICommonService _commonService;
        protected ITaggedUPCService _taggedUPCService;

        protected string GetValueFromClaim(string type) => HttpContext.User.Claims.Where(s=>s.Type == type).FirstOrDefault()?.Value;


        public DashboardController(IUntaggedUPCService untaggedUPCService,ICommonService commonService,
            ITaggedUPCService taggedUPCService)
        {
            _untaggedUPCService = untaggedUPCService;
            _commonService = commonService;
            _taggedUPCService = taggedUPCService;
        }

        [HttpPost]
        [Route("untagged-upc")]
        public async Task<IActionResult> GetUntaggedUPCList([FromBody] UPCSearchFilter filter)
        {
            try
            {
                filter.RoleID = Convert.ToInt32(GetValueFromClaim(Constants.AuthConstants.UserRole));
                filter.UserID = Convert.ToInt32(GetValueFromClaim(Constants.AuthConstants.UserId));

                if (filter.RoleID != (int)Role.Admin && filter.RoleID != (int)Role.User) return Unauthorized();

                var result = (await _untaggedUPCService.GetUPCList(filter));
                if (!result.IsSuccessed) return BadRequest(Constants.BadRequestErrorMessage);
                return Ok(result.Value);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [Route("tagged-upc")]
        public async Task<IActionResult> GetTaggedUPCList([FromBody] UPCSearchFilter filter)
        {
            var roleId = Convert.ToInt32(GetValueFromClaim(Constants.AuthConstants.UserRole));
            if (roleId != (int)Role.Admin && roleId != (int)Role.User) return Unauthorized();
            var result = (await _taggedUPCService.GetUPCList(filter));
            if (!result.IsSuccessed) return BadRequest(Constants.BadRequestErrorMessage);
            return Ok(result.Value);
        }

        [HttpPost]
        [Route("update-untagged-upc")]
        public async Task<IActionResult> UpdateUntaggedUPC([FromBody] UntaggedUPCBusinessModal untaggedUPCBusinessModal)
        {
            var roleId = Convert.ToInt32(GetValueFromClaim(Constants.AuthConstants.UserRole));
            if (roleId != (int)Role.Admin) return Unauthorized();
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
            var roleId = Convert.ToInt32(GetValueFromClaim(Constants.AuthConstants.UserRole));
            if (roleId != (int)Role.Admin) return Unauthorized();
            var result =  await _commonService.GetUsersWhoCanTag();
            if (result.IsSuccessed) return Ok(result.Value.Select(s=>new { s.Name, s.UserID }));
            return BadRequest(result.GetErrorString());
        }

        
        [HttpPost]
        [Route("assign-untagged-upc")]
        public async Task<IActionResult> AssignUntaggedUPCToUser([FromBody] AssignUntagUpcDTO upcDTO)
        {
            var roleId = Convert.ToInt32(GetValueFromClaim(Constants.AuthConstants.UserRole));
            if (roleId != (int)Role.Admin) return Unauthorized();

            if (upcDTO == null) return BadRequest(Constants.BadRequestErrorMessage);
            else if (upcDTO.untaggedUPCIDs.Count() <= 0) return BadRequest(Constants.BadRequestErrorMessage);
            else if (upcDTO.user?.UserID == 0) return BadRequest(Constants.BadRequestErrorMessage);
            var result = await _untaggedUPCService.AssignUserToUntaggedUPC(upcDTO.untaggedUPCIDs, upcDTO.user, 1764);
            if (result.IsSuccessed) return Ok();
            return BadRequest(result.GetErrorString());
        }

        [HttpPost]
        [Route("approve-saved-upc")]
        public async Task<IActionResult> ApproveSavedUPC([FromBody] int[] savedUPC)
        {
            var roleId = Convert.ToInt32(GetValueFromClaim(Constants.AuthConstants.UserRole));
            if (roleId != (int)Role.Admin) return Unauthorized();

            var result = await _commonService.ApprovedSavedUPC(savedUPC, 1764);
            if (!result.IsSuccessed) return BadRequest(result.GetErrorString());
            return Ok();
        }

    }
}
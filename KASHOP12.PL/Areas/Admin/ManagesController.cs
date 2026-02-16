using KASHOP12.BLL.Service;
using KASHOP12.DAL.Data.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP12.PL.Areas.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class ManagesController : ControllerBase
    {
        private readonly IManageUser _manageUser;

        public ManagesController(IManageUser manageUser)
        {
            _manageUser = manageUser;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _manageUser.GetUsersAsync();
            return Ok(result);
        }
        [HttpPatch("blocked/{id}")]
        public async Task<IActionResult> BlockUser([FromRoute] string id)
       => Ok(await _manageUser.BlockedUserAsync(id));


        [HttpPatch("unblocked/{id}")]
        public async Task<IActionResult>UnBlockUser([FromRoute] string id)
=> Ok(await _manageUser.UnBlockedUserAsync(id));


        [HttpPatch("change-role")]
        public async Task<IActionResult> ChangeRole(ChangeUserRoleRequest request)
            => Ok(await _manageUser.ChangeUserRoleAsync(request));
    }
}

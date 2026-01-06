using System.Security.Claims;
using KASHOP12.BLL.Service;
using KASHOP12.DAL.Data.DTO.Request;
using KASHOP12.PL.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOP12.PL.Areas.Admin
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]


    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _category;
        private readonly IStringLocalizer _localizer;

        public CategoriesController(ICategoryService category, IStringLocalizer<SharedResource> localizer)
        {
            _category = category;
            _localizer = localizer;
        }

    

        [HttpPost("")]

        public async Task <IActionResult> Crerat([FromBody]CategoryRequest request)
        {
           // var CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Console.WriteLine("user id is :");
            //Console.WriteLine(CreatedBy);
            var response =await _category.CreateCategory(request);
            return Ok(new { message = _localizer["Success"].Value });
        }


        [HttpGet("")]
        public async Task <IActionResult> Index()
        {
            var response= await _category.GetAllCategories();
            return Ok(new { message = _localizer["Success"].Value,response});
        }


        [HttpPatch("toggle-ststus/{id}")]
        public async Task <IActionResult> ToggleStatus(int id)
        {
            var result = await _category.ToggleStatus(id);
            if (!result.Success)
            {
                if (result.Message.Contains("Not Found"))
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletCategory([FromRoute] int id)
        {
            var result = await _category.DeleteCategoryAsync(id);
            if (!result.Success)
            {
                if(result.Message.Contains("Not Found"))
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }



        [HttpPatch("{id}")]
        public async Task<IActionResult>UpdateCategory([FromRoute]int id,[FromBody]CategoryRequest request)
        {
            var result = await _category.UpdateCategoryAsync(id, request);
            if (!result.Success)
            {
                if (result.Message.Contains("Not Found"))
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }
            return Ok(result);

        }

    }
}


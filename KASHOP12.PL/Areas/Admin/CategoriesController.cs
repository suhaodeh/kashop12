using KASHOP12.BLL.Service;
using KASHOP12.DAL.Data.DTO.Request;
using KASHOP12.PL.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Crerate(CategoryRequest request)
        {
            var response = _category.CreateCategory(request);
            return Ok(new { message = _localizer["Success"].Value });
        }

    }
}


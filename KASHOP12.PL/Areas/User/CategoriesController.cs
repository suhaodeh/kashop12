using KASHOP12.BLL.Service;
using KASHOP12.DAL.Data.DTO.Request;
using KASHOP12.PL.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOP12.PL.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _category;
        private readonly IStringLocalizer _localizer;

        public CategoriesController(ICategoryService category,IStringLocalizer<SharedResource> localizer)
        {
            _category = category;
            _localizer = localizer;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var response = _category.GetAllCategories();
            return Ok(new { message = _localizer["Success"].Value, response });
        }

    

    }
}

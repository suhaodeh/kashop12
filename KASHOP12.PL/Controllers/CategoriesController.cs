using Azure;
using KASHOP12.BLL.Service;
using KASHOP12.DAL.Data;
using KASHOP12.DAL.Data.DTO.Request;
using KASHOP12.DAL.Data.DTO.Response;
using KASHOP12.DAL.Models;
using KASHOP12.DAL.Repository;
using KASHOP12.PL.Resources;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace KASHOP12.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
      
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly ICategoryService _categoryService;

        public CategoriesController( IStringLocalizer<SharedResource> localizer,ICategoryService categoryService)
        {
        
            _localizer = localizer;
            _categoryService = categoryService;
        }

        [HttpGet ("")]
        public IActionResult index()
        {
            var response = _categoryService.GetAllCategories();
    
            return Ok(new {Message = _localizer["Done"].Value,response });
        }

        [HttpPost("")]
        public IActionResult Create(CategoryRequest request)
        {
            var response = _categoryService.CreateCategory(request);
         
            return Ok(new { Message = _localizer["Success"].Value});

        }

      
    }
}

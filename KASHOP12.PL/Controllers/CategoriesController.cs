using Azure;
using KASHOP12.DAL.Data;
using KASHOP12.DAL.Data.DTO.Request;
using KASHOP12.DAL.Data.DTO.Response;
using KASHOP12.DAL.Models;
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
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CategoriesController( ApplicationDbContext context,IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        [HttpGet ("")]
        public IActionResult index()
        {
            var categories = _context.Categories.Include(c => c.Translations).ToList();
            var response = categories.Adapt<List<CategoryResponse>>();
            return Ok(new {Message = _localizer["Done"].Value,response });
        }

        [HttpPost("")]
        public IActionResult Create(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            _context.Add(category);
            _context.SaveChanges();
            return Ok(new { Message = _localizer["Success"].Value});

        }

      
    }
}

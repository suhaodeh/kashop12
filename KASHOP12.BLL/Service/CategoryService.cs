using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data.DTO.Request;
using KASHOP12.DAL.Data.DTO.Response;
using KASHOP12.DAL.Models;
using KASHOP12.DAL.Repository;
using Mapster;

namespace KASHOP12.BLL.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryResponse> CreateCategory(CategoryRequest Request)
        {
            var category = Request.Adapt<Category>();
          await  _categoryRepository.CreateAsync(category);
            return category.Adapt<CategoryResponse>();
        }



        public async Task< List<CategoryResponse>> GetAllCategories()
        {
            var categories =await _categoryRepository.GetAllAsync();

            var response = categories.Adapt<List<CategoryResponse>>();
            return response;



        }


        public async Task<BaseResponse>ToggleStatus(int id)
        {
            try
            {
                var category = await _categoryRepository.FindByIdAsync(id);
                if (category is null)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Category Not Found",
                    };
                }
                category.Status = category.Status == Status.Active ? Status.InActive : Status.Active;
                await _categoryRepository.UpdateAsync(category);
                return new BaseResponse
                {
                    Success = true,
                    Message = "Category ststus updated successfully"
                };


                }catch(Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "can not delete category",
                    Errors = new List<string> { ex.Message }
                };

            }
        }

        public async Task<BaseResponse> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.FindByIdAsync(id);
                if(category is null)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Category Not Found",
                    };
                }
                await _categoryRepository.DeleteAsync(category);
                return new BaseResponse
                {
                    Success = true,
                    Message = "Category deleted successfully",
                };
            }
            
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "can not delete category",
                    Errors=new List<string> { ex.Message}
                };
            }

           
        }
        public async Task<BaseResponse> UpdateCategoryAsync(int id,CategoryRequest request)
        {
            try
            {
                var category = await _categoryRepository.FindByIdAsync(id);
                if(category is null)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Category Not Found",
                    };
                }
                if (request.Translations != null)
                {
                    foreach (var translation in request.Translations)
                    {
                        var existing = category.Translations.FirstOrDefault(t => t.Language == translation.Language);
                        if (existing is not null)
                        {
                            existing.Name = translation.Name;
                        }
                        else
                        {
                            category.Translations.Add(new CategoryTranslation
                            {
                                Name = translation.Name,
                                Language = translation.Language,
                            });
                        }

                    } }
                await _categoryRepository.UpdateAsync(category);
                return new BaseResponse
                    
                        {
                        Success = true,
                        Message = "Category updated successfully"
                    };
                    
                

            }catch(Exception ex)
            {
                return new BaseResponse

                {
                    Success = false,
                    Message = " can not delete Category ",
                    Errors=new List<string> { ex.Message}
                };
            }
        }
        }
    }


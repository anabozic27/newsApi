using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using News.BL.Interfaces;
using News.BL.Models.Request;
using News.BL.Models.Response;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseApiController
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Method for retrieving list of all categories
        /// </summary>
        /// <returns>List of categories</returns>
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<CategoryViewModel>> GetAll()
        {
            return await _categoryService.GetAll();
        }

        /// <summary>
        /// Method for retrieving list of categories by their parent category
        /// </summary>
        /// <param name="parentCategoryId"></param>
        /// <returns>List of categories</returns>
        [HttpGet]
        [Route("getByParentCategory/{parentCategoryId}")]
        public async Task<IEnumerable<CategoryViewModel>> GetByParentCategoryId(int parentCategoryId)
        {
            return await _categoryService.GetByParentCategoryID(parentCategoryId);
        }

        /// <summary>
        /// Method for retrieving list of categories of user
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>List of categories</returns>
        [HttpGet]
        [Route("getUserCategoriesForArticleId/{categoryId}")]
        [Authorize]
        public async Task<IEnumerable<CategoryViewModel>> GetUserCategoriesForArticleId(int categoryId)
        {
            return await _categoryService.GetUserCategoriesForArticleId(categoryId);
        }

        /// <summary>
        /// Method for retrieving category data by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Model with category data</returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<CategoryViewModel> GetById(int id)
        {
            return await _categoryService.GetById(id);
        }

        /// <summary>
        /// Method for inserting or updating category
        /// </summary>
        /// <remarks>
        ///     POST / CategoryModel
        ///     {
        ///         "id": 0,
        ///         "categoryName": "test",
        ///         "parentCategoryId": null,
        ///         "active": 1
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<int> Save([FromBody]CategoryModel model)
        {
            return await _categoryService.Save(model);
        }

        /// <summary>
        /// Method for deleting category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<bool> Delete(int id)
        {
            return await _categoryService.Delete(id);
        }
    }
}
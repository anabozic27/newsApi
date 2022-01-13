using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.BL.Interfaces;
using News.BL.Models.Request;
using News.BL.Models.Response;
using News.Models.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace News.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : BaseApiController
    {
        private IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        /// <summary>
        /// Method for retrieving all articles
        /// </summary>
        /// <returns>List of articles</returns>
        [HttpGet]        [Route("")]        public async Task<IEnumerable<ArticleViewModel>> GetAll()        {            return await _articleService.GetAll();
        }

        /// <summary>
        /// Method for retrieving articles by their category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>List of articles</returns>
        [HttpGet]
        [Route("getByCategory/{categoryId}")]
        public async Task<IEnumerable<ArticleViewModel>> GetByCategoryId(int categoryId)
        {
            return await _articleService.GetByCategoryId(categoryId);
        }

        /// <summary>
        /// Method for retrieving articles of user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of articles</returns>
        [HttpGet]
        [Route("getByUser/{userId}")]
        public async Task<IEnumerable<ArticleViewModel>> GetByUserId(int userId)
        {
            return await _articleService.GetByUserId(userId);
        }

        /// <summary>
        /// Method for retrieving article data by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Model with article data</returns>
        [HttpGet]
        [Route("getById/{id}")]
        public async Task<ArticleViewModel> GetById(int id)
        {
            return await _articleService.GetById(id);
        }

        /// <summary>
        /// Method for inserting or updating article
        /// </summary>
        /// <remarks>
        ///     POST / ArticleModel
        ///     {
        ///         "id": 0,
        ///         "title": "test",
        ///         "text": "test",
        ///         "categoryId": 1,
        ///         "createByUserId": 1,
        ///         "createdDate": "2022-01-12",
        ///         "modifiedByUserId": null,
        ///         "modifiedDate": null
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <param name="authorization"></param>
        /// <returns>Id of saved article</returns>
        [HttpPost]
        [Authorize]
        public async Task<int> Save([FromBody]ArticleModel model, [FromHeader]string authorization)
        {
            model.CreateByUserId = GetUserIdFromToken(authorization);
            return await _articleService.Save(model);
        }

        /// <summary>
        /// Method for deleting article
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<bool> Delete(int id)
        {
            return await _articleService.Delete(id);
        }

        
    }
}
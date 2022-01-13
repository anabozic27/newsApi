using News.BL.Models.Request;
using News.BL.Models.Response;
using News.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace News.BL.Interfaces
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleViewModel>> GetAll();
        Task<IEnumerable<ArticleViewModel>> GetByCategoryId(int categoryId);
        Task<IEnumerable<ArticleViewModel>> GetByUserId(int userId);
        Task<ArticleViewModel> GetById(int id);
        Task<int> Save(ArticleModel model);
        Task<bool> Delete(int id);
    }
}

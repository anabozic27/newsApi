using News.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace News.DAL.Interfaces
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAll();
        Task<IEnumerable<Article>> GetByCategoryId(int categoryId);
        Task<IEnumerable<Article>> GetByUserId(int userId);
        Task<Article> GetById(int id);
        Task<int> Save(Article model);
        Task<bool> Delete(int id);
    }
}

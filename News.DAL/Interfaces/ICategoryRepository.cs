using News.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int id);
        Task<IEnumerable<Category>> GetByParentCategoryID(int parentCategoryId);
        Task<IEnumerable<Category>> GetUserCategoriesForArticleId(int categoryId);
        Task<int> Save(Category model);
        Task<bool> Delete(int id);
    }
}

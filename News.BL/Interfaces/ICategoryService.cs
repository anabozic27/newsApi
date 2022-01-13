using News.BL.Models.Request;
using News.BL.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.BL.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAll();
        Task<CategoryViewModel> GetById(int id);
        Task<IEnumerable<CategoryViewModel>> GetByParentCategoryID(int parentCategoryId);
        Task<IEnumerable<CategoryViewModel>> GetUserCategoriesForArticleId(int categoryId);
        Task<int> Save(CategoryModel model);
        Task<bool> Delete(int id);
    }
}

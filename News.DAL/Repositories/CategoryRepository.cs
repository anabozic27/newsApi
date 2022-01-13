using Microsoft.EntityFrameworkCore;
using News.DAL.Interfaces;
using News.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private ArticleContext _context;

        public CategoryRepository(ArticleContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Category
                .Include(x => x.ParentCategory)
                .Where(x => x.Active)
                .ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _context.Category
                .Include(x => x.ParentCategory)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> GetByParentCategoryID(int parentCategoryId)
        {
            return await _context.Category
                .Include(x => x.ParentCategory)
                .Where(x => x.ParentCategoryId == parentCategoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetUserCategoriesForArticleId(int categoryId)
        {
            return await _context.Category
                .Include(x => x.ParentCategory)
                .Where(x => x.ParentCategoryId == categoryId || (x.Id == categoryId && x.ParentCategoryId != null))
                .ToListAsync();
        }

        public async Task<int> Save(Category model)
        {
            if (model.Id > 0)
            {
                var category = await _context.Category
                    .FirstOrDefaultAsync(x => x.Id == model.Id);
                
                category.Name = model.Name;
                category.ParentCategoryId = model.ParentCategoryId;
                category.Active = model.Active;

                _context.SaveChanges();
            }
            else
            {
                var newCategory = new Category
                {
                    Name = model.Name,
                    ParentCategoryId = model.ParentCategoryId,
                    Active = model.Active
                };
                
                _context.Category.Add(newCategory);
                _context.SaveChanges();
                
                var newCategoryId = newCategory.Id;
                model.Id = newCategoryId;
            }

            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
            
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            
            return true;
        }

    }
}

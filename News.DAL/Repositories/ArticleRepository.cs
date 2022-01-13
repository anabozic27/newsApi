using Microsoft.EntityFrameworkCore;
using News.DAL.Interfaces;
using News.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.DAL.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private ArticleContext _context;

        public ArticleRepository(ArticleContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await _context.Article
                .Include(x => x.Category)
                .Include(x => x.CreateByUser)
                .Include(x => x.ModifiedByUser)
                .Where(x => x.Active == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetByCategoryId(int categoryId)
        {
            return await _context.Article
                .Include(x => x.Category)
                .Include(x => x.CreateByUser)
                .Include(x => x.ModifiedByUser)
                .Where(x => x.CategoryId == categoryId && x.Active == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetByUserId(int userId)
        {
            return await _context.Article
                .Include(x => x.Category)
                .Include(x => x.CreateByUser)
                .Include(x => x.ModifiedByUser)
                .Where(x => x.CreateByUserId == userId && x.Active == true)
                .ToListAsync();
        }

        public async Task<Article> GetById(int id)
        {
            return await _context.Article
                .Include(x => x.Category)
                .Include(x => x.CreateByUser)
                .Include(x => x.ModifiedByUser)
                .Where(x => x.Id == id && x.Active == true)
                .FirstOrDefaultAsync();
        }

        public async Task<int> Save(Article model)
        {
            if(model.Id > 0)
            {
                var article = await _context.Article
                    .FirstOrDefaultAsync(x => x.Id == model.Id);
                
                article.Text = model.Text;
                article.Title = model.Title;
                article.CategoryId = model.CategoryId;
                article.ModifiedByUserId = model.ModifiedByUserId;
                article.ModifiedDate = model.ModifiedDate;
                article.Active = model.Active;

                _context.SaveChanges();
            }
            else
            {
                var newArticle = new Article
                {
                    Title = model.Title,
                    Text = model.Text,
                    CategoryId = model.CategoryId,
                    CreateByUserId = model.CreateByUserId,
                    CreatedDate = DateTime.Now,
                    Active = model.Active
                };
                _context.Article.Add(newArticle);
                _context.SaveChanges();

                var newId = newArticle.Id;
                model.Id = newId;
            }

            await _context.SaveChangesAsync();

            return model.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var article = await _context.Article.FirstOrDefaultAsync(x => x.Id == id);
            
            _context.Article.Remove(article);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

using News.DAL.Interfaces;
using News.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace News.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ArticleContext _context;

        public UnitOfWork(ArticleContext context)
        {
            _context = context;
        }

        public IArticleRepository ArticleRepository => new ArticleRepository(_context);
        public ICategoryRepository CategoryRepository => new CategoryRepository(_context);
        public IUserRepository UserRepository => new UserRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

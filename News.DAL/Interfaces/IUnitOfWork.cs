using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace News.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IArticleRepository ArticleRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IUserRepository UserRepository { get; }
    }
}

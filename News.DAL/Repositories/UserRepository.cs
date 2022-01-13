using Microsoft.EntityFrameworkCore;
using News.DAL.Interfaces;
using News.Models.Entities;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace News.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ArticleContext _context;

        public UserRepository(ArticleContext context)
        {
            _context = context;
        }

        public async Task<int> GetUserIdByUsername(string username)
        {
            var userId = 0;

            var user = await _context.User
                .FirstOrDefaultAsync(u => u.Username.Equals(username));

            if (user != null && user.Id > 0)
            {
                userId = user.Id;
            }

            return userId;
        }

        public async Task<int> RegisterUser(User user)
        {
            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Password = user.Password,
                ArticleCategoryId = user.ArticleCategoryId,
                Active = true
            };
            
            _context.User.Add(newUser);
            await _context.SaveChangesAsync();
            
            var registeredUserId = newUser.Id;
            return registeredUserId;
        }       

        public async Task<User> LoginUser(User login)
        {
            return await _context.User
                .Include(x => x.ArticleCategory)
                .Where(x => x.Username == login.Username && x.Password == login.Password)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == id);

            user.Active = false;
            
            await _context.SaveChangesAsync();

            return true;
        }

    }
}

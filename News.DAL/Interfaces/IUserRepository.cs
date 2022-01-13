using News.Models.Entities;
using System.Threading.Tasks;

namespace News.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<int> RegisterUser(User user);
        Task<int> GetUserIdByUsername(string username);
        Task<User> LoginUser(User login);
        Task<User> GetUserById(int id);
        Task<bool> Delete(int id);
    }
}

using News.BL.Models.Request;
using News.BL.Models.Response;
using News.Models.Entities;
using System.Threading.Tasks;

namespace News.BL.Interfaces
{
    public interface IUserService
    {
        Task<int> RegisterUser(RegisterModel user);
        Task<LoggedUserViewModel> LoginUser(LoginModel login);
        Task<bool> Delete(int id);
    }
}

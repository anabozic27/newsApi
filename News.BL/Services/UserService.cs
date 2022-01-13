using AutoMapper;
using FluentValidation;
using News.BL.Interfaces;
using News.BL.Models.Request;
using News.BL.Models.Response;
using News.DAL.Interfaces;
using News.Models.Entities;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace News.BL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LoggedUserViewModel> LoginUser(LoginModel login)
        {
            var passHash = GeneratePassHash(login.Password);
            login.Password = passHash;
            
            var user = await _unitOfWork.UserRepository.LoginUser(_mapper.Map<User>(login));


            if (user == null || user.Id <= 0)
                throw new ValidationException("Invalid username or password");

            if (!user.Active)
                throw new ValidationException("User not active");

            var loggedInUser = _mapper.Map<LoggedUserViewModel>(user); 
            
            return loggedInUser;
        }

        public async Task<int> RegisterUser(RegisterModel user)
        {
            var userExists = await _unitOfWork.UserRepository.GetUserIdByUsername(user.Username);
            if (userExists > 0)
                throw new ValidationException("Username already exists in application");

            var passwordHash = GeneratePassHash(user.Password);
            user.Password = passwordHash;

            var res = await _unitOfWork.UserRepository.RegisterUser(_mapper.Map<User>(user));
            
            return res;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _unitOfWork.UserRepository.GetUserById(id);
            if (user == null)
                throw new ValidationException("User not found");

            return await _unitOfWork.UserRepository.Delete(id);
        }


        #region methods
        private string GeneratePassHash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            byte[] hashBytes;
            using (var algorithm = new SHA512Managed())
            {
                hashBytes = algorithm.ComputeHash(bytes);
            }
            return Convert.ToBase64String(hashBytes);
        }
        #endregion
    }
}

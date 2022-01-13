using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using News.BL.Interfaces;
using News.BL.Models.Request;
using News.BL.Models.Response;
using News.Models.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace News.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Method for registering new user
        /// </summary>
        /// <remarks>
        ///     POST / RegisterModel
        ///     {
        ///       "id": 0,
        ///       "firstName": "Test",
        ///       "lastName": "Test",
        ///       "username": "testni",
        ///       "password": "Test123!",
        ///       "confirmPassword": "Test123!",
        ///       "articleCategoryId": 1
        ///     }
        /// </remarks>
        /// <param name="user">Object which contains all necessery information for registration</param>
        /// <returns>Object which contains info about registered user like id, username, categoryId...</returns>
        [HttpPost]
        [Route("register")]
        [SwaggerOperation(Tags = new[] { "User" })]
        public async Task<int> RegisterUser([FromBody]RegisterModel user)
        {
            var newUserId = await _userService.RegisterUser(user);

            return newUserId;
        }

        /// <summary>
        /// Method for user login
        /// </summary>
        /// <remarks>
        ///     POST /LoginModel
        ///     {
        ///         "username": "test",
        ///         "password": "Test123!"
        ///     }
        /// </remarks>
        /// <param name="user">Model which contains user credentials (username and password)</param>
        /// <returns>Object with information about logged in user like id, name, token...</returns>
        [HttpPost]
        [Route("login")]
        [SwaggerOperation(Tags = new[] { "User" })]
        public async Task<LoggedUserViewModel> LoginUser([FromBody]LoginModel user)
        {
            var loggedUser = await _userService.LoginUser(user);

            if(loggedUser.Id > 0)
            {
                loggedUser.Token = CreateToken(loggedUser.Id, loggedUser.Username, loggedUser.FirstName, loggedUser.LastName, loggedUser.ArticleCategoryId);
            }

            return loggedUser;
        }

        /// <summary>
        /// Method for deleting user (setting user not active)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True or false</returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        [SwaggerOperation(Tags = new[] { "User" })]
        public async Task<bool> DeleteUser(int id)
        {
            return await _userService.Delete(id);
        }

    }
}
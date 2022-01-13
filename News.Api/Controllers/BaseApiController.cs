using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace News.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Method for creating token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="username"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        protected string CreateToken(int userId, string username, string firstName, string lastName, int? categoryId)
        {
            string retValue = string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("articleAplication_2022@_secret");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString()),
                    new Claim("username", username),
                    new Claim("firstName", firstName),
                    new Claim("lastName", lastName),
                    new Claim("categoryId", categoryId.HasValue ? categoryId.ToString() : "")
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            retValue = tokenHandler.WriteToken(token);

            return retValue;
        }

        protected int GetUserIdFromToken(string authValue)
        {
            int userId = 0;

            try
            {
                userId = Convert.ToInt16(User.Identity.Name);
            }
            catch
            {
                userId = 0;
            }
            return userId;
        }
    }
}
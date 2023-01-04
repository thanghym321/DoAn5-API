using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using DoAn5_API.Entities;
using DoAn5_API.Helpers;
using DoAn5_API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using DoAn5.DataContext.EF;
using DoAn5.Application.BLL.Interfaces;

namespace WebApi.Services
{
    public class ManageUser : IManageUser
    {
        private readonly DoAn5DbContext _context;
        private readonly AppSettings _appSettings;

        public ManageUser(IOptions<AppSettings> appSettings, DoAn5DbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public UserViewModel Authenticate(string username, string password)
        {
            var result = from a in _context.Accounts
                         join u in _context.Users on a.User_Id equals u.Id
                         select new UserViewModel 
                         { 
                             Role = a.Permissions, 
                             User_Id = a.User_Id, 
                             UserName = a.UserName, 
                             Name = u.Name, 
                             PassWord = a.Password,
                             Address = u.Address,
                             Phone = u.Phone, 
                             Email = u.Email 
                         };

            var user = result.SingleOrDefault(x => x.UserName == username && x.PassWord == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Name.ToString()),
                    new Claim(ClaimTypes.MobilePhone, user.Phone.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

    }
}
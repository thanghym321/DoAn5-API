﻿using System;
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
using System.Threading.Tasks;
using DoAn5.DataContext.Entities;
using DoAn5.Application.Common;
using Microsoft.Data.SqlClient.Server;

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
                         join b in _context.Users on a.User_Id equals b.Id
                         select new UserViewModel 
                         {
                             User_Id = a.User_Id,
                             UserName = a.UserName,
                             PassWord = a.Password,
                             StartDate = a.StartDate,
                             EndDate = a.EndDate,
                             Status_Account = a.Status,
                             Role = a.Role,
                             Name = b.Name,
                             DateOfBirth = b.DateOfBirth,
                             GioiTinh = b.GioiTinh,
                             Image = b.Image,
                             Address = b.Address,
                             Phone = b.Phone,
                             Email = b.Email,
                             Status_User = b.Status,
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

        public async Task<List<UserViewModel>> Get()
        {
            var query = from a in _context.Accounts
                        join b in _context.Users on a.User_Id equals b.Id
                        select new
                        { a , b };
            return await query.Select(x => new UserViewModel()
            {
                User_Id = x.a.User_Id,
                UserName = x.a.UserName,
                PassWord = x.a.Password,
                StartDate = x.a.StartDate,
                EndDate = x.a.EndDate,
                Status_Account = x.a.Status,
                Role = x.a.Role,
                Name = x.b.Name,
                DateOfBirth = x.b.DateOfBirth,
                GioiTinh = x.b.GioiTinh,
                Image = x.b.Image,
                Address = x.b.Address,
                Phone = x.b.Phone,
                Email = x.b.Email,
                Status_User = x.b.Status,

            }).ToListAsync();
        }


        public async Task<PagedResult<UserViewModel>> GetAllPaging(int pageindex, int pagesize, string UserName, string Name, string Role)
        {
            var query = from a in _context.Accounts
                        join b in _context.Users on a.User_Id equals b.Id
                        select new { a , b };

            if (!string.IsNullOrEmpty(UserName))
            {
                query = query.Where(x => x.a.UserName.ToLower().Contains(UserName.ToLower()));
            }
            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(x => x.b.Name.ToLower().Contains(Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(Role))
            {
                query = query.Where(x => x.a.Role.ToLower().Contains(Role.ToLower()));
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageindex - 1) * pagesize).Take(pagesize)
            .Select(x => new UserViewModel()
            {
                User_Id = x.a.User_Id,
                UserName = x.a.UserName,
                PassWord = x.a.Password,
                StartDate = x.a.StartDate,
                EndDate = x.a.EndDate,
                Status_Account = x.a.Status,
                Role = x.a.Role,
                Name = x.b.Name,
                DateOfBirth = x.b.DateOfBirth,
                GioiTinh = x.b.GioiTinh,
                Image = x.b.Image,
                Address = x.b.Address,
                Phone = x.b.Phone,
                Email = x.b.Email,
                Status_User = x.b.Status,

            }).ToListAsync();

            var pageResult = new PagedResult<UserViewModel>()
            {
                TotalItem = totalRow,
                Items = data,
            };

            return pageResult;
        }

        public async Task<UserViewModel> GetById(int Id)
        {
            var query = from a in _context.Accounts
                        join b in _context.Users on a.User_Id equals b.Id
                        select new UserViewModel
                        {
                            User_Id = a.User_Id,
                            UserName = a.UserName,
                            PassWord = a.Password,
                            StartDate = a.StartDate,
                            EndDate = a.EndDate,
                            Status_Account = a.Status,
                            Role = a.Role,
                            Name = b.Name,
                            DateOfBirth = b.DateOfBirth,
                            GioiTinh = b.GioiTinh,
                            Image = b.Image,
                            Address = b.Address,
                            Phone = b.Phone,
                            Email = b.Email,
                            Status_User = b.Status,
                        };
          
            var user = await query.SingleOrDefaultAsync(x => x.User_Id == Id);

            return user;
        }

        public async Task<int> Create(UserModel request)
        {
            _context.Users.Add(request.user);
            await _context.SaveChangesAsync();

            int User_Id = request.user.Id;
            request.account.User_Id = User_Id;
            _context.Accounts.Add(request.account);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<int> Update(UserModel request)
        {
            var user = await _context.Users.FindAsync(request.user.Id);

            user.Name = request.user.Name;
            user.DateOfBirth = request.user.DateOfBirth;
            user.GioiTinh = request.user.GioiTinh;
            user.Image = request.user.Image;
            user.Address = request.user.Address;
            user.Phone = request.user.Phone;
            user.Email = request.user.Email;
            user.Status = request.user.Status;

            await _context.SaveChangesAsync();

            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.User_Id == request.account.User_Id);

            account.UserName = request.account.UserName;
            account.Password = request.account.Password;
            account.StartDate = request.account.StartDate;
            account.EndDate = request.account.EndDate;
            account.Status = request.account.Status;
            account.Role = request.account.Role;

            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<int> Delete(int Id)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(x => x.User_Id == Id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id==Id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return 1;
        }
    }
}
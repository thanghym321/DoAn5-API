using DoAn5_API.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.Application.BLL.Interfaces
{
    public interface IManageUser
    {
        UserViewModel Authenticate(string username, string password);
    }
}

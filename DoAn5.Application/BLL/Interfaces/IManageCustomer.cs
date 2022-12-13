using DoAn5.Application.Common;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoAn5.Application.BLL.Interfaces
{
    public interface IManageCustomer
    {
        Task<List<Customer>> Get();
        Task<PagedResult<Customer>> GetAllPaging(int pageindex, int pagesize, string keyword);
        Task<Customer> GetById(int Id);
        Task<int> Create(Customer request);
        Task<int> Update(Customer request);
        Task<int> Delete(int Id);
    }
}

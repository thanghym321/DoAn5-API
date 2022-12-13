using DoAn5.Application.Common;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoAn5.Application.BLL.Interfaces
{
    public interface IManageProducer
    {
        Task<List<Producer>> Get();
        Task<PagedResult<Producer>> GetAllPaging(int pageindex, int pagesize, string keyword);
        Task<Producer> GetById(int Id);
        Task<int> Create(Producer request);
        Task<int> Update(Producer request);
        Task<int> Delete(int Id);
    }
}

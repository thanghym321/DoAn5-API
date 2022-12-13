using DoAn5.Application.Common;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoAn5.Application.BLL.Interfaces
{
    public interface IManageSlide
    {
        Task<List<Slide>> Get();
        Task<PagedResult<Slide>> GetAllPaging(int pageindex, int pagesize);
        Task<Slide> GetById(int Id);
        Task<int> Create(Slide request);
        Task<int> Update(Slide request);
        Task<int> Delete(int Id);
    }
}

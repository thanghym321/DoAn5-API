using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.Application.Common
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalItem { get; set; }
    }
}

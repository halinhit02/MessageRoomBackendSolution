using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Common
{
    public class PagedResult<T>
    {
        public List<T> Items { set; get; }
        public int TotalRecord { set; get; }
    }
}
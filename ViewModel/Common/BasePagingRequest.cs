using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Common
{
    public class BasePagingRequest
    {
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
    }
}
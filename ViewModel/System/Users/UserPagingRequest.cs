using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.Common;

namespace ViewModel.System.Users
{
    public class UserPagingRequest : BasePagingRequest
    {
        public string Keyword { get; set; }
    }
}
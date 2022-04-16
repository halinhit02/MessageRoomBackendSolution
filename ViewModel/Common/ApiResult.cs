using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Common
{
    public class ApiResult<T>
    {
        public bool IsSucceeded { get; set; }

        public string Message { get; set; }

        public T Result { get; set; }

        public static ApiResult<bool> From(bool result)
        {
            if (result)
            {
                return new ApiSuccessResult<bool>(true);
            }
            else
            {
                return new ApiErrorResult<bool>("Đã xảy ra lỗi.");
            }
        }
    }
}
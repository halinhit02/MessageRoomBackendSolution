using System;
using System.Collections.Generic;
using System.Text;
using Utilities;

namespace ViewModel.Common
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public ApiErrorResult()
        {
            IsSucceeded = false;
            Message = ResultConstants.CommonError;
        }

        public ApiErrorResult(string message)
        {
            IsSucceeded = false;
            Message = message;
        }

        public ApiErrorResult(T resultObj)
        {
            IsSucceeded = false;
            Result = resultObj;
        }
    }
}
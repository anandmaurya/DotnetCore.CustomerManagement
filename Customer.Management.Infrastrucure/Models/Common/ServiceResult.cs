using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Infrastrucure.Models.Common
{
    public class ServiceResult<T>
    {
        public bool Success { get; }
        public string Message { get; }
        public T Data { get; }

        public ServiceResult(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static ServiceResult<T> CreateSuccess(T data, string message)
        {
            return new ServiceResult<T>(true, message, data);
        }

        public static ServiceResult<T> CreateFail(string message)
        {
            return new ServiceResult<T>(false, message, default(T));
        }
    }
}

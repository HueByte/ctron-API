using Ctron.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ctron.API.Services
{
    public static class Response<T>
    {
        public static ApiResponse<T> Create(T obj, bool isSuccess, string message) => new ApiResponse<T>() { Data = obj, isSuccess = isSuccess, Message = message };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctron.Infrastructure.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool isSuccess { get; set; }
        public string Message { get; set; }
    }
}

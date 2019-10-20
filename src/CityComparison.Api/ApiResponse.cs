using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityComparison.Api
{
    public class ApiResponse<T>
    {     
        public ApiResponse(T data)
        {
            Data = data;
        }
        /// <summary>
        /// The data response data
        /// </summary>
        public T Data { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.DTOs
{
    public record Response<T>
    {
        public Response()
        {
        }

        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }

        public Response(T data, string message)
        {
            Succeeded = true;
            Message = message;
            Errors = null;
            Data = data;
        }

        public Response(string message)
        {
            Succeeded = true;
            Message = message;
            Errors = null;
            Data = default;
        }

        public Response(string[] errors)
        {
            Succeeded = false;
            Message = string.Empty;
            Errors = errors;
            Data = default;
        }

        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}

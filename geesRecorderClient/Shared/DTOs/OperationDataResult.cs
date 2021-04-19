using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.DTOs
{
    /// <summary>
    /// Operation result model class for successful operations returning data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record OperationDataResult<T> : OperationResult
    {
        public OperationDataResult(T data)
        {
            Succeeded = true;
            Data = data;
        }

        public OperationDataResult(string error)
        {
            Error = error;
            Succeeded = false;
        }

        public T Data { get; set; }
    }
}

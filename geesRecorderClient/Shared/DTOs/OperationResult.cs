using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.DTOs
{
    /// <summary>
    /// Operation result model class for operations
    /// </summary>
    public record OperationResult
    {
        public OperationResult()
        {

        }

        public OperationResult(bool succeeded)
        {
            Succeeded = succeeded;
        }

        /// <summary>
        /// When errors are present, succeded is false
        /// </summary>
        /// <param name="errors"></param>
        public OperationResult(string error)
        {
            Succeeded = false;
            Error = error;
        }

        public bool Succeeded { get; set; }
        public string Error { get; set; }
    }
}

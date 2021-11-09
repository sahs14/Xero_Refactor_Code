using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public enum ErrorType
    {
        Ok,
        BadRequest,
        NotFound

    }
    public class ManageResult
    {
        public ManageResult(bool isSuccess, ErrorType error, List<string> returnMessage)
        {
            IsSuccess = isSuccess;
            ReturnMessage = returnMessage;
            errorType = error;

        }

        public ManageResult(bool isSuccess, ErrorType error, string returnMessage)
        {
            IsSuccess = isSuccess;
            ReturnMessage = new List<string> {returnMessage};
            errorType = error;
        }

        public bool IsSuccess { get; set; }
        public ErrorType errorType { get; set; }
        public List<string> ReturnMessage { get; set;}
    }
}
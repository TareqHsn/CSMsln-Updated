using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.Core.Common
{
    public class ResponseResult
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccessStatus { get; set; }
    }
}

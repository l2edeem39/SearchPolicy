using System;
using System.Collections.Generic;
using System.Text;

namespace SearchPolicy.Model.PreValidate
{
    public class ResponsePreValidate
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public string Token { get; set; }
    }
}

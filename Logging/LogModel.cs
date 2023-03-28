using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchPolicy.Api.Logging
{
    public class LogModel<T1, T2, T3> where T1 : class where T2 : class where T3 : class
    {
        public T1 Request { get; set; }
        public T2 Header { get; set; }
        public T3 Response { get; set; }
        public string Message { get; set; }
    }
}

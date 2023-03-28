using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchPolicy.Api.Logging
{
    public class Logging
    {
        protected MSSqlServerSinkOptions sinks;
        protected Logger Log;
    }
}

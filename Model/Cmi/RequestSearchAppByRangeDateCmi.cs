using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchPolicy.Api.Model.Cmi
{
    public class RequestSearchAppByRangeDateCmi
    {
        public string field { get; set; }
        public string keyword { get; set; }
        public string startYear { get; set; }
        public string endYear { get; set; }
    }
}

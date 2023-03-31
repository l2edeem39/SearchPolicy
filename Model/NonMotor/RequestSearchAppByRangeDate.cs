using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchPolicy.Api.Model.NonMotor
{
    public class RequestSearchAppByRangeDate
    {
        public string fieldType { get; set; }
        public string keyword { get; set; }
        public string keyname { get; set; }
        public string startYear { get; set; }
        public string endYear { get; set; }
    }


}

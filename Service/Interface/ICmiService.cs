using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchPolicy.Api.Model.Cmi;

namespace SearchPolicy.Api.Service.Interface
{
    public interface ICmiService
    {
        public List<ResponseSearchByRangeDateModel> GenarateQuerySearchPolByRangeDate(string polnumber, string maxCmiReturned);
        public List<ResponseSearchByRangeDateModel> GenarateQueryLicenseByRangeDate(string chassisnumber, string startYear, string endYear, string maxCmiReturned, string conn);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchPolicy.Api.Model.Cmi;

namespace SearchPolicy.Api.Service.Interface
{
    public interface ICmiService
    {
        public string SearchCmi();
        public string GenarateQuerySearchPolByRangeDate(string polnumber, int maxCmiReturned);
        public List<ResponseSearchByRangeDateModel> GenarateQueryLicenseByRangeDate(string chassisnumber, string startYear, string endYear, string maxCmiReturned, string conn);
    }
}

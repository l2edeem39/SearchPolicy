using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchPolicy.Api.Model.Cmi;

namespace SearchPolicy.Api.Service.Interface
{
    public interface ICmiService
    {
        public List<ResponseSearchByRangeDateModel> GenarateQuerySearchPolByRangeDate(string polNumber, string maxCmiReturned, string conn);
        public List<ResponseSearchByRangeDateModel> GenarateQueryLicenseByRangeDate(string licenseNumber, string startYear, string endYear, string maxCmiReturned, string conn);
        public List<ResponseSearchByRangeDateModel> GenarateQueryChassisByRangeDate(string chassisNumber, string startYear, string endYear, string maxCmiReturned, string conn);
        public List<ResponseSearchByRangeDateModel> GenarateQueryNameByRangeDate(string name, string startYear, string endYear, string maxCmiReturned, string conn);
        public List<ResponseSearchByRangeDateModel> GenarateQuerySerialnumberByRangeDate(string serialNumber, string startYear, string endYear, string maxCmiReturned, string conn);
        public List<ResponseSearchByRangeDateModel> GenarateQueryIdNoByRangeDate(string idNo, string startYear, string endYear, string maxCmiReturned, string conn);
    }
}

using SearchPolicy.Api.Model.Vmi;
using System.Collections.Generic;

namespace SearchPolicy.Api.Service.Interface
{
    public interface IVmiService
    {
        public List<ResponseSearchByRangeDateModel> GenarateQuerySearchAppByRangeDate(string polNumber, string maxCmiReturned, string conn);
        public List<ResponseSearchByRangeDateModel> GenarateQuerySearchPolByRangeDate(string polNumber, string maxCmiReturned, string conn);
        public List<ResponseSearchByRangeDateModel> GenarateQuerySearchLicense(string licenseNumber, string startYear, string endYear, string maxCmiReturned, string conn);
        public List<ResponseSearchByRangeDateModel> GenarateQuerySearchChassisByRangeDate(string chassisNumber, string startYear, string endYear, string maxCmiReturned, string conn);
        public List<ResponseSearchByRangeDateModel> GenerateQuerySearchNameByRangeDate(string name, string startYear, string endYear, string maxCmiReturned, string conn);
        public List<ResponseSearchByRangeDateModel> GenarateQuerySearchIdNoByRangeDate(string idNo, string startYear, string endYear, string maxCmiReturned, string conn);
    }
}

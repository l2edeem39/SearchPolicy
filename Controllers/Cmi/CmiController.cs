using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SearchPolicy.Api.Service;
using SearchPolicy.Api.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchPolicy.Api.Controllers.Cmi
{
    [Route("CmiSearchPolicy")]
    [ApiController]
    public class CmiController : Controller
    {
        private readonly ICmiService _servicecmi;
        private readonly IConfiguration _config;
        public CmiController(ICmiService servicecmi, IConfiguration config)
        {
            _servicecmi = servicecmi;
            _config = config;
        }
        [Route("api/1.0/SearchPolicyByRangeDate")]
        [HttpPost]
        public IActionResult SearchPolicyByRangeDate(string field, string keyword, string startYear, string endYear)
        {
            startYear = !string.IsNullOrEmpty(startYear) ? startYear.Substring(2, 2) : string.Empty;
            endYear = !string.IsNullOrEmpty(endYear) ? endYear.Substring(2, 2) : string.Empty;
            var MaxPolicyReturned = _config.GetSection("DataDefault")["MaxPolicyReturned"].ToString();
            
            switch (field)
            {
                case "pol":
                    //cmd.CommandText = GenarateQuerySearchPolByRangeDate(keyword, MaxCmiReturned);
                    break;
                case "license":
                    var result = _servicecmi.GenarateQueryLicenseByRangeDate(keyword, startYear, endYear, MaxPolicyReturned, GetConnection());
                    return Ok(result);
                case "chassis":
                    //cmd.CommandText = GenarateQueryChassisByRangeDate(keyword, startYear, endYear, MaxCmiReturned);
                    break;
                case "name":
                    //cmd.CommandText = GenarateQueryNameByRangeDate(keyword, startYear, endYear, MaxCmiReturned);
                    break;
                case "serial":
                    //cmd.CommandText = GenarateQuerySerialnumberByRangeDate(keyword, startYear, endYear, MaxCmiReturned);
                    break;
                case "idno":
                    //cmd.CommandText = GenarateQueryIdNoByRangeDate(keyword, startYear, endYear, MaxCmiReturned);
                    break;
            }

            return Ok();
        }
        private string GetConnection()
        {
            var conn = string.Empty;
            try
            {
                if (_config.GetSection("DataDefault")["DBSWHour"].IndexOf("," + DateTime.Now.Hour.ToString() + ",") > 0)
                {
                    conn = _config.GetConnectionString("CmiDB2");
                    // เวลาที่ switch db (intranetdb)
                }
                else
                {
                    conn = _config.GetConnectionString("CmiDB1");
                    // เวลาหลัก
                }
            }
            catch (Exception ex)
            {
                try
                {
                    conn = conn = _config.GetConnectionString("CmiDB1");
                }
                catch (Exception ex2)
                {
                    throw (new Exception(ex2.Message));
                }
            }
            return conn;
        }
    }
}

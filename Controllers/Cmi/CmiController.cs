using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SearchPolicy.Api.Logging;
using SearchPolicy.Api.Model;
using SearchPolicy.Api.Model.Cmi;
using SearchPolicy.Api.Service.Interface;
using SearchPolicy.Share.EnvironmentShared;
using SearchPolicy.Share.Utility;
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
        ResponseSearchPolicy response = new ResponseSearchPolicy();
        int statusCode = 200;
        DateTime requestDate;

        private readonly ICmiService _servicecmi;
        private readonly IConfiguration _config;
        public CmiController(ICmiService servicecmi, IConfiguration config)
        {
            _servicecmi = servicecmi;
            _config = config;
        }
        [Route("api/1.0/SearchPolicyByRangeDate")]
        [HttpPost]
        public IActionResult SearchPolicyByRangeDate([FromBody] RequestSearchAppByRangeDateCmi requestSearch)
        {
            RequestHeader requestHeader = new RequestHeader
            {
                sourceTransID = Request.Headers["sourceTransID"].ToString(),
                requestTime = Request.Headers["requestTime"].ToString()
            };

            requestDate = DateTime.Now;

            try
            {
                var result = new List<ResponseSearchByRangeDateModel>();
                requestSearch.startYear = !string.IsNullOrEmpty(requestSearch.startYear) ? requestSearch.startYear.Substring(2, 2) : string.Empty;
                requestSearch.endYear = !string.IsNullOrEmpty(requestSearch.endYear) ? requestSearch.endYear.Substring(2, 2) : string.Empty;
                var MaxPolicyReturned = _config.GetSection("DataDefault")["MaxPolicyReturned"].ToString();

                switch (requestSearch.field)
                {
                    case "pol":
                        result = _servicecmi.GenarateQuerySearchPolByRangeDate(requestSearch.keyword, MaxPolicyReturned, GetConnection());
                        break;
                    case "license":
                        result = _servicecmi.GenarateQueryLicenseByRangeDate(requestSearch.keyword, requestSearch.startYear, requestSearch.endYear, MaxPolicyReturned, GetConnection());
                        break;
                    case "chassis":
                        result = _servicecmi.GenarateQueryChassisByRangeDate(requestSearch.keyword, requestSearch.startYear, requestSearch.endYear, MaxPolicyReturned, GetConnection());
                        break;
                    case "name":
                        result = _servicecmi.GenarateQueryNameByRangeDate(requestSearch.keyword, requestSearch.startYear, requestSearch.endYear, MaxPolicyReturned, GetConnection());
                        break;
                    case "serial":
                        result = _servicecmi.GenarateQuerySerialnumberByRangeDate(requestSearch.keyword, requestSearch.startYear, requestSearch.endYear, MaxPolicyReturned, GetConnection());
                        break;
                    case "idno":
                        result = _servicecmi.GenarateQueryIdNoByRangeDate(requestSearch.keyword, requestSearch.startYear, requestSearch.endYear, MaxPolicyReturned, GetConnection());
                        break;
                }

                if (result == null)
                {
                    statusCode = 400;
                    response.ErrorCode = ErrorCode.Code400;
                    response.ErrorMessage = "Field Not Found";
                    response.Status = "1";
                    WriteLog(LogEnum.Level.Information, requestSearch, requestHeader, response, statusCode, GetConnection());
                    return StatusCode(statusCode, new { response, data = new List<ResponseSearchByRangeDateModel>() });
                }
                else if (result.Count == 0)
                {
                    statusCode = 400;
                    response.ErrorCode = ErrorCode.Code400;
                    response.ErrorMessage = "Data Not Found";
                    response.Status = "1";
                    WriteLog(LogEnum.Level.Information, requestSearch, requestHeader, response, statusCode, GetConnection());
                    return StatusCode(statusCode, new { response, data = new List<ResponseSearchByRangeDateModel>() });
                }
                statusCode = 200;
                response.ErrorCode = "";
                response.ErrorMessage = "";
                response.Status = "0";
                WriteLog(LogEnum.Level.Information, requestSearch, requestHeader, response, statusCode, GetConnection());
                return StatusCode(statusCode, new { response, data = result });
            }
            catch (Exception ex)
            {
                statusCode = 500;
                response.ErrorCode = SystemStatusCode.SystemError;
                response.ErrorMessage = ex.Message;
                response.Status = "1";
                WriteLog(requestSearch, requestHeader, response, ex, statusCode, GetConnection());
                return StatusCode(statusCode, new { response, data = new List<ResponseSearchByRangeDateModel>() });
            }
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
                throw (new Exception(ex.Message));
            }
            return conn;
        }

        private async void WriteLog(LogEnum.Level level, RequestSearchAppByRangeDateCmi requestData, RequestHeader requestHeader, ResponseSearchPolicy response, int status_code, string connectionString)
        {
            var log = new LogModel
            {
                Application = EnvironmentShared.GetProjectName(),
                TimeStamp = requestDate,
                Body = JsonConvert.SerializeObject(requestData),
                Header = JsonConvert.SerializeObject(requestHeader),
                Response = JsonConvert.SerializeObject(response),
                HttpStatus = status_code.ToString(),
                Message = response.ErrorMessage,
                Description1 = this.ControllerContext.RouteData.Values["controller"].ToString(),
                Description2 = this.ControllerContext.RouteData.Values["action"].ToString()
            };
            if (level.Equals(LogEnum.Level.Information))
                await Logging.Logging.LogInformation(log, connectionString);
            else if (level.Equals(LogEnum.Level.Success))
                await Logging.Logging.LogSuccess(log, connectionString);
        }

        private async void WriteLog(RequestSearchAppByRangeDateCmi requestData, RequestHeader requestHeader, ResponseSearchPolicy response, Exception ex, int status_code, string connectionString)
        {
            var log = new LogModel
            {
                Application = EnvironmentShared.GetProjectName(),
                TimeStamp = requestDate,
                Body = JsonConvert.SerializeObject(requestData),
                Header = JsonConvert.SerializeObject(requestHeader),
                Response = JsonConvert.SerializeObject(response),
                Exception = ex,
                HttpStatus = status_code.ToString(),
                Message = response.ErrorMessage,
                Description1 = this.ControllerContext.RouteData.Values["controller"].ToString(),
                Description2 = this.ControllerContext.RouteData.Values["action"].ToString()
            };
            await Logging.Logging.LogError(log, connectionString);
        }
    }
}

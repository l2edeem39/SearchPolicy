using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SearchPolicy.Api.Logging;
using SearchPolicy.Api.Model;
using SearchPolicy.Api.Model.Vmi;
using SearchPolicy.Api.Service.Interface;
using SearchPolicy.Share.EnvironmentShared;
using SearchPolicy.Share.Utility;
using System;
using System.Collections.Generic;

namespace SearchPolicy.Api.Controllers.Vmi
{
    [Route("VmiSearchPolicy")]
    [ApiController]
    public class VmiController : Controller
    {
        ResponseSearchPolicy response = new ResponseSearchPolicy();
        int statusCode = 200;
        DateTime requestDate;

        private readonly IVmiService _servicevmi;
        private readonly IConfiguration _config;
        public VmiController(IVmiService servicecmi, IConfiguration config)
        {
            _servicevmi = servicecmi;
            _config = config;
        }
        [Route("api/1.0/SearchPolicyByRangeDate")]
        [HttpPost]
        public IActionResult SearchPolicyByRangeDate(string field, string keyword, string startYear, string endYear)
        {
            RequestHeader requestHeader = new RequestHeader
            {
                sourceTransID = Request.Headers["sourceTransID"].ToString(),
                requestTime = Request.Headers["requestTime"].ToString()
            };

            RequestSearchAppByRangeDate requestSearch = new RequestSearchAppByRangeDate
            {
                field = field,
                keyword = keyword,
                startYear = startYear,
                endYear = endYear
            };

            requestDate = DateTime.Now;

            try
            {
                var result = new List<ResponseSearchByRangeDateModel>();
                startYear = !string.IsNullOrEmpty(startYear) ? startYear.Substring(2, 2) : string.Empty;
                endYear = !string.IsNullOrEmpty(endYear) ? endYear.Substring(2, 2) : string.Empty;
                var MaxPolicyReturned = _config.GetSection("DataDefault")["MaxPolicyReturned"].ToString();

                switch (field)
                {
                    case "app":
                        result = _servicevmi.GenarateQuerySearchAppByRangeDate(keyword, MaxPolicyReturned, GetConnection());
                        break;
                    case "pol":
                        result = _servicevmi.GenarateQuerySearchPolByRangeDate(keyword, MaxPolicyReturned, GetConnection());
                        break;
                    case "license":
                        result = _servicevmi.GenarateQuerySearchLicense(keyword, startYear, endYear, MaxPolicyReturned, GetConnection());
                        break;
                    case "chassis":
                        result = _servicevmi.GenarateQuerySearchChassisByRangeDate(keyword, startYear, endYear, MaxPolicyReturned, GetConnection());
                        break;
                    case "name":
                        result = _servicevmi.GenerateQuerySearchNameByRangeDate(keyword, startYear, endYear, MaxPolicyReturned, GetConnection());
                        break;
                    case "idno":
                        result = _servicevmi.GenarateQuerySearchIdNoByRangeDate(keyword, startYear, endYear, MaxPolicyReturned, GetConnection());
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
                    conn = _config.GetConnectionString("VmiDB2");
                    // เวลาที่ switch db (intranetdb)
                }
                else
                {
                    conn = _config.GetConnectionString("VmiDB1");
                    // เวลาหลัก
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            return conn;
        }

        private async void WriteLog(LogEnum.Level level, RequestSearchAppByRangeDate requestData, RequestHeader requestHeader, ResponseSearchPolicy response, int status_code, string connectionString)
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

        private async void WriteLog(RequestSearchAppByRangeDate requestData, RequestHeader requestHeader, ResponseSearchPolicy response, Exception ex, int status_code, string connectionString)
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

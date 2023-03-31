using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SearchPolicy.Api.Logging;
using SearchPolicy.Share.EnvironmentShared;
using SearchPolicy.Share.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SearchPolicy.Api.Service;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Data;
using SearchPolicy.Api.Model.NonMotor;
using SearchPolicy.Api.Model;
using Newtonsoft.Json;

namespace SearchPolicy.Api.Controllers.NonMotor
{
    [Route("NonMotorSearchPolicy")]
    [ApiController]
    public class NonMotorController : Controller
    {
        private readonly IConfiguration _config;
        ResponseSearchPolicy response = new ResponseSearchPolicy();
        DateTime requestDate;
        string Condb = "";
        public NonMotorController(IConfiguration config)
        {
            _config = config;
        }

        [Route("api/1.0/SearchPolicyByRangeDate")]
        [HttpPost]
        public IActionResult SearchPolicyByRangeDate(string field, string keyword, string keyname, string startYear, string endYear)
        {
            RequestHeader requestHeader = null;
            requestHeader = new RequestHeader
            {
                sourceTransID = Request.Headers["sourceTransID"].ToString(),
                requestTime = Request.Headers["requestTime"].ToString()
            };

            RequestSearchAppByRangeDate requestSearch = null;
            requestSearch = new RequestSearchAppByRangeDate
            {
                fieldType = field,
                keyword = keyword,
                keyname = keyname,
                startYear = startYear,
                endYear = endYear
            };
            int statusCode = HttpStatusHelper.OK;
            requestDate = DateTime.Now;
            try
            {
                List<SearchAppByRangeDate> SearchPolicys = null;
                if (_config.GetValue<string>("DataDefault:DBSWHour").IndexOf("," + DateTime.Now.Hour.ToString() + ",") > 0)
                {
                    Condb = "NonDB2";
                }
                else
                {
                    Condb = "NonDB1";
                }
                
                var MaxPolicyReturned = _config.GetValue<int>("DataDefault:MaxPolicyReturned");
                var SubclassCar = _config.GetValue<string>("DataDefault:SubclassByType:Car");
                startYear = !string.IsNullOrEmpty(startYear) ? DateTime.Parse("01/01/" + startYear).ToString("yyyy", new CultureInfo("en-EN")).Substring(2, 2) : string.Empty;
                endYear = !string.IsNullOrEmpty(endYear) ? DateTime.Parse("01/01/" + endYear).ToString("yyyy", new CultureInfo("en-EN")).Substring(2, 2) : string.Empty;
                var nonMotor = new NonMotorService();

                switch (field)
                {
                    case "app":
                        SearchPolicys = nonMotor.GenarateQuerySearchAppByRangeDate(keyword, keyname, MaxPolicyReturned, _config.GetConnectionString(Condb));
                        break;
                    case "pol":
                        SearchPolicys = nonMotor.GenarateQuerySearchPolByRangeDate(keyword, keyname, MaxPolicyReturned, _config.GetConnectionString(Condb));
                        break;
                    case "idno":
                        SearchPolicys = nonMotor.GenarateQuerySearchIdNoByRangeDate(keyword, keyname, startYear, endYear, MaxPolicyReturned, _config.GetConnectionString(Condb));
                        break;
                    case "license":
                        SearchPolicys = nonMotor.GenarateQuerySearchbyLicenseByRangeDate(keyword, keyname, startYear, endYear, MaxPolicyReturned, SubclassCar, _config.GetConnectionString(Condb));
                        break;
                    case "chassis":
                        SearchPolicys = nonMotor.GenarateQuerySearchChassisByRangeDate(keyword, keyname, startYear, endYear, MaxPolicyReturned, SubclassCar, _config.GetConnectionString(Condb));
                        break;
                    case "engine":
                        SearchPolicys = nonMotor.GenarateQuerySearchEngineNoRangeDate(keyword, keyname, startYear, endYear, MaxPolicyReturned, SubclassCar, _config.GetConnectionString(Condb));
                        break;
                    case "asname":
                        SearchPolicys = nonMotor.GenarateQuerySearchAsnameByRangeDate(keyname, startYear, endYear, MaxPolicyReturned, _config.GetConnectionString(Condb));
                        break;
                    case "name":
                        SearchPolicys = nonMotor.GenarateQuerySearchNameByRangeDate(keyname, startYear, endYear, MaxPolicyReturned, _config.GetConnectionString(Condb));
                        break;
                    case "hldname":
                        SearchPolicys = nonMotor.GenarateQuerySearchHldnameRangeDate(keyname, startYear, endYear, MaxPolicyReturned, _config.GetConnectionString(Condb));
                        break;
                }
                if (SearchPolicys == null)
                {
                    statusCode = HttpStatusHelper.BadRequest;
                    response.ErrorCode = ErrorCode.Code400;
                    response.ErrorMessage = "Field Not Found";
                    response.Status = "1";
                    WriteLog(LogEnum.Level.Information, requestSearch, requestHeader, response, statusCode, _config.GetConnectionString(Condb));
                    return StatusCode(statusCode, new { response, data = new List<SearchAppByRangeDate>() });
                }
                else if (SearchPolicys.Count == 0)
                {
                    statusCode = HttpStatusHelper.BadRequest;
                    response.ErrorCode = ErrorCode.Code400;
                    response.ErrorMessage = "Data Not Found";
                    response.Status = "1";
                    WriteLog(LogEnum.Level.Information, requestSearch, requestHeader, response, statusCode, _config.GetConnectionString(Condb));
                    return StatusCode(statusCode, new { response, data = new List<SearchAppByRangeDate>() });
                }
                //return Ok(Json(SearchPolicys));
                statusCode = HttpStatusHelper.OK;
                response.ErrorCode = "";
                response.ErrorMessage = "";
                response.Status = "0";
                WriteLog(LogEnum.Level.Success, requestSearch, requestHeader, response, statusCode, _config.GetConnectionString(Condb));
                return StatusCode(statusCode, new { response, data = SearchPolicys });
            }
            catch (Exception ex)
            {
                statusCode = HttpStatusHelper.InternalServerError;
                response.ErrorCode = SystemStatusCode.SystemError;
                response.ErrorMessage = ex.Message;
                response.Status = "1";
                WriteLog(requestSearch, requestHeader, response, ex, statusCode, _config.GetConnectionString(Condb));
                return StatusCode(statusCode, new { response, data = new List<SearchAppByRangeDate>() });
            }
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
                Message = response.ErrorMessage

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
                Message = response.ErrorMessage
            };
            await Logging.Logging.LogError(log, connectionString);
        }
    }
}

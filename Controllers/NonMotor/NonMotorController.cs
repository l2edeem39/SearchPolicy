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
using VaultSharp.V1.SystemBackend;
using SearchPolicy.Api.Service;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Data;
using SearchPolicy.Api.Model.NonMotor;
using SearchPolicy.Api.Model;

namespace SearchPolicy.Api.Controllers.NonMotor
{
    [Route("NonMotorSearchPolicy")]
    [ApiController]
    public class NonMotorController : Controller
    {
        private readonly IConfiguration _config;
        ResponseSearchPolicy response = new ResponseSearchPolicy();
        int statusCode = 200;
        public NonMotorController(IConfiguration config)
        {
            _config = config;
        }

        [Route("api/1.0/SearchPolicyByRangeDate")]
        [HttpPost]
        public IActionResult SearchPolicyByRangeDate(string field, string keyword, string keyname, string startYear, string endYear)
        {
            try
            {
                List<SearchAppByRangeDate> SearchPolicys = null;
                var Condb = "NonDB1";
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
                    statusCode = 400;
                    response.ErrorCode = ErrorCode.Code400;
                    response.ErrorMessage = "Field Not Found";
                    response.Status = "1";
                    return StatusCode(statusCode, new { response, data = new List<SearchAppByRangeDate> ()});
                }
                else if (SearchPolicys.Count == 0)
                {
                    statusCode = 400;
                    response.ErrorCode = ErrorCode.Code400;
                    response.ErrorMessage = "Data Not Found";
                    response.Status = "1";
                    return StatusCode(statusCode, new { response, data = new List<SearchAppByRangeDate>() });
                }
                //return Ok(Json(SearchPolicys));
                statusCode = 200;
                response.ErrorCode = "";
                response.ErrorMessage = "";
                response.Status = "0";
                return StatusCode(statusCode, new { response, data = SearchPolicys });
            }
            catch (Exception ex)
            {
                statusCode = 500;
                response.ErrorCode = SystemStatusCode.SystemError;
                response.ErrorMessage = ex.Message;
                response.Status = "1";
                return StatusCode(statusCode, new { response, data = new List<SearchAppByRangeDate>() });
            }            
        }
        //private void GetResponseHeader(string requestTime)
        //{
        //    Response.Headers.Add("requestTime", requestTime);
        //    Response.Headers.Add("responseTime", DateHelper.GetHttpDateTime());
        //}

        //private async void WriteLog(LogEnum.Level level, RequestPreValidateCompulsory requestData, RequestHeader requestHeader, ResponsePreValidate response, int status_code)
        //{
        //    var log = new LogModel
        //    {
        //        Application = EnvironmentShared.GetProjectName(),
        //        TimeStamp = requestDate,
        //        Body = JsonConvert.SerializeObject(requestData),
        //        Header = JsonConvert.SerializeObject(requestHeader),
        //        Response = JsonConvert.SerializeObject(response),
        //        HttpStatus = status_code.ToString()
        //    };
        //    if (level.Equals(LogEnum.Level.Information))
        //        await Logging.LogInformation(log);
        //    else if (level.Equals(LogEnum.Level.Success))
        //        await Logging.LogSuccess(log);
        //}

        //private async void WriteLog(RequestPreValidateCompulsory requestData, RequestHeader requestHeader, ResponsePreValidate response, Exception ex, int status_code)
        //{
        //    var log = new LogModel
        //    {
        //        Application = EnvironmentShared.GetProjectName(),
        //        TimeStamp = requestDate,
        //        Body = JsonConvert.SerializeObject(requestData),
        //        Header = JsonConvert.SerializeObject(requestHeader),
        //        Response = JsonConvert.SerializeObject(response),
        //        Exception = ex,
        //        HttpStatus = status_code.ToString()
        //    };
        //    await Logging.LogError(log);
        //}
    }
}

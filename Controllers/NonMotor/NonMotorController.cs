using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SearchPolicy.Api.Logging;
using SearchPolicy.Model.PreValidate;
using SearchPolicy.Share.EnvironmentShared;
using SearchPolicy.Share.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using VaultSharp.V1.SystemBackend;

namespace SearchPolicy.Api.Controllers.NonMotor
{
    [Route("NonMotorSearchPolicy")]
    [ApiController]
    public class NonMotorController : Controller
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [Route("api/1.0/SearchPolicyByRangeDate")]
        [HttpPost]
        public IEnumerable<WeatherForecast> SearchPolicyByRangeDate(string field, string keyword, string keyname, string startYearTh, string endYearTh)
        {
            startYearTh = !string.IsNullOrEmpty(startYearTh) ? DateTime.Parse("01/01/" + startYearTh).ToString("yyyy", new CultureInfo("en-EN")).Substring(2, 2) : string.Empty;
            endYearTh = !string.IsNullOrEmpty(endYearTh) ? DateTime.Parse("01/01/" + endYearTh).ToString("yyyy", new CultureInfo("en-EN")).Substring(2, 2) : string.Empty;

            //switch (field)
            //{
            //    case "app":
            //        cmd.CommandText = GenarateQuerySearchAppByRangeDate(keyword, keyname, MaxPolicyReturned);
            //        break;
            //    case "pol":
            //        cmd.CommandText = GenarateQuerySearchPolByRangeDate(keyword, keyname, MaxPolicyReturned);
            //        break;
            //    case "idno":
            //        cmd.CommandText = GenarateQuerySearchIdNoByRangeDate(keyword, keyname, startYearTh, endYearTh, MaxPolicyReturned);
            //        break;
            //    case "license":
            //        cmd.CommandText = GenarateQuerySearchbyLicenseByRangeDate(keyword, keyname, startYearTh, endYearTh, MaxPolicyReturned);
            //        break;
            //    case "chassis":
            //        cmd.CommandText = GenarateQuerySearchChassisByRangeDate(keyword, keyname, startYearTh, endYearTh, MaxPolicyReturned);
            //        break;
            //    case "engine":
            //        cmd.CommandText = GenarateQuerySearchEngineNoRangeDate(keyword, keyname, startYearTh, endYearTh, MaxPolicyReturned);
            //        break;
            //    case "asname":
            //        cmd.CommandText = GenarateQuerySearchAsnameByRangeDate(keyname, startYearTh, endYearTh, MaxPolicyReturned);
            //        break;
            //    case "name":
            //        cmd.CommandText = GenarateQuerySearchNameByRangeDate(keyname, startYearTh, endYearTh, MaxPolicyReturned);
            //        break;
            //    case "hldname":
            //        cmd.CommandText = GenarateQuerySearchHldnameRangeDate(keyname, startYearTh, endYearTh, MaxPolicyReturned);
            //        break;
            //}

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
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

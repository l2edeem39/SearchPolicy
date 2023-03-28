using Microsoft.AspNetCore.Mvc;
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
        public CmiController(ICmiService servicecmi)
        {
            _servicecmi = servicecmi;
        }
        [Route("api/1.0/SearchPolicyByRangeDate")]
        [HttpPost]
        public IActionResult SearchPolicyByRangeDate(string field, string keyword, string keyname, string startYearTh, string endYearTh)
        {
            var a = new CmiService();
            var aa = a.SearchCmi();
            var a1 = _servicecmi.SearchCmi();
            return Ok(a1);
        }
    }
}

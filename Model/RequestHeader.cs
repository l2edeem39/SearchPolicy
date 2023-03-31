using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchPolicy.Api.Model
{
    public class RequestHeader
    {
        public string sourceTransID { get; set; }
        //public DateTime requestTime { get; set; }
        public string requestTime { get; set; }
    }

    public class RequestHeaderValidator : AbstractValidator<RequestHeader>
    {
        public RequestHeaderValidator()
        {
            RuleFor(r => r.sourceTransID).NotEmpty().MaximumLength(50).Matches("[a-zA-Z0-9]+_+[a-zA-Z0-9-]+_+[0-9]{2}-[0-9]{2}-[0-9]{4} [0-9]{2}:[0-9]{2}:[0-9]{2}.[0-9]{3}");
            RuleFor(e => e.requestTime).NotEmpty().Matches("^[0-9]{2}-[0-9]{2}-[0-9]{4} [0-9]{2}:[0-9]{2}:[0-9]{2}.[0-9]{3}$");
        }
    }
}

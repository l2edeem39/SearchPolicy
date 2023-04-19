using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchPolicy.Api.Model.Vmi
{
    public class ResponseSearchByRangeDateModel
    {
        public int Itemnumber { get; set; }
        public int Foundcount { get; set; }
        public int Returnedcount { get; set; }
        public string Policynumber { get; set; }
        public string Polyr { get; set; }
        public string Polbr { get; set; }
        public string Polno { get; set; }
        public string Appnumber { get; set; }
        public string Appyr { get; set; }
        public string Appbr { get; set; }
        public string Appno { get; set; }
        public string Fullname { get; set; }
        public string Prename { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Licensenumber { get; set; }
        public string Licenseno { get; set; }
        public string Licenseprv { get; set; }
        public string Chassisno { get; set; }
    }
}

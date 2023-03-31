using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchPolicy.Api.Model.NonMotor
{
    public class SearchAppByRangeDate
    {
        public int Itemnumber { get; set; }
        public int Foundcount { get; set; }
        public int Returnedcount { get; set; }
        public string Policynumber { get; set; }
        public string Polyr { get; set; }
        public string Polbr { get; set; }
        public string Polno { get; set; }
        public string Polpre { get; set; }
        public string PaidDate { get; set; }
        public decimal Net { get; set; }
        public decimal Total { get; set; }
        public string Appnumber { get; set; }
        public string Appyr { get; set; }
        public string Appbr { get; set; }
        public string Appno { get; set; }
        public string Apppre { get; set; }
        public string Fullname { get; set; }
        public string Prename { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string asFullname { get; set; }
        public string asPrename { get; set; }
        public string asFname { get; set; }
        public string asLname { get; set; }
        public string hldFullname { get; set; }
        public string hldPrename { get; set; }
        public string hldFname { get; set; }
        public string hldLname { get; set; }
        public string Licensenumber { get; set; }
        public string Licenseno { get; set; }
        public string Licenseprv { get; set; }
        public string Chassisno { get; set; }
        public DateTime Begindate { get; set; }
        public DateTime Enddate { get; set; }
        public int Insured_sequence { get; set; }
        public string Idno { get; set; }
    }
}

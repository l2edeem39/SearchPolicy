using System;
using System.Collections.Generic;
using System.Text;

namespace SearchPolicy.Share.Utility
{
    public static class ErrorHelper
    {
        public static string GenerateintegrationStatusCode(string code)
        {
            return string.Format("{0}_{1}-{2}", (code == "500") ? "1" : "2", Common.SystemName, code);
        }
    }
    public class Common
    {
        public const string SystemName = "CMI";
    }
    public class SystemStatusCode
    {
        public const string Success = "0";
        public const string Fail = "1";
        public const string FunctionalError4xx = "4xx";
        public const string SystemError = "500";
    }
    public class SystemFormatDate
    {
        public const string DateFormat1 = "dd-MM-yyyy HH:mm:ss.fff";

    }
    public class ErrorMessage
    {
        public const string SystemError = "เกิดความผิดพลาดทางระบบ";
        public const string FindingError = "ไม่พบข้อมูลในระบบ";
        public const string AuthorizationError = "Authorization failed.";
    }
    public class ErrorCode
    {
        public const string Code100 = "100";
        public const string Code200 = "200";
        public const string Code300 = "300";
        public const string Code400 = "400";
    }
}

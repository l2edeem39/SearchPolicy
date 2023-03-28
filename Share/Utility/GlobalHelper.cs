using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace SearchPolicy.Share.Utility
{
    public static class GlobalHelper
    {
        public static bool IsNumeric(string stringToTest)
        {
            int result;

            return int.TryParse(stringToTest, out result);

        }
        public static string GetIPAddressClient()
        {
            IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = Convert.ToString(iPHostEntry.AddressList.FirstOrDefault(a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork));

            return ipAddress;
        }
        public static bool ValueContains<T>(this T item, params T[] items)
        {
            if (items == null) return false;
            return items.Contains(item);
        }
        public static bool chkIDCardNo(string idcard)
        {
            if (Regex.IsMatch(idcard, @"^\d+$"))
            {
                int checkDigit = Convert.ToInt16(idcard.Substring(12, 1));
                string insuredcardNo = idcard.Substring(0, 12);
                int numOrder = 13, sumCheck = 0;
                char[] c = insuredcardNo.ToCharArray();
                for (int i = 0; i < c.Length; i++)
                {
                    sumCheck += Convert.ToInt16(c[i].ToString()) * numOrder;
                    numOrder--;
                }

                int sumMod = 11 - (sumCheck % 11);
                if (sumMod >= 10)
                {
                    sumMod = Convert.ToInt16(sumMod.ToString().Substring(1, 1));
                }
                if (sumMod != checkDigit)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
    public static class HttpStatusHelper
    {
        public static int OK => (int)HttpStatusCode.OK;
        public static int BadRequest => (int)HttpStatusCode.BadRequest;
        public static int InternalServerError => (int)HttpStatusCode.InternalServerError;
    }
}

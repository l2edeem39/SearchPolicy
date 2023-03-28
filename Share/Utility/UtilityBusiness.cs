using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SearchPolicy.Share.Utility
{
    public class UtilityBusiness
    {
        public static string MD5Hash(string Data)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(Data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        public static string GetDigitPolicy(string polno)

        {

            //ตรวจสอบว่าทุก ๆ ตัวอักษรเป็นตัวเลข
            //ตรวจสอบว่าข้อมูลมีทั้งหมด 13 ตัวอักษร

            if (polno.Trim().Length != 12) return "";

            int sumValue = 0;

            for (int i = 0; i < polno.Length; i++)
            { sumValue += int.Parse(polno[i].ToString()) * (13 - i); }
            int v = 11 - (sumValue % 11);
            if (v.ToString().Length > 1) v = int.Parse(v.ToString().Substring(1, 1));

            return v.ToString();

        }
    }
}

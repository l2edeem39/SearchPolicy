using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using SearchPolicy.Api.Model.NonMotor;

namespace SearchPolicy.Api.Service
{
    public class NonMotorService
    {
        SqlConnection connection;
        
        public List<SearchAppByRangeDate> GenarateQuerySearchAppByRangeDate(string keyword, string keyname, int MaxPolicyReturned, string connectionString)
        {
            try
            {
                string appyear = string.Empty;
                string appbr = string.Empty;
                string appno = string.Empty;
                string apppre = string.Empty;

                if (keyword.Length == 14)
                {
                    // length = 14
                    appyear = keyword.Substring(0, 2);
                    appbr = keyword.Substring(2, 3);
                    appno = keyword.Substring(5, 6);
                    apppre = keyword.Substring(11, 3);
                }
                else
                {
                    // length = 20
                    appyear = keyword.Substring(0, 2);
                    appbr = keyword.Substring(2, 3);
                    appno = keyword.Substring(10, 6);
                    apppre = keyword.Substring(17, 3);
                }

                using (connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("sp_GenarateQuerySearchAppByRangeDate", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MaxRows", MaxPolicyReturned);
                    command.Parameters.AddWithValue("@AppYear", appyear);
                    command.Parameters.AddWithValue("@Appbr", appbr);
                    command.Parameters.AddWithValue("@Appno", appno);
                    command.Parameters.AddWithValue("@Apppre", apppre);
                    command.Parameters.AddWithValue("@keyname", keyname != null ? keyname : "");

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    connection.Open();
                    adapter.Fill(dataTable);
                    connection.Close();

                    return GenarateDataFormat(dataTable);
                }
            }
            catch (Exception ex)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return null;
            }
        }

        public List<SearchAppByRangeDate> GenarateQuerySearchPolByRangeDate(string keyword, string keyname, int MaxPolicyReturned, string connectionString)
        {

            try
            {
                string polyear = string.Empty;
                string polbr = string.Empty;
                string polno = string.Empty;
                string polpre = string.Empty;

                if (keyword.Length == 14)
                {
                    // length = 14
                    polyear = keyword.Substring(0, 2);
                    polbr = keyword.Substring(2, 3);
                    polno = keyword.Substring(5, 6);
                    polpre = keyword.Substring(11, 3);
                }
                else
                {
                    // length = 20
                    polyear = keyword.Substring(0, 2);
                    polbr = keyword.Substring(2, 3);
                    polno = keyword.Substring(10, 6);
                    polpre = keyword.Substring(17, 3);
                }

                using (connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("sp_GenarateQuerySearchPolByRangeDate", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MaxRows", MaxPolicyReturned);
                    command.Parameters.AddWithValue("@PolYear", polyear);
                    command.Parameters.AddWithValue("@Polbr", polbr);
                    command.Parameters.AddWithValue("@Polno", polno);
                    command.Parameters.AddWithValue("@Polpre", polpre);
                    command.Parameters.AddWithValue("@keyname", keyname != null ? keyname : "");

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    connection.Open();
                    adapter.Fill(dataTable);
                    connection.Close();

                    return GenarateDataFormat(dataTable);
                }
            }
            catch (Exception ex)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return null;
            }
        }

        public List<SearchAppByRangeDate> GenarateQuerySearchIdNoByRangeDate(string keyword, string keyname, string startYear, string endYear, int MaxPolicyReturned, string connectionString)
        {
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("sp_GenarateQuerySearchIdNoByRangeDate", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MaxRows", MaxPolicyReturned);
                    command.Parameters.AddWithValue("@Idno", keyword);
                    command.Parameters.AddWithValue("@keyname", keyname != null ? keyname : "");
                    command.Parameters.AddWithValue("@startYear", startYear);
                    command.Parameters.AddWithValue("@endYear", endYear);
                    

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    connection.Open();
                    adapter.Fill(dataTable);
                    connection.Close();

                    return GenarateDataFormat(dataTable);
                }
            }
            catch (Exception ex)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                throw;
            }
        }

        public List<SearchAppByRangeDate> GenarateQuerySearchbyLicenseByRangeDate(string keyword, string keyname, string startYear, string endYear, int MaxPolicyReturned, string SubclassCar, string connectionString)
        {
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("sp_GenarateQuerySearchbyLicenseByRangeDate", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MaxRows", MaxPolicyReturned);
                    command.Parameters.AddWithValue("@license", keyword);
                    command.Parameters.AddWithValue("@keyname", keyname != null ? keyname : "");
                    command.Parameters.AddWithValue("@startYear", startYear);
                    command.Parameters.AddWithValue("@endYear", endYear);
                    command.Parameters.AddWithValue("@SubclassCar", SubclassCar);


                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    connection.Open();
                    adapter.Fill(dataTable);
                    connection.Close();

                    return GenarateDataFormat(dataTable);
                }
            }
            catch (Exception ex)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return null;
            }
        }

        public List<SearchAppByRangeDate> GenarateQuerySearchChassisByRangeDate(string keyword, string keyname, string startYear, string endYear, int MaxPolicyReturned, string SubclassCar, string connectionString)
        {
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("sp_GenarateQuerySearchChassisByRangeDate", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MaxRows", MaxPolicyReturned);
                    command.Parameters.AddWithValue("@chassis", keyword);
                    command.Parameters.AddWithValue("@keyname", keyname != null ? keyname : "");
                    command.Parameters.AddWithValue("@startYear", startYear);
                    command.Parameters.AddWithValue("@endYear", endYear);
                    command.Parameters.AddWithValue("@SubclassCar", SubclassCar);


                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    connection.Open();
                    adapter.Fill(dataTable);
                    connection.Close();

                    return GenarateDataFormat(dataTable);
                }
            }
            catch (Exception ex)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return null;
            }
        }

        public List<SearchAppByRangeDate> GenarateQuerySearchEngineNoRangeDate(string keyword, string keyname, string startYear, string endYear, int MaxPolicyReturned, string SubclassCar, string connectionString)
        {
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("sp_GenarateQuerySearchEngineNoRangeDate", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MaxRows", MaxPolicyReturned);
                    command.Parameters.AddWithValue("@EngineNo", keyword != null ? keyword : "");
                    command.Parameters.AddWithValue("@keyname", keyname != null ? keyname : "");
                    command.Parameters.AddWithValue("@startYear", startYear);
                    command.Parameters.AddWithValue("@endYear", endYear);
                    command.Parameters.AddWithValue("@SubclassCar", SubclassCar);


                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    connection.Open();
                    adapter.Fill(dataTable);
                    connection.Close();

                    return GenarateDataFormat(dataTable);
                }
            }
            catch (Exception ex)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return null;
            }
        }

        public List<SearchAppByRangeDate> GenarateQuerySearchAsnameByRangeDate(string keyname, string startYear, string endYear, int MaxPolicyReturned, string connectionString)
        {

            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("sp_GenarateQuerySearchAsnameByRangeDate", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MaxRows", MaxPolicyReturned);
                    command.Parameters.AddWithValue("@keyname", keyname != null ? keyname : "");
                    command.Parameters.AddWithValue("@startYear", startYear);
                    command.Parameters.AddWithValue("@endYear", endYear);


                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    connection.Open();
                    adapter.Fill(dataTable);
                    connection.Close();

                    return GenarateDataFormat(dataTable);
                }
            }
            catch (Exception ex)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return null;
            }
        }

        public List<SearchAppByRangeDate> GenarateQuerySearchNameByRangeDate(string keyname, string startYear, string endYear, int MaxPolicyReturned, string connectionString)
        {
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("sp_GenarateQuerySearchNameByRangeDate", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MaxRows", MaxPolicyReturned);
                    command.Parameters.AddWithValue("@keyname", keyname != null ? keyname : "");
                    command.Parameters.AddWithValue("@startYear", startYear);
                    command.Parameters.AddWithValue("@endYear", endYear);


                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    connection.Open();
                    adapter.Fill(dataTable);
                    connection.Close();

                    return GenarateDataFormat(dataTable);
                }
            }
            catch (Exception ex)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return null;
            }
        }

        public List<SearchAppByRangeDate> GenarateQuerySearchHldnameRangeDate(string keyname, string startYear, string endYear, int MaxPolicyReturned, string connectionString)
        {
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("sp_GenarateQuerySearchHldnameRangeDate", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MaxRows", MaxPolicyReturned);
                    command.Parameters.AddWithValue("@keyname", keyname != null ? keyname : "");
                    command.Parameters.AddWithValue("@startYear", startYear);
                    command.Parameters.AddWithValue("@endYear", endYear);


                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    connection.Open();
                    adapter.Fill(dataTable);
                    connection.Close();

                    return GenarateDataFormat(dataTable);
                }
            }
            catch (Exception ex)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return null;
            }
        }

        public List<SearchAppByRangeDate> GenarateDataFormat(DataTable dt)
        {
            List<SearchAppByRangeDate> SearchAppByRangeDates = new List<SearchAppByRangeDate>();
            try
            {
                int index = 0;
                foreach (DataRow row in dt.Rows)
                {
                    index++;
                    SearchAppByRangeDate searchdata = new SearchAppByRangeDate
                    {
                        Itemnumber = index,
                        Foundcount = index == 1 ? (dt.Rows.Count > 200 ? -1 : 1) : (-1),
                        Returnedcount = index == 1 ? (dt.Rows.Count > 200 ? 200 : dt.Rows.Count) : (-1),
                        Policynumber = (string)row["policynumber"],
                        Polyr = row["polyr"] != null ? (string)row["polyr"] : string.Empty,
                        Polbr = row["polbr"] != null ? (string)row["polbr"] : string.Empty,
                        Polno = row["polno"] != null ? (string)row["polno"] : string.Empty,
                        Polpre = row["polpre"] != null ? (string)row["polpre"] : string.Empty,
                        PaidDate = (string)row["paidDate"],
                        Net = (decimal)row["net"],
                        Total = (decimal)row["total"],
                        Appnumber = (string)row["appnumber"],
                        Appyr = row["appyr"] != null ? (string)row["appyr"] : string.Empty,
                        Appbr = row["appbr"] != null ? (string)row["appbr"] : string.Empty,
                        Appno = row["appno"] != null ? (string)row["appno"] : string.Empty,
                        Apppre = row["polpre"] != null ? (string)row["polpre"] : string.Empty,
                        Fullname = (string)row["fullname"],
                        Prename = row["inspname"] != null ? (string)row["inspname"] : string.Empty,
                        Fname = row["firstname"] != null ? (string)row["firstname"] : string.Empty,
                        Lname = row["lastname"] != null ? (string)row["lastname"] : string.Empty,
                        asFullname = (string)row["asfullname"],
                        asPrename = row["asinspname"] != null ? (string)row["asinspname"] : string.Empty,
                        asFname = row["asfirstname"] != null ? (string)row["asfirstname"] : string.Empty,
                        asLname = row["aslastname"] != null ? (string)row["aslastname"] : string.Empty,
                        hldFullname = (string)row["hldfullname"],
                        hldPrename = (string)row["hldprename"],
                        hldFname = (string)row["hldfname"],
                        hldLname = (string)row["hldlname"],
                        Licensenumber = (string)row["licensenumber"],
                        Licenseno = (string)row["licenseno"],
                        Licenseprv = (string)row["licenseprv"],
                        Chassisno = (string)row["chassisno"],
                        Begindate = row["begindate"] != DBNull.Value ? (DateTime)row["begindate"] : DateTime.MinValue,
                        Enddate = row["enddate"] != DBNull.Value ? (DateTime)row["enddate"] : DateTime.MinValue,
                        Insured_sequence = row["ins_seq"] != null ? (short)row["ins_seq"] : 1,
                        Idno = (string)row["idno"]
                    };

                    SearchAppByRangeDates.Add(searchdata);
                    if (index == 200)
                    {
                        break;
                    }
                }
                return SearchAppByRangeDates;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

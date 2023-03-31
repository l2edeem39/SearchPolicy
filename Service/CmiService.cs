using Microsoft.Data.SqlClient;
using SearchPolicy.Api.Model.Cmi;
using SearchPolicy.Api.Service.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Globalization;
using System.Linq;

namespace SearchPolicy.Api.Service
{
    public class CmiService : ICmiService
    {
        public List<ResponseSearchByRangeDateModel> GenarateQuerySearchPolByRangeDate(string polnumber, string maxCmiReturned)
        {
            var result = new List<ResponseSearchByRangeDateModel>();
            return result;
        }

        public List<ResponseSearchByRangeDateModel> GenarateQueryLicenseByRangeDate(string chassisnumber, string startYear, string endYear, string maxCmiReturned, string conn)
        {
            var result = new List<ResponseSearchByRangeDateModel>();

            string currentDate = DateTime.Now.ToString("yyyy", new CultureInfo("th-TH"));
            string currentYear = !string.IsNullOrEmpty(currentDate.ToString()) ? currentDate.ToString().Substring(2, 2) : string.Empty;

            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_GenarateQueryLicenseByRangeDate", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@licensenumber", chassisnumber);
                    command.Parameters.AddWithValue("@startYear ", startYear);
                    command.Parameters.AddWithValue("@endYear ", endYear);
                    command.Parameters.AddWithValue("@maxCmiReturned ", maxCmiReturned);
                    command.Parameters.AddWithValue("@currentYear ", currentYear);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        var dt = dataSet.Tables[0];
                        result = GetDataTableToModelList(dt);
                    }
                }

                return result;
            }
        }
        public List<ResponseSearchByRangeDateModel> GetDataTableToModelList(DataTable dt)
        {
            int index = 0;
            var result = new List<ResponseSearchByRangeDateModel>();
            int ReturnRow = dt.Rows.Count > 199 ? 200 : dt.Rows.Count;

            foreach (DataRow dr in dt.Rows)
            {
                if (index > 199) continue;
                ResponseSearchByRangeDateModel model = new ResponseSearchByRangeDateModel();
                DataRow row = dt.Rows[index];
                model.Itemnumber = index + 1;
                model.Foundcount = -1;
                model.Returnedcount = -1;
                model.Policynumber = (string)row["polyr"].ToString() + (string)row["polbr"].ToString() + "/กธ/" + (string)row["polno"].ToString();
                model.Polyr = row["polyr"] != null ? (string)row["polyr"].ToString() : string.Empty;
                model.Polbr = row["polbr"] != null ? (string)row["polbr"].ToString() : string.Empty;
                model.Polno = row["polno"] != null ? (string)row["polno"].ToString() : string.Empty;
                model.Appnumber = string.Empty;
                model.Appyr = string.Empty;
                model.Appbr = string.Empty;
                model.Appno = string.Empty;
                model.Fullname = (string)row["prename"].ToString() + (string)row["firstname"].ToString() + " " + (string)row["lastname"].ToString();
                model.Prename = row["prename"] != null ? (string)row["prename"].ToString() : string.Empty;
                model.Fname = row["firstname"] != null ? (string)row["firstname"].ToString() : string.Empty;
                model.Lname = row["lastname"] != null ? (string)row["lastname"].ToString() : string.Empty;
                model.Licensenumber = row["licenseno"] != null ? (string)row["licenseno"].ToString() + " " + (string)row["licenseprv"].ToString() : string.Empty;
                model.Licenseno = row["licenseno"] != null ? (string)row["licenseno"].ToString() : string.Empty;
                model.Licenseprv = row["licenseprv"] != null ? (string)row["licenseprv"].ToString() : string.Empty;
                model.Chassisno = row["chassisno"] != null ? (string)row["chassisno"].ToString() : string.Empty;
                model.Serialno = row["serialno"] != null ? (string)row["serialno"].ToString() : string.Empty;

                result.Add(model);
                index++;
            }
            if (dt.Rows.Count > 0)
            {
                result[0].Returnedcount = ReturnRow;
                result[0].Foundcount = dt.Rows.Count > 200 ? -1 : 1;
            }

            return result;
        }
    }
}

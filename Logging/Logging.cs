using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace SearchPolicy.Api.Logging
{
    public static class Logging
    {
        private static string _logConnectionString;

        //public static void SetLogConnection(string connectionString)
        //{
        //    _logConnectionString = connectionString;
        //}
        public static async Task<int> LogInformation(LogModel model, string connectionString)
        {
            int insert_row = -1;
            try
            {
                _logConnectionString = connectionString;
                insert_row = await Insert(LogEnum.Level.Information, model);
            }
            catch (Exception)
            {

            }
            return insert_row;
        }
        public static async Task<int> LogWarnning(LogModel model, string connectionString)
        {
            int insert_row = -1;
            try
            {
                _logConnectionString = connectionString;
                insert_row = await Insert(LogEnum.Level.Warnning, model);
            }
            catch (Exception)
            {

            }
            return insert_row;
        }
        public static async Task<int> LogError(LogModel model, string connectionString)
        {
            int insert_row = -1;
            try
            {
                _logConnectionString = connectionString;
                insert_row = await Insert(LogEnum.Level.Error, model);
            }
            catch (Exception)
            {

            }
            return insert_row;
        }
        public static async Task<int> LogSuccess(LogModel model, string connectionString)
        {
            int insert_row = -1;
            try
            {
                _logConnectionString = connectionString;
                insert_row = await Insert(LogEnum.Level.Success, model);
            }
            catch (Exception)
            {

            }
            return insert_row;
        }
        private static async Task<int> Insert(LogEnum.Level level, LogModel model)
        {
            int result = 0;
            try
            {
                string exception = string.Empty;
                if (model.Exception != null)
                {
                    exception = JsonConvert.SerializeObject(model.Exception);
                }
                string sql = @"Insert into Log_CPSD(Message, Header, Body, Response, Level, TimeStamp, Exception, Description1, Description2, Description3, ProjectCode, HttpStatus) 
                                        VALUES(@Message, @Header, @Body, @Response, @Level, @TimeStamp, @Exception, @Description1, @Description2, @Description3, @ProjectCode, @HttpStatus)";

                SqlParameter[] param = new SqlParameter[]
                {
                new SqlParameter("@Message",string.IsNullOrEmpty(model.Message)?string.Empty:model.Message),
                new SqlParameter("@Header",string.IsNullOrEmpty(model.Header)?string.Empty:model.Header),
                new SqlParameter("@Body",string.IsNullOrEmpty(model.Body)?string.Empty:model.Body),
                new SqlParameter("@Response",string.IsNullOrEmpty(model.Response)?string.Empty:model.Response),
                new SqlParameter("@Level",level.ToString()),
                new SqlParameter("@TimeStamp",model.TimeStamp),
                new SqlParameter("@Exception",string.IsNullOrEmpty(exception)?string.Empty:exception),
                new SqlParameter("@Description1",string.IsNullOrEmpty(model.Description1)?string.Empty:model.Description1),
                new SqlParameter("@Description2",string.IsNullOrEmpty(model.Description2)?string.Empty:model.Description2),
                new SqlParameter("@Description3",string.IsNullOrEmpty(model.Description3)?string.Empty:model.Description3),
                new SqlParameter("@ProjectCode",string.IsNullOrEmpty(model.Application)?string.Empty:model.Application),
                new SqlParameter("@HttpStatus",string.IsNullOrEmpty(model.HttpStatus)?string.Empty:model.HttpStatus)
                };
                using (SqlConnection conn = new SqlConnection(_logConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.Parameters.AddRange(param);
                        result = await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}

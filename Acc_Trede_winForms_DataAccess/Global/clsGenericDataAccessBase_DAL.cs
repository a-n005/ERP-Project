using Acc_Trade_Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_DataAccess.Global
{
    public static class clsGenericDataAccessBase_DAL
    {
        public static Result<List<T>> ExecuteReader<T>(
       string query,
       Func<SqlDataReader, T> mapper,
       SqlParameter[] parameters = null,
       CommandType commandType = CommandType.Text) 
        {
            List<T> list = new List<T>();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = commandType; 

                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(mapper(reader));
                            }
                        }
                        return Result<List<T>>.Success(list);
                    }
                    catch (Exception ex)
                    {
                        return Result<List<T>>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
        }
        public static Result<T> ExecuteSingle<T>(string query, Func<SqlDataReader, T> mapper, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (reader.Read())
                            {
                                T result = mapper(reader);
                                return Result<T>.Success(result);
                            }
                            else
                            {
                                return Result<T>.Failure("لم يتم العثور على السجل المطلوب.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return Result<T>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
        }
    }
}
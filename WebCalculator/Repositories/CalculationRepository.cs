using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebCalculator.Helpers;
using WebCalculator.Models;
using WebCalculator.Repositories.Interfaces;

namespace WebCalculator.Repositories
{
    public class CalculationRepository : ICalculationRepository
    {
        readonly string _connectionString;

        public CalculationRepository()
        {
            _connectionString = DbHelper.ConnectionString;
        }

        public void Create(Calculation instance)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "CalculationCreate";
                command.Parameters.AddWithValue("@FirstOperand", instance.FirstOperand);
                command.Parameters.AddWithValue("@SecondOperand", instance.SecondOperand);
                command.Parameters.AddWithValue("@Result", instance.Result);
                command.Parameters.AddWithValue("@Operation", instance.Operation);
                command.Parameters.AddWithValue("@CreationDate", instance.CreationDate);
                command.Parameters.AddWithValue("@UserId", instance.UserId);

                SqlParameter id = new SqlParameter("@CalculationId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(id);
                command.ExecuteNonQuery();

                instance.UserId = (int)id.Value;
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "CalculationDelete";
                command.Parameters.AddWithValue("@CalculationId", id);
                command.ExecuteNonQuery();
            }
        }

        public Calculation Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "CalculationSelectById";
                command.Parameters.AddWithValue("@CalculationId", id);
                using (var reader = command.ExecuteReader())
                {
                        if (!reader.Read())
                        {
                            return null; //TODO log?
                        }
                        else
                        {
                            return new Calculation
                            {
                                FirstOperand = reader.GetDouble(reader.GetOrdinal("FirstOperand")),
                                SecondOperand = reader.GetDouble(reader.GetOrdinal("SecondOperand")),
                                Operation = (CalcOperation)reader.GetInt32(reader.GetOrdinal("SecondOperand")),
                                Result = reader.GetDouble(reader.GetOrdinal("SecondOperand")),
                                CreationDate = reader.GetDateTime(reader.GetOrdinal("SecondOperand")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                CalculationId = reader.GetInt32(reader.GetOrdinal("CalculationId"))
                            };
                        }
                }
            }
        }

        public IEnumerable<Calculation> GetListByUserIpAddress(string ipAddress)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "CalculationSelectByUserIpAddress";
                command.Parameters.AddWithValue("@IpAddress", ipAddress);

                List<Calculation> result = new List<Calculation>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Calculation
                        {
                            FirstOperand = reader.GetDouble(reader.GetOrdinal("FirstOperand")),
                            SecondOperand = reader.GetDouble(reader.GetOrdinal("SecondOperand")),
                            Operation = (CalcOperation)reader.GetInt32(reader.GetOrdinal("Operation")),
                            Result = reader.GetDouble(reader.GetOrdinal("Result")),
                            CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            CalculationId = reader.GetInt32(reader.GetOrdinal("CalculationId"))
                        });
                    };
                }

                return result;
            }
        }

        public void Update(Calculation instance)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "CalculationUpdate";
                command.Parameters.AddWithValue("@CalculationId", instance.CalculationId);
                command.Parameters.AddWithValue("@FirstOperand", instance.FirstOperand);
                command.Parameters.AddWithValue("@SecondOperand", instance.SecondOperand);
                command.Parameters.AddWithValue("@Result", instance.Result);
                command.Parameters.AddWithValue("@Operation", instance.Operation);
                command.Parameters.AddWithValue("@CreationDate", instance.CreationDate);
                command.Parameters.AddWithValue("@UserId", instance.UserId);
                command.ExecuteNonQuery();
            }
        }
    }
}
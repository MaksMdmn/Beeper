using System.Data;
using System.Data.SqlClient;
using WebCalculator.Controllers;
using WebCalculator.Models;
using WebCalculator.Helpers;
using WebCalculator.Repositories.Interfaces;

namespace WebCalculator.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = DbHelper.ConnectionString;
        }

        public void Create(User instance)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UserCreate";
                command.Parameters.AddWithValue("@IpAddress", instance.IpAddress);

                SqlParameter id = new SqlParameter("@UserId", SqlDbType.Int)
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
                command.CommandText = "UserDelete";
                command.Parameters.AddWithValue("@UserId", id);
                command.ExecuteNonQuery();
            }
        }

        public User Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UserSelectById";
                command.Parameters.AddWithValue("@UserId", id);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null; //TODO log?
                    }
                    else
                    {
                        return new User
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            IpAddress = reader.GetString(reader.GetOrdinal("IpAddress"))
                        };
                    }
                }
            }
        }

        public User GetUserByIpAddress(string ipAddress)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UserSelectByIpAddress";
                command.Parameters.AddWithValue("@IpAddress", ipAddress);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null; //TODO log?
                    }
                    else
                    {
                        return new User
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            IpAddress = reader.GetString(reader.GetOrdinal("IpAddress"))
                        };
                    }
                }
            }
        }

        public void Update(User instance)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UserUpdate";
                command.Parameters.AddWithValue("@UserId", instance.UserId);
                command.Parameters.AddWithValue("@IpAddress", instance.IpAddress);
                command.ExecuteNonQuery();
            }
        }
    }
}
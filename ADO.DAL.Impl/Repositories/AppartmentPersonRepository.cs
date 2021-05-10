using ADO.DAL.Impl.Infrastructure;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ADO.DAL.Impl.Repositories
{
    class AppartmentPersonRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;
        private readonly static string connectionString = "Server = (localdb)\\mssqllocaldb;Database=Mapsdb;Trusted_Connection=True;";

        public AppartmentPersonRepository()
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();

            _transaction = _connection.BeginTransaction();
        }

        #region baseRegionFunc

        public T ExecuteScalar<T>(string sql, IDictionary<string, object> parameters = null)
        {

            using (SqlCommand command = new SqlCommand(sql, _connection, _transaction))
            {
                FillParameters(parameters, command);
                return (T)command.ExecuteScalar();
            }

        }

        public int ExecuteNonQuery(string sql, IDictionary<string, object> parameters = null)
        {
            using (SqlCommand command = new SqlCommand(sql, _connection, _transaction))
            {
                FillParameters(parameters, command);
                return command.ExecuteNonQuery();
            }
        }

        public T ExecuteSingleRowSelect<T>(
            string sql,
            Func<SqlDataReader, T> rowMapping,
            IDictionary<string, object> parameters = null
            )
        {
            using (SqlCommand command = new SqlCommand(sql, _connection, _transaction))
            {
                FillParameters(parameters, command);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return rowMapping(reader);
                    }
                    else
                    {
                        return default(T);
                    }
                }
            }
        }

        public IList<T> ExecuteSelect<T>(
            string sql,
            Func<SqlDataReader, T> rowMapping,
            IDictionary<string, object> parameters = null
            )
        {

            using (SqlCommand command = new SqlCommand(sql, _connection, _transaction))
            {
                FillParameters(parameters, command);

                using (var reader = command.ExecuteReader())
                {
                    List<T> list = new List<T>(1);
                    while (reader.Read())
                    {
                        list.Add(rowMapping(reader));
                    }

                    return list;
                }
            }
        }

        public AppartmentPerson ExecuteSingleRowSelect(string sql, SqlParameters sqlParameters = null)
        {
            return ExecuteSingleRowSelect(sql, DefaultRowMapping, sqlParameters);
        }

        public IList<AppartmentPerson> ExecuteSelect(string sql, SqlParameters sqlParameters = null)
        {
            return ExecuteSelect(sql, DefaultRowMapping, sqlParameters);
        }

        private static void FillParameters(IDictionary<string, object> parameters, SqlCommand command)
        {
            if (parameters != null)
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
                }
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        #endregion
        
        public AppartmentPerson DefaultRowMapping(SqlDataReader reader)
        {
            return new AppartmentPerson
            {
                AppartmentId = (int)reader["AppartmentId"],
                PersonId = (int)reader["PersonId"]
            };
        }

        public bool Delete(int appartmentId, int personId)
        {
            var res = ExecuteNonQuery(
                "delete from AppartmentPersons where AppartmentId = @id and PersonId = @PersonId",
                new SqlParameters() {
                    { "AppartmentId", appartmentId },
                    { "PersonId", personId },
                });

            return res == 1;
        }

        public IList<AppartmentPerson> GetAll()
        {
            return ExecuteSelect("Select ap.AppartmentId, ap.PersonId from AppartmentPersons ap");
        }

        public IList<AppartmentPerson> GetRelationByPersonId(int personId)
        {
            return ExecuteSelect("Select ap.AppartmentId, ap.PersonId from AppartmentPersons ap where ap.PersonId = @PersonId",
                new SqlParameters()
                    {
                        { "PersonId", personId }
                    });
        }

        public IList<AppartmentPerson> GetRelationByAppartmentId(int appartmentId)
        {
            return ExecuteSelect("Select ap.AppartmentId, ap.PersonId from AppartmentPersons ap where ap.AppartmentId = @AppartmentId",
                new SqlParameters()
                    {
                        { "AppartmentId", appartmentId }
                    });
        }

        public AppartmentPerson GetById(int appartmentId, int personId)
        {
            return ExecuteSingleRowSelect(
                   "Select ap.AppartmentId, ap.PersonId from AppartmentPersons ap where ap.AppartmentId = @AppartmentId AND ap.PersonId = @PersonId",
                   new SqlParameters()
                   {
                        { "AppartmentId", appartmentId },
                        { "PersonId", personId }
                   }
               );
        }

        public int Insert(AppartmentPerson entity)
        {
            var newEntityId = (int)
                ExecuteNonQuery(
                        "insert into AppartmentPersons (AppartmentId,PersonId) values (@AppartmentId,@PersonId) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            { "AppartmentId", entity.AppartmentId },
                            { "PersonId", entity.PersonId }
                        }
                    );
            
            return newEntityId;
        }
    }
}

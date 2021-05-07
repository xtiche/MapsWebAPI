using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Entity.Models.Abstract;

namespace ADO.DAL.Impl.Infrastructure
{
    public abstract class BaseRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransactionManager _transactionManager;

        public BaseRepository(SqlConnection connection, SqlTransactionManager transactionManager)
        {
            _connection = connection;
            _transactionManager = transactionManager;
        }

        public T ExecuteScalar<T>(string sql, IDictionary<string, object> parameters = null)
        {
            using (SqlCommand command = new SqlCommand(sql, _connection, _transactionManager.CurrentTransaction))
            {
                FillParameters(parameters, command);

                return (T)command.ExecuteScalar();
            }
        }

        public int ExecuteNonQuery(string sql, IDictionary<string, object> parameters=null)
        {
            using (SqlCommand command = new SqlCommand(sql, _connection, _transactionManager.CurrentTransaction))
            {
                FillParameters(parameters, command);

                return command.ExecuteNonQuery();
            }
        }

        public T ExecuteSingleRowSelect<T>(
            string sql,
            Func<SqlDataReader,T> rowMapping, 
            IDictionary<string, object> parameters = null
            )
        {
            using (SqlCommand command = new SqlCommand(sql, _connection, _transactionManager.CurrentTransaction))
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
            using (SqlCommand command = new SqlCommand(sql, _connection, _transactionManager.CurrentTransaction))
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


        public TEntity ExecuteSingleRowSelect(string sql, SqlParameters sqlParameters = null)
        {
            return ExecuteSingleRowSelect(sql, DefaultRowMapping, sqlParameters);
        }

        public IList<TEntity> ExecuteSelect(string sql, SqlParameters sqlParameters = null)
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


        public TKey Upsert(TEntity entity)
        {
            if (Object.Equals(entity.Id, default(TKey)))
                return Insert(entity);
            else
            {
                if (Update(entity))
                    return entity.Id;
                else
                    return default(TKey);
            }
        }

        public abstract TKey Insert(TEntity entity);
        public abstract bool Update(TEntity entity);
        public abstract TEntity DefaultRowMapping(SqlDataReader reader);

    }
}
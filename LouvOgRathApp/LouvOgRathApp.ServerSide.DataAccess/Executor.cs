/**************************************************************************************************
*  Author: Mads Mikkel Rasmussen (mara@aspit.dk), github: https://github.com/Mara-AspIT/          *
*  Solution: .NET version: 4.7.1, C# version: 7.1                                                 *
*  Visual Studio version: Visual Studio Enterprise 2017, version 15.4.5                           *
*  Repository:                                     *
**************************************************************************************************/
using LouvOgRathApp.Shared.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace LouvOgRathApp.ServerSide.DataAccess
{
    /// <summary>Executes SQL queries against a SQL Server database.</summary>
    public class Executor
    {
        #region Fields
        /// <summary>The connection string used to identify the database on a SQL Server. Is readonly. Available for read in deriving classes. Should not be inline assigned.</summary>
        protected readonly string connectionString;
        #endregion


        #region Constructors
        /// <summary>Creates a new <see cref="Executor"/> object, using the provided connection string. Should not be called from outside this namespace.</summary>
        /// <param name="connectionString">The connection string used to connect to a SQL Server.</param>
        internal Executor(string connectionString)
        {
            (bool isNotNull, string errorMsg) = ConnectionNullCheck(connectionString);
            if (isNotNull && String.IsNullOrEmpty(errorMsg))
            {
                this.connectionString = connectionString;
            }
            else
            {
                throw new DataAccessException(errorMsg, new Exception());
            }
        }
        #endregion


        #region Methods
        /// <summary>Executes the provided SQL query. Should not be called from outside this namespace. Can be overridden.</summary>
        /// <param name="sql">The SQL query to execute.</param>
        /// <returns>A <see cref="DataSet"/> containing any data returned from the database.</returns>
        public virtual DataSet Execute(string sql)
        {
            try
            {
                DataSet resultSet;
                using (SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(sql, new SqlConnection(connectionString))))
                {
                    resultSet = new DataSet();
                    adapter.Fill(resultSet);
                }
                return resultSet;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }


        private (bool, string) ConnectionNullCheck(string connectionString)
        {
            if (!String.IsNullOrEmpty(connectionString))
            {
                return (true, string.Empty);
            }
            else
            {
                return (false, "it cant be empty");
            }
        }
        #endregion
    }
}
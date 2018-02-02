using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouvOgRathApp.ServerSide.DataAccess
{
    /// <summary>
    /// used to make connection with the executer class
    /// </summary>
    public abstract class RepositoryBase
    {
        #region fields
        protected readonly Executor executor;
        #endregion


        #region constructer
        /// <summary>
        /// ment to be used as a caller to the Executer class
        /// </summary>
        /// <param name="nameOfConfigFileConnectionString"></param>
        public RepositoryBase(string nameOfConfigFileConnectionString)
        {
            executor = new Executor(GetConString(nameOfConfigFileConnectionString));
        }
        #endregion
        
        
        #region propetis
        protected Executor Executor
        {
            get { return executor; }
        }
        #endregion
        
        
        #region methods
        private string GetConString(string name)
        {
            try
            {
            return System.Configuration.ConfigurationManager.ConnectionStrings[name].ConnectionString;
            }
            catch (Exception ex)
            {

                throw new DataAccessException("you have to make a config file and you have to make a connection string with the right name", ex);
            }
        }
        #endregion
    }
}

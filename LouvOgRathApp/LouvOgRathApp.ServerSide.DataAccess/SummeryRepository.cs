using LouvOgRathApp.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouvOgRathApp.ServerSide.DataAccess
{
    public class SummeryRepository : RepositoryBase
    { 
        #region contructer
        public SummeryRepository(string nameOfConfigFileConnectionString) : base(nameOfConfigFileConnectionString)
        {
        }
        #endregion


        #region methods
        internal (bool, string) ExecutorIsValid(Executor executor)
        {
            if (executor != null)
            {
                return (true, String.Empty);
            }
            else
            {
                return (false, "it cant be null");
            }
        }
        /// <summary>
        /// used for getting all summerys from the database the foreign caseId 
        /// </summary>
        /// <param name="caseId"></param>
        /// <returns></returns>
        public List<MettingSummery> GetAllSummerysById(int caseId)
        {
            List<MettingSummery> mettingSummery = new List<MettingSummery>();
            DataSet ds = Executor.Execute($"EXECUTE GetSummerysById @CaseId = {caseId}");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                MettingSummery tempSum = new MettingSummery(dr.Field<string>("Resumé"), dr.Field<string>("CaseName"), dr.Field<int>("CaseId"));
                mettingSummery.Add(tempSum);
            }
            return mettingSummery;
        }
        /// <summary>
        /// used to save summery in the database
        /// </summary>
        /// <param name="summery"></param>
        public void SafeSummery(MettingSummery summery)
        {
            Executor.Execute($"INSERT INTO Summery(CaseId, Resumé) VALUES({summery.Case.Id}, '{summery.Resumé}')");
        }
        /// <summary>
        /// used for getting summery to the client (get only 3) from the data with the foreign caseId
        /// </summary>
        /// <param name="caseId"></param>
        /// <returns></returns>
        public List<MettingSummery> GetSummeryToClient(int caseId)
        {
            List<MettingSummery> mettingSummery = new List<MettingSummery>();
            DataSet ds = Executor.Execute($"EXECUTE Last3ForTheClient @CaseId = {caseId}");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                MettingSummery tempSum = new MettingSummery(dr.Field<string>("Resumé"), dr.Field<string>("CaseName"), dr.Field<int>("CaseId"));
                mettingSummery.Add(tempSum);
            }
            return mettingSummery;
        }
        #endregion
    }
}

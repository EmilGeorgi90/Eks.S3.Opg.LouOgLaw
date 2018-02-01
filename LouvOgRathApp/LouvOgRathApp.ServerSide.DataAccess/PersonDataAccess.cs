using LouvOgRathApp.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouvOgRathApp.ServerSide.DataAccess
{
    public class PersonDataAccess
    {
        #region fields
        private readonly Executor executor;
        #endregion


        #region contructer
        public PersonDataAccess()
        {
            executor = new Executor(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=louvOgLaw;Integrated Security=True");
        }
        #endregion


        #region propetis
        public Executor Executor
        {
            get { return executor; }
        }
        #endregion


        #region methods
        /// <summary>
        /// Gets all persons from the database
        /// </summary>
        /// <returns></returns>
        public List<Person> GetAllPersons()
        {
            List<Person> person = new List<Person>();
            DataSet ds = Executor.Execute("Select PersonId ,Fullname, rolesname from Person INNER JOIN Roles on person.rolesId = roles.rolesId");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                person.Add(new Person(dr.Field<string>("Fullname"), (RoleKind)Enum.Parse(typeof(RoleKind), dr.Field<string>("rolesname")), dr.Field<int>("PersonId")));
            }
            return person;
        }
        #endregion
    }
}

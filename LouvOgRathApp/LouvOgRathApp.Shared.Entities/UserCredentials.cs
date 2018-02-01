using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouvOgRathApp.Shared.Entities
{
    [Serializable]
    public class UserCredentials : IPersistable
    {
        private readonly int id;
        private string username;
        private string password;
        private RoleKind? roleKind;
        private Person person;



        /// <summary>
        /// basic login form
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="roleKind_"></param>
        public UserCredentials(string username, string password, RoleKind? roleKind_)
        {
            RoleKind_ = roleKind_;
            Password = password;
            Username = username;
        }
        /// <summary>
        /// login with id
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="rolekind_"></param>
        /// <param name="id"></param>
        public UserCredentials(string username, string password, RoleKind rolekind_, int id) :this(username, password, rolekind_)
        {
            this.id = id;
        }
        /// <summary>
        /// login with person
        /// </summary>
        /// <param name="roleKind_"></param>
        /// <param name="password"></param>
        /// <param name="username"></param>
        /// <param name="person"></param>
        public UserCredentials(RoleKind? roleKind_, string password, string username, Person person) : this(username, password, roleKind_)
        {
            Person = person;
        }

        public RoleKind? RoleKind_
        {
            get { return roleKind; }
            set { roleKind = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public Person Person
        {
            get { return person; }
            set { person = value; }
        }

        public int Id => id;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouvOgRathApp.Shared.Entities
{
    [Serializable]
    public class Person : IPersistable
    {
        private readonly int id;
        private string fullname;
        private string email;
        private string phoneNumber;



        private RoleKind RoleKind;


        /// <summary>
        /// standard person
        /// </summary>
        /// <param name="fullname"></param>
        /// <param name="rolekind"></param>
        public Person(string fullname, RoleKind rolekind)
        {
            Rolekind = rolekind;
            Fullname = fullname;
        }
        /// <summary>
        /// person with id
        /// </summary>
        /// <param name="fullname"></param>
        /// <param name="rolekind"></param>
        /// <param name="id"></param>
        public Person(string fullname, RoleKind rolekind, int id) : this(fullname, rolekind)
        {
            this.id = id;
        }
        /// <summary>
        /// full person info
        /// </summary>
        /// <param name="fullname"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="rolekind"></param>
        public Person(string fullname, string phoneNumber, string email, RoleKind rolekind) : this(fullname, rolekind)
        {
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string Fullname
        {
            get
            {
                return fullname;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    fullname = value;
                }
                else
                {
                    throw new ArgumentException("it cant be null or whitespace");
                }
            }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public override string ToString()
        {
            return $"{Fullname}";
        }
        public RoleKind Rolekind
        {
            get { return RoleKind; }
            set { RoleKind = value; }
        }
    }
}

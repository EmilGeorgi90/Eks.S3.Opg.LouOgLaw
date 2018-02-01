using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouvOgRathApp.Shared.Entities
{
    [Serializable]
    public class Case : IPersistable
    {
        readonly int id;
        string caseName;
        List<RoleKind> roles;
        CaseKind caseKind;
        private Person clientPerson;
        private Person lawyerPerson;
        private Person secretaryPerson;


        /// <summary>
        /// standard case
        /// </summary>
        /// <param name="caseName"></param>
        /// <param name="caseKind"></param>
        public Case(string caseName, CaseKind caseKind)
        {
            CaseName = caseName;
            CaseKind = caseKind;
        }

        /// <summary>
        /// case with list<roleKind>'s
        /// </summary>
        /// <param name="caseName"></param>
        /// <param name="caseKind"></param>
        /// <param name="roles"></param>
        public Case(string caseName, CaseKind caseKind, List<RoleKind> roles)
        {
            CaseName = caseName;
            CaseKind = caseKind;
            Roles = roles;
        }

        /// <summary>
        /// case with roles and id
        /// </summary>
        /// <param name="caseName"></param>
        /// <param name="caseKind"></param>
        /// <param name="roles"></param>
        /// <param name="id"></param>
        public Case(string caseName, CaseKind caseKind, List<RoleKind> roles, int id) : this(caseName, caseKind, roles)
        {
            this.id = id;
        }
        /// <summary>
        /// case with Persons instead of roles
        /// </summary>
        /// <param name="caseName"></param>
        /// <param name="caseKind"></param>
        /// <param name="secretaryPerson"></param>
        /// <param name="lawyerPerson"></param>
        /// <param name="clientPerson"></param>
        /// <param name="id"></param>
        public Case(string caseName, CaseKind caseKind, Person secretaryPerson, Person lawyerPerson, Person clientPerson, int id) : this(caseName, caseKind)
        {
            this.id = id;
            SecretaryPerson = secretaryPerson;
            LawyerPerson = lawyerPerson;
            ClientPerson = clientPerson;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }
        public string CaseName
        {
            get
            {
                return caseName;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    caseName = value;
                }
                else
                {
                    throw new ArgumentException("it cant be null or white space");
                }
            }
        }
        public CaseKind CaseKind
        {
            get
            {
                return caseKind;
            }
            set
            {
                caseKind = value;
            }
        }
        public List<RoleKind> Roles
        {
            get
            {
                return roles;
            }
            set
            {
                if (value.Count >= Enum.GetNames(typeof(RoleKind)).Length)
                {
                    roles = value;
                }
                else
                {
                    throw new ArgumentException("you have to have each of the roles before making a case");
                }
            }
        }
        public Person SecretaryPerson
        {
            get { return secretaryPerson; }
            set { secretaryPerson = value; }
        }

        public Person LawyerPerson
        {
            get { return lawyerPerson; }
            set { lawyerPerson = value; }
        }

        public Person ClientPerson
        {
            get { return clientPerson; }
            set { clientPerson = value; }
        }
    }
}

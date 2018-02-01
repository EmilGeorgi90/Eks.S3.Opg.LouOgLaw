using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouvOgRathApp.Shared.Entities
{
    [Serializable]
    public class MettingSummery : IPersistable
    {
        readonly int id;
        string resumé;
        private string caseName;
        Case @case;
        /// <summary>
        /// summery with only the summery
        /// </summary>
        /// <param name="resumé"></param>
        public MettingSummery(string resumé)
        {
            Resumé = resumé;
        }
        /// <summary>
        /// summery with a casename
        /// </summary>
        /// <param name="resumé"></param>
        /// <param name="caseName"></param>
        public MettingSummery(string resumé, string caseName) : this(resumé)
        {
            CaseName = caseName;
        }
        /// <summary>
        /// summery with an id
        /// </summary>
        /// <param name="summery"></param>
        /// <param name="caseName"></param>
        /// <param name="id"></param>
        public MettingSummery(string summery, string caseName, int id) :this(summery, caseName)
        {
            this.id = id;
        }
        /// <summary>
        /// summery with a case
        /// </summary>
        /// <param name="resumé"></param>
        /// <param name="caseName"></param>
        /// <param name="case"></param>
        public MettingSummery(string resumé, string caseName, Case @case) : this(resumé, caseName)
        {
            Case = @case;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string Resumé
        {
            get
            {
                return resumé;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    resumé = value;
                }
                else
                {
                    throw new ArgumentNullException("it can't be null or whitespace");
                }
            }
        }
        public string CaseName
        {
            get { return caseName; }
            set { caseName = value; }
        }
        public Case Case
        {
            get { return @case; }
            set { @case = value; }
        }
        public override string ToString()
        {
            return $"{Resumé}, {CaseName}";
        }
    }
}

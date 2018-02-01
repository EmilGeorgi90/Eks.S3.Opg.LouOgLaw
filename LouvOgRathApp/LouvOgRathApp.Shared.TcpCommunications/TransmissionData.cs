/**************************************************************************************************
*  Author: Mads Mikkel Rasmussen (mara@aspit.dk), github: https://github.com/Mara-AspIT/          *
*  Solution: .NET version: 4.7.1, C# version: 7.1                                                 *
*  Visual Studio version: Visual Studio Enterprise 2017, version 15.4.5                           *
*  Repository: https://github.com/Mara-AspIT/Mara.Modules.git                                     *
**************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using LouvOgRathApp.Shared.Entities;

namespace LouvOgRathApp.Shared.TcpCommunications
{
    /// <summary>Wrapper for business logic object(s). Can be inherited.</summary>
    [Serializable]
    public class TransmissionData
    {

        #region Constructors
        /// <summary>Creates a new object of <see cref="TransmissionData"/> with the provided <see cref="IPersistable"/>. <see cref="Entities"/> is set to null.</summary>
        /// <param name="entity">The object implementing <see cref="IPersistable"/> to be wrapped in <see cref="Entity"/>.</param>
        public TransmissionData(IPersistable entity)
        {
            Entity = entity;
            Entities = null;
        }

        /// <summary>Creates a new object of <see cref="TransmissionData"/> with the provided collection of <see cref="IPersistable"/>s. <see cref="Entity"/> is set to null.</summary>
        /// <param name="entity">The collection of objects implementing <see cref="IPersistable"/> to be wrapped in <see cref="Entities"/>.</param>
        public TransmissionData(IEnumerable<IPersistable> entities)
        {
            Entities = entities.ToArray();
            Entity = null;
        } 
        #endregion

        /// <summary>Gets the entity implementing <see cref="IPersistable"/>.</summary>
        public virtual IPersistable Entity { get; protected set; }


        /// <summary>Gets the collection of entities implementing <see cref="IPersistable"/> as an array of <see cref="IPersistable"/>.</summary>
        public virtual IPersistable[] Entities { get; protected set; }

        public static implicit operator Case(TransmissionData data)
        {
            throw new NotImplementedException();
        }
    }
}
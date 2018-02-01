/**************************************************************************************************
*  Author: Mads Mikkel Rasmussen (mara@aspit.dk), github: https://github.com/Mara-AspIT/          *
*  Solution: .NET version: 4.7.1, C# version: 7.1                                                 *
*  Visual Studio version: Visual Studio Enterprise 2017, version 15.4.5                           *
*  Repository: https://github.com/Mara-AspIT/Mara.Modules.git                                     *
**************************************************************************************************/

using System;

namespace LouvOgRathApp.Shared.TcpCommunications
{
    /// <summary>Represents a request to a server by a client. Can be inherited.</summary>
    [Serializable]
    public class ClientRequest
    {
        #region Fields
        /// <summary>The action requested by a client.</summary>
        protected RequestedAction requestedAction;

        /// <summary>Any business logic object(s) (entity or entities) to transmit to the server.</summary>
        protected TransmissionData data;
        #endregion

        #region Constructors
        /// <summary>Creates a new object of <see cref="ClientRequest"/> with the provided <see cref="RequestedAction"/> and <see cref="TransmissionData"/> (can be omitted). </summary>
        /// <param name="requestedAction">The action the server must take.</param>
        /// <param name="data">Any data that should be transmitted along with the request.</param>
        public ClientRequest(RequestedAction requestedAction, TransmissionData data = null)
        {
            RequestedAction = requestedAction;
            Data = data;
        }
        #endregion

        #region Properties
        /// <summary>
        /// The actions you want the server to do
        /// </summary>
        public virtual RequestedAction RequestedAction { get => requestedAction; set => requestedAction = value; }

        /// <summary>
        /// the class the gonna be send and recieved
        /// </summary>
        public virtual TransmissionData Data { get => data; protected set => data = value; } 
        #endregion
    }
}
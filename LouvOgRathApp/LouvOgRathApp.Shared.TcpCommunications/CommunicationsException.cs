/**************************************************************************************************
*  Author: Mads Mikkel Rasmussen (mara@aspit.dk), github: https://github.com/Mara-AspIT/          *
*  Solution: .NET version: 4.7.1, C# version: 7.1                                                 *
*  Visual Studio version: Visual Studio Enterprise 2017, version 15.4.5                           *
*  Repository:                                     *
**************************************************************************************************/

using System;
using System.Runtime.Serialization;

namespace LouvOgRathApp.Shared.TcpCommunications
{
    /// <summary>Represents an error during execution of TCP communication. Will always contain an inner exception.</summary>
    public class CommunicationsException: Exception
    {
        #region Constructors
        /// <summary>Creates a new object of <see cref="CommunicationsException"/> with the provided message and inner exception.</summary>
        /// <param name="message">The message describing the cause of this exception.</param>
        /// <param name="innerException">The exception to be wrapped in this exception.</param>
        public CommunicationsException(string message, Exception innerException) : base(message, innerException)
        {
            // Do not modify.
        }
        #endregion
    }
}
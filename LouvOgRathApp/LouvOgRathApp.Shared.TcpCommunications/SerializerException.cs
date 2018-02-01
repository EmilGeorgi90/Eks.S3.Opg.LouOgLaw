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
    /// <summary>The exception that is thrown when an error occurs during serialization or deserialization. Should not be visible outside this namespace.</summary>
    [Serializable]
    internal class SerializerException: Exception
    {
        /// <summary>Creates a new object of <see cref="SerializationException"/> with the provided message and exception to wrap.</summary>
        /// <param name="message">The message describing the cause of the communications error.</param>
        /// <param name="innerException">The exception to wrap as an inner exception.</param>
        public SerializerException(string message, Exception innerException) : base(message, innerException)
        {
            // Do not modify.
        }
    }
}
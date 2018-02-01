/**************************************************************************************************
*  Author: Mads Mikkel Rasmussen (mara@aspit.dk), github: https://github.com/Mara-AspIT/          *
*  Solution: .NET version: 4.7.1, C# version: 7.1                                                 *
*  Visual Studio version: Visual Studio Enterprise 2017, version 15.4.5                           *
*  Repository:                                     *
**************************************************************************************************/

using System;
using System.Net;
using System.Net.Sockets;
using LouvOgRathApp.Shared.Entities;
using LouvOgRathApp.Shared.TcpCommunications;

namespace LouvOgRathApp.ClientSide.ClientControllers
{
    /// <summary>Abstract base class to handle communication with the remote server.</summary>
    public abstract class ServerCommunicationsHandler
    {
        #region Constants
        /// <summary>Upper limit of transmission size. Accessible for read in deriving classes.</summary>
        protected const int bufferSize = CommunicationConstants.BufferLimit;

        #endregion


        #region Fields
        /// <summary>The object of <see cref="TcpClient"/> used to contacting and transmitting data to the remote server. Accessible in deriving classes.</summary>
        protected TcpClient client;

        /// <summary>The remote endpont to the server. Accessible in deriving classes.</summary>
        protected IPEndPoint remoteEndPoint;

        /// <summary>The bytes recieved from the remote server as a response. Accessible in deriving classes.</summary>
        protected  byte[] recieveBuffer = new byte[bufferSize];
        #endregion


        #region Constructors
        /// <summary>Creates a new object of <see cref="ServerCommunicationsHandler"/> with the provided <see cref="remoteEndPoint"/>. Attempts to establish a connection to a server located at the provided <see cref="remoteEndPoint"/>. Should not be visible outside this namespace.</summary>
        /// <param name="remoteEndPoint"></param>
        internal ServerCommunicationsHandler(IPEndPoint remoteEndPoint)
        {
            try
            {
                client = new TcpClient();   // Do not attempt to create a local endpoint. This is handled automatically this way.
                client.Connect(remoteEndPoint);
                this.remoteEndPoint = remoteEndPoint;
            }
            catch (Exception ex)
            {
                throw new CommunicationsException(ex.Message, ex);
            }
        }
        #endregion
        

        #region Methods
        /// <summary>Transmits the provided byte array to the remote endpoint. Overridable.</summary>
        /// <param name="transmitBuffer">The byte array to transmit to the remote endpoint.</param>
        /// <returns>A byte array with the response data from the server.</returns>
        protected byte[] Transmit(byte[] transmitBuffer)
        {
            try
            {

                Array.Clear(recieveBuffer, index: 0, length: recieveBuffer.Length);
                client.GetStream().Write(transmitBuffer, offset: 0, size: transmitBuffer.Length);
                client.GetStream().Read(recieveBuffer, offset: 0, size: bufferSize);
                return recieveBuffer;
            }
            catch (Exception ex)
            {
                throw new CommunicationsException(ex.Message, ex);
            }
        }
        #endregion
    }
}
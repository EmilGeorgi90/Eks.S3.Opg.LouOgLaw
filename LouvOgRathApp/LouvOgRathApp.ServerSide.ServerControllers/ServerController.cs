/**************************************************************************************************
*  Author: Mads Mikkel Rasmussen (mara@aspit.dk), github: https://github.com/Mara-AspIT/          *
*  Solution: .NET version: 4.7.1, C# version: 7.1                                                 *
*  Visual Studio version: Visual Studio Enterprise 2017, version 15.4.5                           *
*  Repository:                                     *
**************************************************************************************************/

using System;
using System.Net;
using System.Collections.Generic;
using System.Net.Sockets;
using LouvOgRathApp.ServerSide.DataAccess;
using LouvOgRathApp.Shared.Entities;
using LouvOgRathApp.Shared.TcpCommunications;
using System.Configuration;

namespace LouvOgRathApp.ServerSide.ServerControllers
{
    /// <summary>Handles incoming requests from clients. Should not be visible outside this namespace.</summary>
    internal class ServerController
    {
        #region Constants
        /// <summary>The upper limit to both <see cref="recieveBuffer"/> and <see cref="sendBuffer"/>.</summary>
        const int bufferLimit = CommunicationConstants.BufferLimit;
        #endregion


        #region Fields
        /// <summary>The listener.</summary>
        protected TcpListener listener;

        /// <summary>The currently connected client.</summary>
        protected TcpClient connectedClient;

        // TODO: Add nescessary new repositories here:

        /// <summary>The bytes loaded from a client stream.</summary>
        protected byte[] recieveBuffer = new byte[bufferLimit];

        /// <summary>The bytes to transmit through the stream of the <see cref="connectedClient"/>.</summary>
        protected byte[] sendBuffer = new byte[bufferLimit];
        #endregion


        #region Constructors
        /// <summary>
        /// The server controller Get/send serielsed data, and communicate with client
        /// </summary>
        internal ServerController()
        {
            Console.WriteLine("Starting server...");
            try
            {

                (IPAddress address, int port) = ConfigManager();
                listener = new TcpListener(new IPEndPoint(address, port));
            }
            catch (Exception ex)
            {
                throw new CommunicationsException(ex.Message, ex);
            }
        }
        #endregion


        #region Methods
        /// <summary>Starts the internal <see cref="TcpListener"/>. Can be overridden.</summary>
        internal virtual void Run()
        {
            // Do not modify this method.
            try
            {
                listener.Start();
            }
            catch (Exception)
            {
                throw;
            }
            Console.WriteLine("Server started. Waiting for a client to connect...");
            while (true)
            {
                try
                {
                    using (connectedClient = listener.AcceptTcpClient())
                    {
                        Console.WriteLine("A client has connected.");
                        connectedClient.Client.Receive(recieveBuffer);
                        ClientRequest clientRequest = Serializer<ClientRequest>.Deserialize(recieveBuffer);
                        Array.Clear(recieveBuffer, index: 0, length: recieveBuffer.Length);
                        Process(clientRequest);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>Processes a client request.</summary>
        /// <param name="clientRequest">The client request to process.</param>
        protected virtual void Process(ClientRequest clientRequest)
        {
            if (clientRequest is null)
            {
                throw new ArgumentNullException(nameof(clientRequest));
            }
            try
            {
                switch (clientRequest.RequestedAction)
                {
                    case RequestedAction.GetAllCases:
                        CasesRepository caseDataAccess = new CasesRepository("constring");
                        List<Case> cases = caseDataAccess.GetAllCase();
                        Case[] caseArray = cases.ToArray();
                        TransmissionData transmission = new TransmissionData(caseArray);
                        RespondToClient(transmission);
                        break;
                    case RequestedAction.GetAllSummerysById:
                        SummeryRepository summeryDataAccess = new SummeryRepository("constring");
                        Case summery = (Case)clientRequest.Data.Entity;
                        List<MettingSummery> summerys = summeryDataAccess.GetAllSummerysById(summery.Id);
                        TransmissionData transmissionSum = new TransmissionData(summery);
                        RespondToClient(transmissionSum);
                        break;
                    case RequestedAction.SaveNewSummery:
                        SummeryRepository summeryData = new SummeryRepository("constring");
                        foreach (MettingSummery item in clientRequest.Data.Entities)
                        {
                            summeryData.SafeSummery(item);
                        }
                        RespondToClient(clientRequest.Data);
                        break;
                    case RequestedAction.SaveNewCase:
                        CasesRepository caseData = new CasesRepository("constring");
                        foreach (Case item in clientRequest.Data.Entities)
                        {
                            caseData.AddCase(item);
                        }
                        RespondToClient(clientRequest.Data);
                        break;
                    case RequestedAction.GetCasesForTheClient:
                        caseDataAccess = new CasesRepository("constring");
                        UserCredentials clientName = (UserCredentials)clientRequest.Data.Entity;
                        cases = caseDataAccess.GetCases(clientName.Username);
                        transmission = new TransmissionData(cases.ToArray());
                        RespondToClient(transmission);
                        break;
                    case RequestedAction.GetUserCredentialsAttempt:
                        UserCredentialsRepository access = new UserCredentialsRepository("constring");
                        RoleKind? rolekind;
                        bool IsUser;
                        List<UserCredentials> userInfos = new List<UserCredentials>();
                        UserCredentials user;
                        TransmissionData data;
                        foreach (UserCredentials uInfo in clientRequest.Data.Entities)
                        {
                            (IsUser, rolekind) = access.UserCredentialsLoginAttemp(uInfo);
                            user = new UserCredentials(uInfo.Username, uInfo.Password, rolekind);
                            userInfos.Add(user);
                            data = new TransmissionData(userInfos);
                            RespondToClient(data);
                            break;
                        }
                        break;
                    case RequestedAction.CreateUser:
                        access = new UserCredentialsRepository("constring");
                        userInfos = new List<UserCredentials>();
                        foreach (UserCredentials uInfo in clientRequest.Data.Entities)
                        {
                            access.CreateNewUser(uInfo);
                            user = new UserCredentials(uInfo.Username, uInfo.Password, uInfo.RoleKind_);
                            userInfos.Add(user);
                            data = new TransmissionData(userInfos);
                            RespondToClient(data);
                        }
                        break;
                    case RequestedAction.GetSummerysById:
                        summeryDataAccess = new SummeryRepository("constring");
                        summery = (Case)clientRequest.Data.Entity;
                        summerys = summeryDataAccess.GetAllSummerysById(summery.Id);
                        transmissionSum = new TransmissionData(summerys);
                        RespondToClient(transmissionSum);
                        break;
                    case RequestedAction.GetPersons:
                        PersonRepository person = new PersonRepository("constring");
                        List<Person> persons = person.GetAllPersons();
                        data = new TransmissionData(persons);
                        RespondToClient(data);
                        break;
                    case RequestedAction.GetSummeryToClient:
                        summeryDataAccess = new SummeryRepository("constring");
                        summery = (Case)clientRequest.Data.Entity;
                        summerys = summeryDataAccess.GetSummeryToClient(summery.Id);
                        transmissionSum = new TransmissionData(summerys);
                        RespondToClient(transmissionSum);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Responds to the <see cref="connectedClient"/> with the provided <see cref="TransmissionData"/>.</summary>
        /// <param name="transmissionData">The <see cref="TransmissionData"/> object to transmit to the <see cref="connectedClient"/> as a response.</param>
        protected virtual void RespondToClient(TransmissionData transmissionData)
        {
            // Do not modify this method.
            try
            {
                Array.Clear(sendBuffer, index: 0, length: sendBuffer.Length);
                sendBuffer = Serializer<TransmissionData>.Serialize(transmissionData);
                connectedClient.GetStream().Write(sendBuffer, offset: 0, size: sendBuffer.Length);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private (IPAddress, int) ConfigManager()
        {
            IPAddress iPAddressToReturn = null;
            int portToReturn = 0;
            string[] ipaddress = ConfigurationManager.AppSettings.GetValues(0);
            string[] port = ConfigurationManager.AppSettings.GetValues(1);
            foreach (string result in ipaddress)
            {
                if (IPAddress.TryParse(result, out iPAddressToReturn)) { }
                else if (int.TryParse(result, out portToReturn)) { }
            }
            foreach (string portResult in port)
            {
                if (int.TryParse(portResult, out portToReturn)) { }
            }
            if (iPAddressToReturn != null || portToReturn != 0)
            {
                return (iPAddressToReturn, portToReturn);
            }
            else
            {
                throw new Exception("you need a valid config file");
            }
        }
        #endregion
    }
}
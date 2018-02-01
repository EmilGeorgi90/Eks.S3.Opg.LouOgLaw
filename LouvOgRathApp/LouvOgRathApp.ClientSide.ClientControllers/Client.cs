using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using LouvOgRathApp.Shared.TcpCommunications;
using System.Text;
using System.Threading.Tasks;
using LouvOgRathApp.Shared.Entities;

namespace LouvOgRathApp.ClientSide.ClientControllers
{
    public class Client : ServerCommunicationsHandler
    {
        #region Constructors
        /// <summary>
        /// create this for making a connection to the server
        /// </summary>
        /// <param name="remoteEndPoint"></param>
        public Client(IPEndPoint remoteEndPoint) : base(remoteEndPoint)
        {
        }
        #endregion



        #region methods
        /// <summary>
        /// used for checking the connection
        /// </summary>
        /// <returns></returns>
        public bool ClientConnected()
        {
            return client.Connected;
        }
        /// <summary>
        /// used for getting all summerys
        /// </summary>
        /// <returns></returns>
        public IPersistable[] GetAllsummery()
        {
            return GetDataFromServer(RequestedAction.GetAllSummerysById);
        }
        /// <summary>
        /// used to get client summerys (only return 3 or less)
        /// </summary>
        /// <param name="case"></param>
        /// <returns></returns>
        public IPersistable[] GetClientSummerys(Case @case)
        {
            return SentRequestToServer(RequestedAction.GetSummeryToClient, @case);
        }
        /// <summary>
        /// Used to get all the current client cases
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public IPersistable[] GetClientsCases(UserCredentials client)
        {

            return SentRequestToServer(RequestedAction.GetCasesForTheClient, (IPersistable)client);
        }
        /// <summary>
        /// used to get all cases
        /// </summary>
        /// <returns></returns>
        public IPersistable[] GetAllCases()
        {
            return GetDataFromServer(RequestedAction.GetAllCases);
        }
        /// <summary>
        /// used to create a new user
        /// </summary>
        /// <param name="userCredentials"></param>
        public void CreateNewUser(List<UserCredentials> userCredentials)
        {
            SentRequestToServer(RequestedAction.CreateUser, userCredentials.ToList<IPersistable>());
        }
        /// <summary>
        /// making a login attempt
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns></returns>
        public (bool, UserCredentials) LoginAttempt(UserCredentials userCredentials)
        {
            List<UserCredentials> userInfos = new List<UserCredentials>();
            userInfos.Add(userCredentials);
            IPersistable[] pers = userInfos.ToArray();
            IPersistable[] persistable = SentRequestToServer(RequestedAction.GetUserCredentialsAttempt, pers.ToList());
            foreach (UserCredentials uInfo in persistable)
            {
                return (true, uInfo);
            }
            return (false, null);
        }
        /// <summary>
        /// used to get data from the server
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public IPersistable[] GetDataFromServer(RequestedAction action)
        {
            byte[] buffer = Serializer<ClientRequest>.Serialize(new ClientRequest(action));
            byte[] bufferReceiver = Transmit(buffer);
            return Serializer<TransmissionData>.Deserialize(bufferReceiver).Entities;
        }
        /// <summary>
        /// saves a new case
        /// </summary>
        /// <param name="case"></param>
        /// <returns></returns>
        public IPersistable[] SaveCase(Case[] @case)
        {
            IPersistable[] persistable = @case;
            return SentRequestToServer(RequestedAction.SaveNewCase, persistable.ToList());
        }
        /// <summary>
        /// get all summery to the case
        /// </summary>
        /// <param name="case"></param>
        /// <returns></returns>
        public IPersistable[] GetAllSummerys(Case @case)
        {
            return SentRequestToServer(RequestedAction.GetSummerysById, @case);
        }
        /// <summary>
        /// gets all persons
        /// </summary>
        /// <returns></returns>
        public IPersistable[] GetAllPersons()
        {
            return GetDataFromServer(RequestedAction.GetPersons);
        }
        /// <summary>
        /// save new summery
        /// </summary>
        /// <param name="summery"></param>
        /// <returns></returns>
        public IPersistable[] SaveSummery(MettingSummery[] summery)
        {
            IPersistable[] persistable = summery;
            return SentRequestToServer(RequestedAction.SaveNewSummery, persistable.ToList());
        }
        /// <summary>
        /// sending a request to the server with an action and some data
        /// </summary>
        /// <param name="action"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public IPersistable[] SentRequestToServer(RequestedAction action, IPersistable data)
        {
            TransmissionData transmitData = new TransmissionData(data);
            byte[] buffer = Serializer<ClientRequest>.Serialize(new ClientRequest(action, transmitData));
            byte[] bufferReceiver = Transmit(buffer);
            return Serializer<TransmissionData>.Deserialize(bufferReceiver).Entities;
        }
        /// <summary>
        /// sending a request to the server with an action and a list of data
        /// </summary>
        /// <param name="action"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public IPersistable[] SentRequestToServer(RequestedAction action, List<IPersistable> data)
        {
            if (!client.Connected)
            {
                client.Connect(remoteEndPoint);
            }
            TransmissionData transmitData = new TransmissionData(data);
            byte[] buffer = Serializer<ClientRequest>.Serialize(new ClientRequest(action, transmitData));
            byte[] bufferReceiver = Transmit(buffer);
            return Serializer<TransmissionData>.Deserialize(bufferReceiver).Entities;
        }
        /// <summary>
        /// response from client
        /// </summary>
        /// <returns></returns>
        public TransmissionData ResponseDataFromServer()
        {
            byte[] buffer = new byte[bufferSize];
            Transmit(buffer);
            return Serializer<TransmissionData>.Deserialize(buffer);
        }
        #endregion
    }
}

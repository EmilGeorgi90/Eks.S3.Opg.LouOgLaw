/**************************************************************************************************
*  Author: Mads Mikkel Rasmussen (mara@aspit.dk), github: https://github.com/Mara-AspIT/          *
*  Solution: .NET version: 4.7.1, C# version: 7.1                                                 *
*  Visual Studio version: Visual Studio Enterprise 2017, version 15.4.5                           *
*  Repository: https://github.com/Mara-AspIT/Mara.Modules.git                                     *
**************************************************************************************************/

using System;

namespace LouvOgRathApp.Shared.TcpCommunications
{
    /// <summary>Actions the server can take upon a client request.</summary>
    [Serializable]
    public enum RequestedAction
    {
        GetAllCases = 1,
        GetAllSummerysById = 2,
        GetAllCaseKinds = 3,
        GetAllSecretaries = 4,
        SaveNewSummery = 5,
        SaveNewCase = 6,
        GetCasesForTheClient = 7,
        GetUserCredentialsAttempt = 8,
        CreateUser = 9,
        GetPersons,
        GetSummerysById,
        GetSummeryToClient
    }
}

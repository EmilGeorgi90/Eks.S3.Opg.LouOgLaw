/**************************************************************************************************
*  Author: Mads Mikkel Rasmussen (mara@aspit.dk), github: https://github.com/Mara-AspIT/          *
*  Solution: .NET version: 4.7.1, C# version: 7.1                                                 *
*  Visual Studio version: Visual Studio Enterprise 2017, version 15.4.5                           *
*  Repository:                                     *
**************************************************************************************************/

namespace LouvOgRathApp.Shared.TcpCommunications
{
    /// <summary>Provides constants related to TCP communication.</summary>
    public static class CommunicationConstants
    {
        /// <summary>Upper limit on transmission size.</summary>
        public const int BufferLimit = 1024 * 1024; // 1 MB.
    }
}
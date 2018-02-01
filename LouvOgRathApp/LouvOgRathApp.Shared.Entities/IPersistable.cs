/**************************************************************************************************
*  Author: Mads Mikkel Rasmussen (mara@aspit.dk), github: https://github.com/Mara-AspIT/          *
*  Solution: .NET version: 4.7.1, C# version: 7.1                                                 *
*  Visual Studio version: Visual Studio Enterprise 2017, version 15.4.5                           *
*  Repository:                                     *
**************************************************************************************************/

namespace LouvOgRathApp.Shared.Entities
{

    /// <summary>Provides a single property to implement for all business logic related types that are to be persisted in a database.</summary>
    public interface IPersistable
    {
        /// <summary>Represents a readonly unique entity object identifier, corresponding to a particular row in a table in a database.</summary>
        int Id { get; }
    }
}
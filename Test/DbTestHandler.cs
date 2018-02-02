using System;
using System.Data;
using LouvOgRathApp.ServerSide.DataAccess;
using LouvOgRathApp.Shared.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class DbTestHandler : RepositoryBase
    {
        public DbTestHandler() : base("constring")
        {
        }

        [TestMethod]
        public void IfUserNotFoundReturnFalseAndNull()
        {
            //arrange
            UserCredentialsRepository access = new UserCredentialsRepository("constring");
            UserCredentials user = new UserCredentials("emil", "something", null);
            //act
            (bool result, RoleKind? role) = access.UserCredentialsLoginAttemp(user);
            //assert
            Assert.IsNull(role);
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void CraeteUserReturn1()
        {
            UserCredentialsRepository access = new UserCredentialsRepository("constring");
            RoleKind? role = RoleKind.Secretary;
            UserCredentials user = new UserCredentials(role, "cv271198", "something", new Person("emil", "27365900", "emil@gmail.com", RoleKind.Client));
            DataSet ds = Executor.Execute("select * from userCredentials");
            int resultMinus1 = ds.Tables[0].Rows.Count;
            access.CreateNewUser(user);
            ds = Executor.Execute("select * from userCredentials");
            int result = ds.Tables[0].Rows.Count;
            Assert.AreEqual(resultMinus1 + 1, result);
        }
    }
}

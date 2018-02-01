using System;
using System.Data;
using LouvOgRathApp.ServerSide.DataAccess;
using LouvOgRathApp.Shared.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class DbTestHandler
    {
        [TestMethod]
        public void IfUserNotFoundReturnFalseAndNull()
        {
            //arrange
            UserCredentialsDataAccess access = new UserCredentialsDataAccess();
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
            UserCredentialsDataAccess access = new UserCredentialsDataAccess();
            RoleKind? role = RoleKind.Secretary;
            UserCredentials user = new UserCredentials(role, "cv271198", "something", new Person("emil", "27365900", "emil@gmail.com", RoleKind.Client));
            DataSet ds = access.Executor.Execute("select * from userCredentials");
            int resultMinus1 = ds.Tables[0].Rows.Count;
            access.CreateNewUser(user);
            ds = access.Executor.Execute("select * from userCredentials");
            int result = ds.Tables[0].Rows.Count;
            Assert.AreEqual(resultMinus1 + 1, result);
        }
    }
}

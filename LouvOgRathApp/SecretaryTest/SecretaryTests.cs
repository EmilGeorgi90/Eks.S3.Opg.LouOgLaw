using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LouvOgRathApp.ServerSide.DataAccess;
using System.Collections.Generic;
using LouvOgRathApp.Shared.Entities;

namespace SecretaryTest
{
    [TestClass]
    public class SecretaryTests
    {
        [TestMethod]
        public void SecretaryAddCaseToDB()
        {
            //assert
            Case tempCase = new Case("test", CaseKind.kriminal, new Lawyer("something"), new Client("something", 1), new MettingSummery("something", new Secretary("something")));
            CasesDataAccess tempCaseDataAccess = new CasesDataAccess();
            //act
            List<Case> cases = tempCaseDataAccess.GetCases();
            tempCaseDataAccess.AddCase(tempCase);
            List<Case> result = tempCaseDataAccess.GetCases();
            //arange
            Assert.AreNotEqual(result.Count, cases.Count);
            
        }
        [TestMethod]
        public void SecretaryAddAResumé()
        {
            MettingSummery mettingSummery = new MettingSummery("Test", new Secretary("something", 1));
            SummeryDataAccess summeryDataAccess = new SummeryDataAccess();

            List<MettingSummery> resumés = summeryDataAccess.GetAllSummerysById();
            summeryDataAccess.SafeSummery(mettingSummery);
            List<MettingSummery> result = summeryDataAccess.GetAllSummerysById();

            Assert.AreNotEqual(result.Count, resumés.Count);

        }
    }
}

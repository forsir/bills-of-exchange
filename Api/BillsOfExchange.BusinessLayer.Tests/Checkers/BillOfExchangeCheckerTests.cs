using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using BillsOfExchange.BusinessLayer.Checkers;
using BillsOfExchange.DataProvider.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BillsOfExchange.BusinessLayer.Checkers.Tests
{
    [TestClass()]
    public class BillOfExchangeCheckerTests
    {
        [TestMethod()]
        public void BillOfExchangeCheckTest_Correct()
        {
            // arrange
            BillOfExchangeCheckFixture billOfExchangeCheckFixture = new BillOfExchangeCheckFixture().CreateBillOfExchange(1, 2);
            var billOfExchangeChecker = new BillOfExchangeChecker();

            // act
            BillOfExchangeCheckResult result = billOfExchangeChecker.BillOfExchangeCheck(billOfExchangeCheckFixture.BillOfExchange);

            // assert
            Assert.IsTrue(result.IsCorrect);
        }

        [TestMethod()]
        public void BillOfExchangeCheckTest_SameParties()
        {
            // arrange
            int sameId = 1;
            BillOfExchangeCheckFixture billOfExchangeCheckFixture = new BillOfExchangeCheckFixture().CreateBillOfExchange(sameId, sameId);
            var billOfExchangeChecker = new BillOfExchangeChecker();

            // act
            BillOfExchangeCheckResult result = billOfExchangeChecker.BillOfExchangeCheck(billOfExchangeCheckFixture.BillOfExchange);

            // assert
            Assert.IsFalse(result.IsCorrect);
        }

        private class BillOfExchangeCheckFixture
        {
            public BillOfExchange BillOfExchange { get; set; }

            public BillOfExchangeCheckFixture CreateBillOfExchange(int drawerId, int beneficiaryId)
            {
                BillOfExchange = new BillOfExchange();
                BillOfExchange.BeneficiaryId = beneficiaryId;
                BillOfExchange.DrawerId = drawerId;
                return this;
            }
        }
    }
}
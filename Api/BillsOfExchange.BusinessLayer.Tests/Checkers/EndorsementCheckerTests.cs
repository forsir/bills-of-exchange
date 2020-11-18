using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BillsOfExchange.BusinessLayer.Checkers;
using BillsOfExchange.DataProvider.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BillsOfExchange.BusinessLayer.Checkers.Tests
{
    [TestClass()]
    public class EndorsementCheckerTests
    {
        [TestMethod()]
        public void CheckListTest_EmptyList()
        {
            // arrange
            var checkListFixture = new CheckListFixture();
            var endorsementChecker = new EndorsementChecker();

            // act
            EndorsementCheckResult result = endorsementChecker.CheckList(checkListFixture.BillOfExchange, checkListFixture.EndorsementList);

            // assert
            Assert.IsTrue(result.IsCorrect);
        }

        [TestMethod()]
        public void CheckListTest_TwoFirst()
        {
            // arrange
            CheckListFixture checkListFixture = new CheckListFixture().AddFirstEndorsement(1, false).AddFirstEndorsement(2, false);
            var endorsementChecker = new EndorsementChecker();

            // act
            EndorsementCheckResult result = endorsementChecker.CheckList(checkListFixture.BillOfExchange, checkListFixture.EndorsementList);

            // assert
            Assert.IsFalse(result.IsCorrect);
            CollectionAssert.AreEquivalent(checkListFixture.WrongEndorsementList, result.WrongEndorsements);
        }

        [TestMethod()]
        public void CheckListTest_CorrectList()
        {
            // arrange
            CheckListFixture checkListFixture = new CheckListFixture()
                .AddFirstEndorsement(1)
                .AppendEndorsement(2)
                .AppendEndorsement(3);
            var endorsementChecker = new EndorsementChecker();

            // act
            EndorsementCheckResult result = endorsementChecker.CheckList(checkListFixture.BillOfExchange, checkListFixture.EndorsementList);

            // assert
            Assert.IsTrue(result.IsCorrect);
        }

        [TestMethod()]
        public void CheckListTest_FirstHaveSamelBeneficiaryId()
        {
            // arrange
            CheckListFixture checkListFixture = new CheckListFixture()
                .AddFirstEndorsement(CheckListFixture.BillBeneficiaryId, false)
                .AppendEndorsement(2)
                .AppendEndorsement(3);
            var endorsementChecker = new EndorsementChecker();

            // act
            EndorsementCheckResult result = endorsementChecker.CheckList(checkListFixture.BillOfExchange, checkListFixture.EndorsementList);

            // assert
            Assert.IsFalse(result.IsCorrect);
            CollectionAssert.AreEquivalent(checkListFixture.WrongEndorsementList, result.WrongEndorsements);
        }

        [TestMethod()]
        public void CheckListTest_SecondHaveSamelBeneficiaryId()
        {
            // arrange
            CheckListFixture checkListFixture = new CheckListFixture()
                .AddFirstEndorsement(1)
                .AppendEndorsement(CheckListFixture.BillBeneficiaryId)
                .AppendEndorsement(3);
            var endorsementChecker = new EndorsementChecker();

            // act
            EndorsementCheckResult result = endorsementChecker.CheckList(checkListFixture.BillOfExchange, checkListFixture.EndorsementList);

            // assert
            Assert.IsTrue(result.IsCorrect);
        }

        [TestMethod()]
        public void CheckListTest_SameBeneficiaryId()
        {
            // arrange
            int beneficiaryId = 100;
            CheckListFixture checkListFixture = new CheckListFixture()
                .AddFirstEndorsement(1)
                .AppendEndorsement(2)
                .AppendEndorsement(beneficiaryId, false)
                .AppendEndorsement(beneficiaryId, false)
                .AppendEndorsement(5);
            var endorsementChecker = new EndorsementChecker();

            // act
            EndorsementCheckResult result = endorsementChecker.CheckList(checkListFixture.BillOfExchange, checkListFixture.EndorsementList);

            // assert
            Assert.IsFalse(result.IsCorrect);
            CollectionAssert.AreEquivalent(checkListFixture.WrongEndorsementList, result.WrongEndorsements);
        }

        [TestMethod()]
        public void CheckListTest_NotSameBeneficiaryId()
        {
            // arrange
            int beneficiaryId = 100;
            CheckListFixture checkListFixture = new CheckListFixture()
                .AddFirstEndorsement(1)
                .AppendEndorsement(beneficiaryId)
                .AppendEndorsement(3)
                .AppendEndorsement(beneficiaryId)
                .AppendEndorsement(5);
            var endorsementChecker = new EndorsementChecker();

            // act
            EndorsementCheckResult result = endorsementChecker.CheckList(checkListFixture.BillOfExchange, checkListFixture.EndorsementList);

            // assert
            Assert.IsTrue(result.IsCorrect);
        }

        [TestMethod()]
        public void CheckListTest_CyclicficiaryId()
        {
            // arrange
            CheckListFixture checkListFixture = new CheckListFixture()
                .AddFirstEndorsement(1)
                .AppendEndorsement(2)
                .AppendEndorsement(3)
                .AppendEndorsement(4);
            checkListFixture = checkListFixture.AppendEndorsement(5, checkListFixture.EndorsementList.First().Id, false);
            var endorsementChecker = new EndorsementChecker();

            // act
            EndorsementCheckResult result = endorsementChecker.CheckList(checkListFixture.BillOfExchange, checkListFixture.EndorsementList);

            // assert
            Assert.IsFalse(result.IsCorrect);
            CollectionAssert.AreEquivalent(checkListFixture.WrongEndorsementList, result.WrongEndorsements);
        }

        [TestMethod()]
        public void CheckListTest_SecondCyclicficiaryId()
        {
            // arrange
            CheckListFixture checkListFixture = new CheckListFixture()
                .AddFirstEndorsement(1)
                .AppendEndorsement(2)
                .AppendEndorsement(3)
                .AppendEndorsement(4);
            checkListFixture = checkListFixture.AppendEndorsement(5, checkListFixture.EndorsementList.Skip(1).First().Id, false);
            var endorsementChecker = new EndorsementChecker();

            // act
            EndorsementCheckResult result = endorsementChecker.CheckList(checkListFixture.BillOfExchange, checkListFixture.EndorsementList);

            // assert
            Assert.IsFalse(result.IsCorrect);
            CollectionAssert.AreEquivalent(checkListFixture.WrongEndorsementList, result.WrongEndorsements);
        }

        private class CheckListFixture
        {
            public const int BillBeneficiaryId = 1000;
            public const int BillDrawerId = 1001;

            public BillOfExchange BillOfExchange { get; set; }

            public List<Endorsement> EndorsementList { get; internal set; }

            public List<Endorsement> WrongEndorsementList { get; internal set; }

            private int index = 0;

            public CheckListFixture()
            {
                BillOfExchange = new BillOfExchange();
                BillOfExchange.Id = 1;
                BillOfExchange.BeneficiaryId = BillBeneficiaryId;
                BillOfExchange.DrawerId = BillDrawerId;
                EndorsementList = new List<Endorsement>();
                WrongEndorsementList = new List<Endorsement>();
            }

            private Endorsement CreateEndorsement(int newBeneficiaryId, int? previousEndorsementId, bool isCorrect)
            {
                var endorsement = new Endorsement();
                endorsement.Id = index++;
                endorsement.BillId = BillOfExchange.Id;
                endorsement.PreviousEndorsementId = previousEndorsementId;
                endorsement.NewBeneficiaryId = newBeneficiaryId;
                if (!isCorrect)
                {
                    WrongEndorsementList.Add(endorsement);
                }
                return endorsement;
            }

            public CheckListFixture AddFirstEndorsement(int newBeneficiaryId, bool isCorrect = true)
            {
                EndorsementList.Add(CreateEndorsement(newBeneficiaryId, null, isCorrect));
                return this;
            }

            public CheckListFixture AppendEndorsement(int newBeneficiaryId, bool isCorrect = true)
            {
                EndorsementList.Add(CreateEndorsement(newBeneficiaryId, EndorsementList.LastOrDefault()?.Id, isCorrect));
                return this;
            }

            public CheckListFixture AppendEndorsement(int newBeneficiaryId, int previousEndorsementId, bool isCorrect = true)
            {
                Endorsement endorsement = CreateEndorsement(newBeneficiaryId, EndorsementList.FirstOrDefault()?.Id, isCorrect);
                endorsement.PreviousEndorsementId = previousEndorsementId;
                EndorsementList.Add(endorsement);
                return this;
            }
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScoreoidPortable;
using ScoreoidPortable.Entities;

namespace ScoreoidTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // unique_id UniqueId
            var name = Utils.GetDescriptionFromName("UniqueId", typeof (Player));

            Assert.AreEqual("unique_id", name);
        }
    }
}

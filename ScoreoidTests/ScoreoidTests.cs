using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScoreoidPortable;
using ScoreoidPortable.Entities;

namespace ScoreoidTests
{
    [TestClass]
    public class ScoreoidTests
    {
        [TestMethod]
// ReSharper disable once InconsistentNaming
        public void Test_Get_Description_From_Name_Method()
        {
            // unique_id UniqueId
            var name = Utils.GetDescriptionFromName("UniqueId", typeof (Player));

            Assert.AreEqual("unique_id", name);
        }
    }
}

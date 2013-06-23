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

        [TestMethod]
        // ReSharper disable once InconsistentNaming
        public void Test_Get_Number_From_Json()
        {
            var json = "{\"bonus\":0.3}";

            var client = new ScoreoidClient();

            var value = client.GetNumberFromJson(json);

            Assert.AreEqual(0.3, value);
        }
    }
}

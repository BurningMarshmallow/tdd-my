using System.Drawing;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    [TestFixture]
    class Layouter_Tests
    {
        [Test]
        public void AddCentralWord()
        {
            var layouter = new CircularCloudLayouter(new Point(100, 100));
            var placeToPut = layouter.PutNextRectangle(new Size(4, 4));
            Assert.AreEqual(new Rectangle(100, 100, 4, 4), placeToPut);
        }
    }
}

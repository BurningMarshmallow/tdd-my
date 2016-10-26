using System.Drawing;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    [TestFixture]
    class Layouter_Tests
    {
        private readonly Point center = new Point(400, 400);
        private CircularCloudLayouter cloudLayouter;

        [SetUp]
        public void SetUp()
        {
            cloudLayouter = new CircularCloudLayouter(center);
        }

        [Test]
        public void IsFirstRectanglePlacedCorrectly()
        {
            var newRect = cloudLayouter.PutNextRectangle(new Size(200, 100));

            Assert.AreEqual(new Rectangle(300, 350, 200, 100), newRect);
            Assert.AreEqual(center, new Point((newRect.Left + newRect.Right) / 2, (newRect.Bottom + newRect.Top) / 2));
        }

        [TestCase(200)]
        [TestCase(1)]
        [TestCase(101)]
        [TestCase(2)]
        public void AreAllRectanglesSaved_AfterAdding(int numberOfRectangles)
        {
            var rectangleSize = new Size(20, 20);

            for (int i = 0; i < numberOfRectangles; i++)
            {
                cloudLayouter.PutNextRectangle(rectangleSize);
            }

            Assert.AreEqual(numberOfRectangles, cloudLayouter.GetRectangles().Length);
        }

        [TestCase(4800, 600)]
        [TestCase(1337, 22)]
        [TestCase(111, 444)]
        [TestCase(5777, 2232)]
        public void TwoConsequentlyAddedRectangles_DoNotIntersect(int width, int height)
        {
            var rectangleSize = new Size(width, height);

            var first = cloudLayouter.PutNextRectangle(rectangleSize);
            var second= cloudLayouter.PutNextRectangle(rectangleSize);

            Assert.False(first.IntersectsWith(second));
        }

        [TestCase(240)]
        [TestCase(33)]
        [TestCase(1)]
        [TestCase(2)]
        public void ManyAddedRectangles_DoNotIntersect(int numberOfRectangles)
        {
            var rectangleSize = new Size(10, 10);

            for (int i = 0; i < numberOfRectangles; i++)
            {
                cloudLayouter.PutNextRectangle(rectangleSize);
            }
            var rectangles = cloudLayouter.GetRectangles();

            for (int i = 0; i < numberOfRectangles; i++)
            {
                for (int j = i + 1; j < numberOfRectangles; j++)
                {
                    Assert.False(rectangles[i].IntersectsWith(rectangles[j]));
                }
            }
        }
    }
}

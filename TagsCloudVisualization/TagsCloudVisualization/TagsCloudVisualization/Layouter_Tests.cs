using System;
using System.Drawing;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudVisualization
{
    [TestFixture]
    class LayouterTests
    {
        private readonly Point _center = new Point(400, 400);
        private CircularCloudLayouter _cloudLayouter;

        [SetUp]
        public void SetUp()
        {
            _cloudLayouter = new CircularCloudLayouter(_center);
        }

        [Test]
        public void IsFirstRectanglePlacedCorrectly()
        {
            var newRect = _cloudLayouter.PutNextRectangle(new Size(200, 100));

            Assert.AreEqual(new Rectangle(300, 350, 200, 100), newRect);
            Assert.AreEqual(_center, new Point((newRect.Left + newRect.Right) / 2, (newRect.Bottom + newRect.Top) / 2));
        }

        [TestCase(200)]
        [TestCase(1)]
        [TestCase(101)]
        [TestCase(2)]
        public void AreAllRectanglesSaved_AfterAdding(int numberOfRectangles)
        {
            var rectangleSize = new Size(20, 20);

            for (var i = 0; i < numberOfRectangles; i++)
            {
                _cloudLayouter.PutNextRectangle(rectangleSize);
            }

            Assert.AreEqual(numberOfRectangles, _cloudLayouter.GetRectangles().Length);
        }

        [TestCase(800, 40)]
        [TestCase(1337, 22)]
        [TestCase(111, 444)]
        [TestCase(300, 300)]
        public void TwoConsequentlyAddedRectangles_DoNotIntersect(int width, int height)
        {
            var rectangleSize = new Size(width, height);

            var first = _cloudLayouter.PutNextRectangle(rectangleSize);
            var second= _cloudLayouter.PutNextRectangle(rectangleSize);

            Assert.False(first.IntersectsWith(second));
        }

        [TestCase(240)]
        [TestCase(33)]
        [TestCase(1)]
        [TestCase(2)]
        public void ManyAddedRectangles_DoNotIntersect(int numberOfRectangles)
        {
            var rectangleSize = new Size(10, 10);

            for (var i = 0; i < numberOfRectangles; i++)
            {
                _cloudLayouter.PutNextRectangle(rectangleSize);
            }
            var rectangles = _cloudLayouter.GetRectangles();

            for (var i = 0; i < numberOfRectangles; i++)
            {
                for (var j = i + 1; j < numberOfRectangles; j++)
                {
                    Assert.False(rectangles[i].IntersectsWith(rectangles[j]));
                }
            }
        }

        [TearDown]
        public void DrawOnFailure()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) return;

            var visualizator = new CloudVisualizer();
            var dir = TestContext.CurrentContext.TestDirectory;
            var testName = TestContext.CurrentContext.Test.Name;
            var path = dir + testName + ".bmp";
            visualizator.Visualise(_cloudLayouter.GetRectangles(), path);
            Console.WriteLine("Tag cloud visualization saved to file " + path);
        }
    }
}

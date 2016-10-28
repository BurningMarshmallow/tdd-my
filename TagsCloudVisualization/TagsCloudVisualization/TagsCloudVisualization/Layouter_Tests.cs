using System;
using System.Drawing;
using System.Linq;
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

        private static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        private static bool IsInsideCircle(Point point, Point circleCenter, int radius)
        {
            return GetDistance(point.X, point.Y, circleCenter.X, circleCenter.Y) < radius;
        }

        private static double GetOuterRectangles(Rectangle[] rectangles)
        {
            var rectanglesArea = rectangles.Sum(x => x.Width * x.Height);
            var circleArea = rectanglesArea * Math.PI / 2;
            var radius = (int)Math.Ceiling(Math.Sqrt(circleArea / Math.PI));
            var circleCenter = rectangles[0].GetCenterOfRectangle();
            double outerRectanglesCount = rectangles.Count(
                x => !IsInsideCircle(x.GetCenterOfRectangle(), circleCenter, radius));
            return outerRectanglesCount;
        }

        [TestCase(3, 228, 1337)]
        [TestCase(90, 1001, 20)]
        [TestCase(5, 5, 555)]
        [TestCase(33, 44, 55)]
        [TestCase(1, 1, 1)]
        [TestCase(789, 1, 3)]
        [TestCase(300, 100, 100)]

        public void CloudForm_IsSimilarToCircle(int numberOfRectangles, int width, int height)
        {
            var rectangleSize = new Size(width, height);

            for (var i = 0; i < numberOfRectangles; i++)
            {
                _cloudLayouter.PutNextRectangle(rectangleSize);
            }
            var rectangles = _cloudLayouter.GetRectangles();
            const double eps = 0.1;

            var outerRectanglesCount = GetOuterRectangles(rectangles);
            var outerRectanglesCoefficent = outerRectanglesCount / rectangles.Length;

            Assert.Less(outerRectanglesCoefficent, eps);
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

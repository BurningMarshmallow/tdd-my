using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    class Program
    {
        public static Rectangle[] GenerateNewLayout(CircularCloudLayouter layouter, int numberOfRectangles)
        {
            var rnd = new Random();
            for (var i = 0; i < numberOfRectangles; i++)
                layouter.PutNextRectangle(new Size(rnd.Next(30, 50), rnd.Next(20, 40)));
            return layouter.GetRectangles();
        }

        static void Main(string[] args)
        {
            var layouter = new CircularCloudLayouter(new Point(400, 400));
            var visualiser = new CloudVisualizer();
            var testData = GenerateNewLayout(layouter, 250);
            visualiser.Visualise(testData, "BigTest.bmp");
        }
    }
}
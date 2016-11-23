using System.Collections.Generic;
using System.Drawing;
using System.Linq;
// ReSharper disable InconsistentNaming

namespace TagsCloudVisualization
{
    public class LayouterWithEndlessSpiral
    {
        private readonly List<Rectangle> rectangles;
        public Point Center { get; }
        private readonly EndlessSpiral spiral;
        public LayouterWithEndlessSpiral(Point center)
        {
            rectangles = new List<Rectangle>();
            Center = center;
            spiral = new EndlessSpiral(center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle newRectangle;
            if (rectangles.Count == 0)
                newRectangle = GetRectangleFromCenterAndSize(Center, rectangleSize);
            else
            {
                var currentPoint = GetNextPoint(rectangleSize);

                newRectangle = GetRectangleFromCenterAndSize(currentPoint, rectangleSize);
            }
            rectangles.Add(newRectangle);
            return newRectangle;
        }

        private static Rectangle GetRectangleFromCenterAndSize(Point centerPoint, Size rectangleSize)
        {
            var upperLeftPoint = new Point(centerPoint.X - rectangleSize.Width / 2,
                                           centerPoint.Y - rectangleSize.Height / 2);
            return new Rectangle(upperLeftPoint, rectangleSize);
        }

        private Point GetNextPoint(Size rectangleSize)
        {
            foreach (var point in spiral)
            {
                if (!CantBePlaced(point, rectangleSize))
                    return point;
            }
            return Center;
        }

        private bool CantBePlaced(Point point, Size rectangleSize)
        {
            var rect = GetRectangleFromCenterAndSize(point, rectangleSize);
            return rectangles.Any(rect.IntersectsWith);
        }

        public Rectangle[] GetRectangles()
        {
            return rectangles.ToArray();
        }
    }
}

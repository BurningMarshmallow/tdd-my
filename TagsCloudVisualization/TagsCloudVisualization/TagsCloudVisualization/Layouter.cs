using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {

        private readonly List<Rectangle> _rectangles;
        public Point Center { get; }
        private readonly Spiral _spiral;
        public CircularCloudLayouter(Point center)
        {
            _rectangles = new List<Rectangle>();
            Center = center;
            _spiral = new Spiral(center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle newRectangle;
            if (_rectangles.Count == 0)
                newRectangle = GetRectangleFromCenterAndSize(Center, rectangleSize);
            else
            {
                var lastPoint = GetRectangletUpperLeftPoint(_rectangles[_rectangles.Count - 1]);
                var currentPoint = GetNextPoint(lastPoint, rectangleSize);

                newRectangle = GetRectangleFromCenterAndSize(currentPoint, rectangleSize);
            }
            _rectangles.Add(newRectangle);
            return newRectangle;
        }

        private Rectangle GetRectangleFromCenterAndSize(Point centerPoint, Size rectangleSize)
        {
            var upperLeftPoint = new Point(centerPoint.X - rectangleSize.Width / 2,
                                           centerPoint.Y - rectangleSize.Height / 2);
            return new Rectangle(upperLeftPoint, rectangleSize);
        }

        private static Point GetRectangletUpperLeftPoint(Rectangle rect)
        {
            return new Point(rect.Left, rect.Top);
        }

        private Point GetNextPoint(Point start, Size rectangleSize)
        {
            var point = start;
            while (CantBePlaced(point, rectangleSize))
                point = _spiral.GenerateNextPoint();
            return point;
        }

        private bool CantBePlaced(Point point, Size rectangleSize)
        {
            var rect = GetRectangleFromCenterAndSize(point, rectangleSize);
            return _rectangles.Any(rect.IntersectsWith);
        }

        public Rectangle[] GetRectangles()
        {
            return _rectangles.ToArray();
        }
    }
}

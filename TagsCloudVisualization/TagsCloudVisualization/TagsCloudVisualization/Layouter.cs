using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {

        private readonly List<Rectangle> Rectangles;
        public Point Center { get; }
        private readonly Spiral Spiral;
        public CircularCloudLayouter(Point center)
        {
            Rectangles = new List<Rectangle>();
            Center = center;
            Spiral = new Spiral(center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle newRectangle;
            if (Rectangles.Count == 0)
                newRectangle = GetRectangleFromCenterAndSize(Center, rectangleSize);
            else
            {
                var lastPoint = GetRectangletUpperLeftPoint(Rectangles[Rectangles.Count - 1]);
                var currentPoint = GetNextPoint(lastPoint, rectangleSize);

                newRectangle = GetRectangleFromCenterAndSize(currentPoint, rectangleSize);
            }
            Rectangles.Add(newRectangle);
            return newRectangle;
        }

        private Rectangle GetRectangleFromCenterAndSize(Point centerPoint, Size rectangleSize)
        {
            var upperLeftPoint = new Point(centerPoint.X - rectangleSize.Width / 2,
                                           centerPoint.Y - rectangleSize.Height / 2);
            return new Rectangle(upperLeftPoint, rectangleSize);
        }

        private Point GetRectangletUpperLeftPoint(Rectangle rect)
        {
            return new Point(rect.Left, rect.Top);
        }

        private Point GetNextPoint(Point start, Size rectangleSize)
        {
            var point = start;
            while (CantBePlaced(point, rectangleSize))
                point = Spiral.GenerateNextPoint();
            return point;
        }

        private bool CantBePlaced(Point point, Size rectangleSize)
        {
            var rect = GetRectangleFromCenterAndSize(point, rectangleSize);
            return Rectangles.Any(rect.IntersectsWith);
        }

        public Rectangle[] GetRectangles()
        {
            return Rectangles.ToArray();
        }
    }
}

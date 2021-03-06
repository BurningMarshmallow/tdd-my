﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
// ReSharper disable InconsistentNaming

namespace TagsCloudVisualization
{
    public class Layouter
    {
        private readonly List<Rectangle> rectangles;
        public Point Center { get; }
        private readonly ISpiral spiral;
        public Layouter(Point center, ISpiral spiral)
        {
            rectangles = new List<Rectangle>();
            Center = center;
            this.spiral = spiral;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle newRectangle;
            if (rectangles.Count == 0)
                newRectangle = GetRectangleFromCenterAndSize(Center, rectangleSize);
            else
            {
                var lastPoint = GetRectangletUpperLeftPoint(rectangles[rectangles.Count - 1]);
                var currentPoint = GetNextPoint(lastPoint, rectangleSize);

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

        private static Point GetRectangletUpperLeftPoint(Rectangle rect)
        {
            return new Point(rect.Left, rect.Top);
        }

        private Point GetNextPoint(Point start, Size rectangleSize)
        {
            var point = start;
            while (CantBePlaced(point, rectangleSize))
                point = spiral.GenerateNextPoint();
            return point;
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

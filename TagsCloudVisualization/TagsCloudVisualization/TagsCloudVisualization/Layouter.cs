using System.Drawing;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        private Point center;
        public CircularCloudLayouter(Point center)
        {
            this.center = center;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            return new Rectangle(center, rectangleSize);
        }
    }
}

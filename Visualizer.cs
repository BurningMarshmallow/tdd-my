using System.Drawing;
using System.Drawing.Imaging;


namespace TagsCloudVisualization
{
    class CloudVisualizer
    {
        private readonly int ImageWidth;
        private readonly int ImageHeight;
        private readonly Pen RectangleColor;
        private readonly Color BackgroundColor;

        public CloudVisualizer()
        {
            BackgroundColor = Color.SeaShell;
            RectangleColor = new Pen(Color.Tomato, 3);
            ImageWidth = 800;
            ImageHeight = 800;
        }

        public CloudVisualizer(Pen rectangleColor, Color backgroundColor, int imageWidth, int imageHeight)
        {
            RectangleColor = rectangleColor;
            BackgroundColor = backgroundColor;
            ImageWidth = imageWidth;
            ImageHeight = imageHeight;
        }

        public void Visualise(Rectangle[] rectangles, string filename)
        {
            var bitmap = new Bitmap(ImageWidth, ImageHeight);
            var graphics = Graphics.FromImage(bitmap);
            DrawRectangles(rectangles, graphics);
            bitmap.Save(filename, ImageFormat.Bmp);
        }

        private void DrawRectangles(Rectangle[] rectangles, Graphics graphics)
        {
            graphics.Clear(BackgroundColor);
            foreach (var rect in rectangles)
                graphics.DrawRectangle(RectangleColor, rect);
            graphics.Save();
        }
    }
}
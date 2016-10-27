using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;


namespace TagsCloudVisualization
{
    class CloudVisualizer
    {
        private readonly int _imageWidth;
        private readonly int _imageHeight;
        private readonly Pen _rectangleColor;
        private readonly Color _backgroundColor;

        public CloudVisualizer()
        {
            _backgroundColor = Color.SeaShell;
            _rectangleColor = new Pen(Color.Tomato, 3);
            _imageWidth = 800;
            _imageHeight = 800;
        }

        public CloudVisualizer(Pen rectangleColor, Color backgroundColor, int imageWidth, int imageHeight)
        {
            _rectangleColor = rectangleColor;
            _backgroundColor = backgroundColor;
            _imageWidth = imageWidth;
            _imageHeight = imageHeight;
        }

        public void Visualise(Rectangle[] rectangles, string filename)
        {
            var bitmap = new Bitmap(_imageWidth, _imageHeight);
            var graphics = Graphics.FromImage(bitmap);
            DrawRectangles(rectangles, graphics);
            bitmap.Save(filename, ImageFormat.Bmp);
        }

        private void DrawRectangles(IEnumerable<Rectangle> rectangles, Graphics graphics)
        {
            graphics.Clear(_backgroundColor);
            foreach (var rect in rectangles)
                graphics.DrawRectangle(_rectangleColor, rect);
            graphics.Save();
        }
    }
}
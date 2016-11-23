using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
// ReSharper disable InconsistentNaming

namespace TagsCloudVisualization
{
    public class EndlessSpiral : IEnumerable<Point>
    {
        public readonly Point spiralCenter;

        public EndlessSpiral(Point spiralCenter)
        {
            this.spiralCenter = spiralCenter;
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return new EndlessSpiralEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class EndlessSpiralEnumerator : IEnumerator<Point>
    {
        private readonly EndlessSpiral endlessSpiral;
        private int currentIteration;
        private const double StartRadius = 0.01;
        private const double StartAngle = 50;

        public EndlessSpiralEnumerator(EndlessSpiral endlessSpiral)
        {
            this.endlessSpiral = endlessSpiral;
            currentIteration = 1;
        }

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            var radius = StartRadius * currentIteration;
            var angle = StartAngle * currentIteration;
            var x = (int)(radius * Math.Cos(angle));
            var y = (int)(radius * Math.Sin(angle));
            currentIteration++;
            Current = new Point(endlessSpiral.spiralCenter.X + x, endlessSpiral.spiralCenter.Y + y);
            return true;
        }

        public void Reset()
        {

        }

        public Point Current { get; private set; }

        object IEnumerator.Current => Current;
    }
}

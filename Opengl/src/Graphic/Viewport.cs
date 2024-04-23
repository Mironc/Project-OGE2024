using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
namespace Graphic
{
    public sealed class Viewport
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float AspectRatio{
            get
            {
                return (float)Width / (float)Height;
            }
        }
        public Viewport(Rectangle rectangle)
        {
            this.Width = rectangle.Width;
            this.Height = rectangle.Height;
            this.X = rectangle.X;
            this.Y = rectangle.Y;
            UpdateViewport();
        }
        public void SetViewport(Rectangle rectangle)
        {
            this.Width = rectangle.Width;
            this.Height = rectangle.Height;
            this.X = rectangle.X;
            this.Y = rectangle.Y;
            UpdateViewport();
        }
        private void UpdateViewport()
        {
            GL.Viewport(this.X, this.Y, this.Width, this.Height);
        }
    }
}

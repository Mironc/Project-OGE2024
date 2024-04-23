using System.Drawing;
using OpenTK.Graphics.OpenGL;
namespace Graphic.Rendering
{
    public static class Rendering
    {
        public static DepthTest DepthTest = new DepthTest();
        public static ClearOptions ClearOptions = new ClearOptions();
        public static Color BackgroundColor = Color.Black;
        public static void Clear()
        {
            GL.Clear((ClearBufferMask)ClearOptions.Flags);
        }
        public static void ClearColor()
        {
            GL.ClearColor(BackgroundColor);
        }
    }
}

using OpenTK.Graphics.OpenGL;
namespace Graphic.Rendering
{
    public class DepthTest
    {
        public void Enable()
        {
            GL.Enable(EnableCap.DepthTest);
        }
        public void Disable()
        {
            GL.Disable(EnableCap.DepthTest);
        }
        public void SetDepthFunction(DepthFunction depthFunction)
        {
            GL.DepthFunc((OpenTK.Graphics.OpenGL.DepthFunction)depthFunction);
        }
    }
    public enum DepthFunction
    {
        Less=513,
        LessEqual=515,
        Greater=516,
        GreaterEqual = 518,
        Always = 519,
        Never=512,
    }
}

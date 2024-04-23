using System;
using OpenTK.Graphics.OpenGL;
namespace Graphic
{
    public class FrameBuffer
    {
        public readonly int ID;
        public FrameBuffer()
        {
            this.ID = GL.GenFramebuffer();
        }
        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer,this.ID);
        }
        public static void BindDefault()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer,0);
        }
    }
}

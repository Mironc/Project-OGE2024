using System;
using OpenTK.Graphics.OpenGL;
namespace Graphic
{
    public sealed class VAO 
    {
        public readonly int ID;

        public VAO()
        {
            this.ID = GL.GenVertexArray();
        }
        public void Bind()
        {
            GL.BindVertexArray(this.ID);
        }
        public void Unbind()
        {
            GL.BindVertexArray(0);
        }
    }
}

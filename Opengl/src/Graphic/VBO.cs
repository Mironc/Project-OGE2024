using System;
using Tools;
using OpenTK.Graphics.OpenGL;
namespace Graphic
{
    public sealed class VBO 
    {
        public readonly int ID;
        public VBO()
        {
            this.ID = GL.GenBuffer();
        }
        public void BindData<T>(ref T[] vertexes) where T: struct
        {
            this.Bind();
            GL.BufferData(BufferTarget.ArrayBuffer, vertexes.Length * TypeSize<T>.Size, vertexes, BufferUsageHint.StaticDraw);
        }
        public void Bind()
        {
                GL.BindBuffer(BufferTarget.ArrayBuffer, this.ID);
        }
    }
}

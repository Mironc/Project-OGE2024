using System;
using OpenTK.Graphics.OpenGL;
namespace Tools
{
    public sealed class VertexBuilder<T> 
    {
        readonly int Stride = TypeSize<T>.Size;
        int Index = 0;
        int Offset = 0;
        public void Float(int Count,bool normalized)
        {
            GL.VertexAttribPointer(Index,Count,VertexAttribPointerType.Float,normalized,Stride,Offset);
            GL.EnableVertexAttribArray(Index);
            Index++;
            Offset += Count * sizeof(float);
        }
    }
}

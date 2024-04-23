using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using Graphic;
using Tools;

public struct Vertex : IVertex
{
    private Vector3 Position;
    public void Description()
    {
        int Stride = TypeSize<Vertex>.Size;
        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Stride,0);
    }
    public Vertex(Vector3 Position)
    {
        this.Position = Position;
    }
}

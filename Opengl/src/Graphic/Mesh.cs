using System;
using OpenTK.Graphics.OpenGL;
namespace Graphic
{
    /// <summary>
    /// T must implement IVertex
    /// </summary>
    public class Mesh<T> where T: struct
    {
        private EBO EBO;
        private VBO VBO;
        private VAO VAO;
        private readonly int VertexCount;
        public Mesh(T[] vertexes,uint[] Indicies) 
        {
            this.VAO = new VAO();
            VAO.Bind();
            this.VBO = new VBO();
            this.EBO = new EBO();
            VBO.Bind();
            EBO.Bind();
            this.VBO.BindData(ref vertexes);
            this.VertexCount = this.EBO.BindData(Indicies);
            if (vertexes[0] is IVertex)
            {
                (vertexes[0] as IVertex).Description();
            }
            else
            {
                throw new Exception("T does not implement IVertex");
            }
        }
        public void Draw()
        {
            VAO.Bind();
            GL.DrawElements(PrimitiveType.Triangles,VertexCount,DrawElementsType.UnsignedInt,0);
        }
    }
}

using OpenTK.Graphics.OpenGL;
namespace Graphic
{
    public sealed class EBO 
    {
        public readonly int ID;
        public EBO() {
            this.ID = GL.GenBuffer();
          }
        public void Bind()
        {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.ID);
        }
        public int BindData(uint[] Indicies)
        {
            this.Bind();
            GL.BufferData(BufferTarget.ElementArrayBuffer, Indicies.Length * sizeof(uint),Indicies,BufferUsageHint.StaticDraw);
            return Indicies.Length;
        }
    }
}

using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using OpenTK.Graphics.OpenGL;
using System;
namespace Graphic
{
    public class Texture2D
    {
        public readonly int ID;
        public Texture2D(string image_source)
        {
            this.ID = Gen();
            Bitmap bitmap = (Bitmap)Image.FromStream(File.Open(image_source,FileMode.Open));
            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
               ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);
        }
        public void SetActive(int Unit)
        {
            GL.ActiveTexture((TextureUnit)(33984 + Unit));
            Bind();
        }
        public void AttachToFramebuffer(FrameBuffer frameBuffer,int attachment)
        {
            frameBuffer.Bind();
            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0+attachment, this.ID, 0);
            FrameBuffer.BindDefault();
        }
        private int Gen()
        {
            int ID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, ID);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            return ID;
        }
        private void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D,this.ID);
        }
        public Texture2D(int width, int height)
        {
            this.ID = Gen();
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.Float, IntPtr.Zero);
        }
    }
}

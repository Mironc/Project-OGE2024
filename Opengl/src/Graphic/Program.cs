using System;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Collections.Generic;
namespace Graphic
{
    public sealed class Program
    {
        private readonly Dictionary<string,int> Uniforms = new Dictionary<string,int>();
        public readonly int ID;

        public Program(Shader[] Shaders)
        {
            this.ID = GL.CreateProgram();
            foreach (Shader shader in Shaders)
            {
                GL.AttachShader(this.ID, shader.ID);
            }
            GL.LinkProgram(this.ID);
            GL.GetProgram(this.ID, GetProgramParameterName.LinkStatus, out int success);
            if (success == (int)All.False)
            {
                throw new Exception($"Linking error :{GL.GetProgramInfoLog(this.ID)}");
            }
        }
        public void Bind()
        {
            GL.UseProgram(this.ID);
        }
        public void SetVector3(Vector3 vector3,string UniformName)
        {
            GL.Uniform3(this.GetUniformLocation(UniformName),ref vector3);
        }
        public void SetMatrix(Matrix4 matrix,string UniformName)
        {
            GL.UniformMatrix4(this.GetUniformLocation(UniformName),true,ref matrix);
        }
        public void SetInt(int Int,string UniformName)
        {
            GL.Uniform1(this.GetUniformLocation(UniformName), Int);
        }
        public void SetFloat(float Float,string UniformName)
        {
            GL.Uniform1(this.GetUniformLocation(UniformName),Float);
        }
        public void SetBool(bool Bool,string UniformName)
        {
            GL.Uniform1(this.GetUniformLocation(UniformName),Convert.ToInt32(Bool));
        }
        private int GetUniformLocation(string UniformName)
        {
            int loc;
            if (this.Uniforms.TryGetValue(UniformName,out loc))
            {
                return loc;
            }
            else
            {
                loc = GL.GetUniformLocation(this.ID, UniformName);
                if (loc != -1)
                {
                    this.Uniforms.Add(UniformName, loc);
                }
                return loc;
            }
        }
    }
}

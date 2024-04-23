using System;
using OpenTK.Graphics.OpenGL;
namespace Graphic
{
    public sealed class Shader 
    {
        public readonly int ID;
        public Shader(string source,ShaderType shaderType)
        {
            this.ID = GL.CreateShader((OpenTK.Graphics.OpenGL.ShaderType)shaderType);
            GL.ShaderSource(this.ID, source);
            GL.CompileShader(this.ID);
            int success;
            GL.GetShader(this.ID, ShaderParameter.CompileStatus, out success);
            if (success == (int)All.False)
            {
                throw new Exception($"{GL.GetShaderInfoLog(this.ID)}");
            }
        }
    }
    public enum ShaderType {
        Vertex = OpenTK.Graphics.OpenGL.ShaderType.VertexShader,
        Fragment = OpenTK.Graphics.OpenGL.ShaderType.FragmentShader,
        Compute = OpenTK.Graphics.OpenGL.ShaderType.ComputeShader,
        Geometry = OpenTK.Graphics.OpenGL.ShaderType.GeometryShader,
        TessalationControl = OpenTK.Graphics.OpenGL.ShaderType.TessControlShader,
        TesselationEvaluation = OpenTK.Graphics.OpenGL.ShaderType.TessEvaluationShader
    }
}

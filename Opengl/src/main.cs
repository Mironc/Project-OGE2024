using System;
using OpenTK;
using OpenTK.Input;
using Graphic;
using Graphic.Rendering;
using System.IO;
using System.Globalization;

class main
{
    private static Vector3 LightPosition =  new Vector3(0.0f,-10.0f,0.0f);
    private static Vector3 LightColor = new Vector3(1.0f,1.0f,1.0f);
    private static float SpecularStrength = 0.2f;
    private static float AmbientStrength = 0.3f;
    private static Vector3 ObjectColor = new Vector3(1.0f,1.0f,1.0f);
    private static double TimeFromStart;

    [STAThread]
    public static void Main(string[] args)
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
        using (var window = new GameWindow(800,800,OpenTK.Graphics.GraphicsMode.Default,"Game",GameWindowFlags.Default,DisplayDevice.Default,3,3,OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible))
        {
            window.MakeCurrent();
            
            var texture = new Texture2D("texture.png");
            var Viewport = new Viewport(window.ClientRectangle);
            var VertexShader = new Shader(new StreamReader("vert.glsl").ReadToEnd(), ShaderType.Vertex);
            var FragmentShader = new Shader(new StreamReader("frag.glsl").ReadToEnd(), ShaderType.Fragment);
            var Program = new Program(new Shader[] { VertexShader, FragmentShader });
            var ModelTransform = new Transform(new Vector3(0.0f, 0.0f, 10.0f), new Vector3(0.0f, 180.0f, 0.0f));
            var CameraTransform = new Transform(new Vector3(0.0f, 0.0f, 0.0f));
            var Camera = new Camera(45.0f, 0.01f, 1024.0f, Viewport);
            var Mesh = MeshLoader.LoadMesh(new StreamReader("MonkeyHead.obj").ReadToEnd());
            Rendering.DepthTest.Enable();
            Rendering.ClearOptions.AddFlag(ClearFlag.Color | ClearFlag.Depth);
            window.Resize+= (sender, e) => Viewport.SetViewport(window.ClientRectangle);
            window.UpdateFrame += (sender, e) =>
            {
                if (!window.Focused) return;
                var KeyboardInput = Keyboard.GetState();
                if (KeyboardInput.IsKeyDown(Key.W))
                {
                    CameraTransform.Translate(CameraTransform.Forward * (float)e.Time * 10.0f);
                }
                if (KeyboardInput.IsKeyDown(Key.S))
                {
                    CameraTransform.Translate(-CameraTransform.Forward * (float)e.Time * 10.0f);
                }
                if (KeyboardInput.IsKeyDown(Key.A))
                {
                    CameraTransform.Translate(CameraTransform.Right * (float)e.Time * 10.0f);
                }
                if (KeyboardInput.IsKeyDown(Key.D))
                {
                    CameraTransform.Translate(-CameraTransform.Right * (float)e.Time * 10.0f);
                }
                if (KeyboardInput.IsKeyDown(Key.Up))
                {
                    CameraTransform.Rotate(-Vector3.UnitX * (float)e.Time * 100.0f);
                }
                if (KeyboardInput.IsKeyDown(Key.Down))
                {
                    CameraTransform.Rotate(Vector3.UnitX * (float)e.Time * 100.0f);
                }
                if (KeyboardInput.IsKeyDown(Key.Left))
                {
                    CameraTransform.Rotate(Vector3.UnitY * (float)e.Time * 100.0f);
                }
                if (KeyboardInput.IsKeyDown(Key.Right))
                {
                    CameraTransform.Rotate(-Vector3.UnitY* (float)e.Time * 100.0f);
                }
            };
            window.RenderFrame+= (sender, e) => {
                if (!window.Focused) return;
                TimeFromStart += e.Time;
                Rendering.Clear();
                Rendering.ClearColor();
                Camera.CalculateView(CameraTransform);
                Camera.CalculateProjection();
                LightPosition.X =  (float)Math.Sin(TimeFromStart) *  10.0f;
                texture.SetActive(1);
                Program.Bind();
                Program.SetMatrix(Camera.Projection, "projection");
                Program.SetMatrix(Camera.View,"view");
                Program.SetMatrix(ModelTransform.GetMatrix(), "transformation");
                Program.SetVector3(LightColor, "LightColor");
                Program.SetVector3(LightPosition, "LightPosition");
                Program.SetFloat(SpecularStrength, "Specular");
                Program.SetFloat(AmbientStrength, "Ambient");
                Program.SetInt(1,"ObjectColor");
                Program.SetVector3(Camera.View.ExtractTranslation(), "CameraPosition");
                Mesh.Draw();
                window.Context.SwapBuffers();
            };
            window.Run();
        }
    }
    static readonly Vertex[] CubeVertexes = {
                     new Vertex(new Vector3(-1.0f, 1.0f, 1.0f)),
                     new Vertex(new Vector3(-1.0f, -1.0f, 1.0f)),
                     new Vertex(new Vector3(1.0f, 1.0f, 1.0f)),
                     new Vertex(new Vector3(1.0f, -1.0f, 1.0f)),
                     new Vertex(new Vector3(-1.0f, 1.0f, -1.0f)),
                     new Vertex(new Vector3(-1.0f, -1.0f, -1.0f)),
                     new Vertex(new Vector3(1.0f, 1.0f, -1.0f)),
                     new Vertex(new Vector3(1.0f, -1.0f, -1.0f)) };
    static readonly uint[] CubeIndices = {
                    0,2,3,
                    1,2,3,
                    4,6,5,
                    5,6,7,
                    2,6,3,
                    3,6,7,
                    4,0,5,
                    5,0,1,
                    0,4,6,
                    6,2,0,
                    5,7,1,
                    7,3,1,};
}
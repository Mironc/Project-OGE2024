using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Graphic;
using System.IO;
using System.Globalization;

class main
{
    private static Vector3 LightPosition =  new Vector3(0.0f,-10.0f,0.0f);
    private static Vector3 LightColor = new Vector3(1.0f,1.0f,1.0f);
    private static float SpecularStrength = 0.3f;
    private static float AmbientStrength = 0.2f;
    private static Vector3 ObjectColor = new Vector3(1.0f,1.0f,1.0f);
    private static double TimeFromStart;
    static float[] CubeVertexes = {
        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
         0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
         0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
         0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
        -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,

        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
         0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
        -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,

        -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,
        -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
        -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
        -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
        -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,
        -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,

         0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,
         0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
         0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
         0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
         0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,

        -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,
         0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,
         0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
         0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,

        -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,
         0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
        -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
        -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f};

    [STAThread]
    public static void Main(string[] args)
    {
        using (var window = new GameWindow(800,800,OpenTK.Graphics.GraphicsMode.Default,"Game",GameWindowFlags.Default,DisplayDevice.Default,3,3,OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible))
        {
            window.MakeCurrent();
            //создание фрагментного и вершиного шейдера
            int vertexShader = GL.CreateShader(OpenTK.Graphics.OpenGL.ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, new StreamReader("vert.glsl").ReadToEnd());
            GL.CompileShader(vertexShader);
            int success = 0;
            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out success);
            if (success == (int)All.False)
            {
                throw new Exception($"{GL.GetShaderInfoLog(vertexShader)}");
            }
            int fragmentShader = GL.CreateShader(OpenTK.Graphics.OpenGL.ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, new StreamReader("frag.glsl").ReadToEnd());
            GL.CompileShader(fragmentShader);
            success = 0;
            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
            if (success == (int)All.False)
            {
                throw new Exception($"{GL.GetShaderInfoLog(fragmentShader)}");
            }
            int Program = GL.CreateProgram();
            GL.AttachShader(Program,vertexShader);
            GL.AttachShader(Program, fragmentShader);
            GL.LinkProgram(Program);
            GL.GetProgram(Program, GetProgramParameterName.LinkStatus, out success);
            if (success == (int)All.False)
            {
                throw new Exception($"Linking error :{GL.GetProgramInfoLog(Program)}");
            }
            //отправляем модель в видеокарту для дальнейшей отрисовки
            int VAO = GL.GenVertexArray();
            int VBO = GL.GenBuffer();
            GL.BindVertexArray(VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer,VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float)*CubeVertexes.Length, CubeVertexes,BufferUsageHint.StaticDraw); //
            //Разметка того как будет восприниматься каждая вершина(сколько памяти занимает каждый вертекс и его атрибуты, а также где эти атрибуты распалогать в шейдерах)
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3,VertexAttribPointerType.Float , false, 6 * sizeof(float),0);
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            var ModelTransform = Matrix4.CreateRotationY(180.0f) * Matrix4.CreateTranslation(new Vector3(0.0f,0.0f,10.0f));//Матрица модели
            var CameraRotation = new Vector3();
            var CameraPosition = new Vector3();
            var CameraView = Matrix4.Identity;
            var CameraProjection = Matrix4.Identity;
            float Aspect =window.ClientSize.Width / window.ClientSize.Height;

            window.Resize += (sender, e) => { 
            GL.Viewport(window.ClientRectangle);
             Aspect = (float)window.ClientSize.Width / window.ClientSize.Height; };
            window.UpdateFrame += (sender, e) =>
            {
                if (!window.Focused) return;
                var KeyboardInput = Keyboard.GetState();
                if (KeyboardInput.IsKeyDown(Key.W)){CameraPosition += (Vector3.UnitZ* (float)e.Time * 10.0f);}
                if (KeyboardInput.IsKeyDown(Key.S)){CameraPosition += (-Vector3.UnitZ * (float)e.Time * 10.0f);}
                if (KeyboardInput.IsKeyDown(Key.A)){CameraPosition += (Vector3.UnitX * (float)e.Time * 10.0f);}
                if (KeyboardInput.IsKeyDown(Key.D)){CameraPosition += (-Vector3.UnitX * (float)e.Time * 10.0f);}
                if (KeyboardInput.IsKeyDown(Key.Up)){CameraRotation += (Vector3.UnitX * (float)e.Time * 10.0f);}
                if (KeyboardInput.IsKeyDown(Key.Down)){CameraRotation += (-Vector3.UnitX * (float)e.Time * 10.0f);}
                if (KeyboardInput.IsKeyDown(Key.Left)){CameraRotation += (Vector3.UnitY * (float)e.Time * 10.0f);}
                if (KeyboardInput.IsKeyDown(Key.Right)){CameraRotation += (-Vector3.UnitY* (float)e.Time * 10.0f);}
            };
            window.RenderFrame+= (sender, e) => {
                if (!window.Focused) return;
                TimeFromStart += e.Time;
                //e.Time это время прошедшее с последнего кадра
                GL.Enable(EnableCap.DepthTest); //Включение проверки глубины
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); // Очистка буфера цвета и глубины
                GL.ClearColor(.0f,.0f,.0f,0.0f); //Ставим фоновый цвет
                LightPosition.X =  (float)Math.Sin(TimeFromStart) *  10.0f; //Перемещение света
                GL.UseProgram(Program); //Делаем наши шейдеры активными
                var CameraRotationMatrix = new Matrix3(
                    Matrix4.CreateRotationX(MathHelper.DegreesToRadians(CameraRotation.X)) *
                     Matrix4.CreateRotationY(MathHelper.DegreesToRadians(CameraRotation.Y)) *
                     Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(CameraRotation.Z))); // Создание матрицы поворота
                CameraView = Matrix4.LookAt(CameraPosition,CameraPosition+Vector3.UnitZ*CameraRotationMatrix,Vector3.UnitY*CameraRotationMatrix); //Создание матрицы вида (отвечает за положение и поворот камеры)
                CameraProjection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Aspect, 0.01f, 1024.0f);//создание перспективной матрицы (создает эффект перспективы)
                //Отправка информации в шейдеры(матрицы модели,вида,перспективы, цвета и тд)
                GL.UniformMatrix4(GL.GetUniformLocation(Program,"transformation"),true,ref ModelTransform);
                GL.UniformMatrix4(GL.GetUniformLocation(Program, "view"), true, ref CameraView);
                GL.UniformMatrix4(GL.GetUniformLocation(Program, "projection"), true, ref CameraProjection);
                GL.Uniform3(GL.GetUniformLocation(Program,"LightColor"),ref LightColor);
                GL.Uniform3(GL.GetUniformLocation(Program, "LightPosition"), ref LightPosition);
                GL.Uniform1(GL.GetUniformLocation(Program, "Specular"), SpecularStrength);
                GL.Uniform1(GL.GetUniformLocation(Program, "Ambient"), AmbientStrength);
                GL.Uniform3(GL.GetUniformLocation(Program, "ObjectColor"), ref ObjectColor);
                GL.Uniform3(GL.GetUniformLocation(Program, "CameraPosition"), CameraView.ExtractTranslation());
                GL.BindVertexArray(VAO); //делаем нашу модель активной
                GL.DrawArrays(PrimitiveType.Triangles, 0,CubeVertexes.Length); // вызов отрисовки
                window.Context.SwapBuffers(); //Смена буферов передний на задний
            };
            window.Run();
        }
    }
}

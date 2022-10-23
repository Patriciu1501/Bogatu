using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


//Bogatu Patriciu gr 3132B

namespace OpenTK_console_sample02 {
    class SimpleWindow3D : GameWindow {
        
        private float[] oldCubeYCoords = new float[24] { -1f, 1f, 1f, -1f, -1f, -1f, -1f, -1f, -1f, -1f, 1f, 1f, -1f, -1f, 1f, 1f, 1f, 1f, 1f, 1f, -1f, 1f, 1f, -1f };
        private float[] cubeYCoords = new float[24] {-1f, 1f, 1f, -1f, -1f, -1f, -1f, -1f, -1f, -1f, 1f, 1f, -1f, -1f, 1f, 1f, 1f, 1f, 1f, 1f, -1f, 1f, 1f, -1f };
        private const float stepUp = 0.1f;
        private const float stepDown = -0.1f;


        

        const float rotation_speed = 100.0f;
        float angle;
        bool showCube = true;
        KeyboardState lastKeyPress;
        public SimpleWindow3D() : base(800, 600) {
            VSync = VSyncMode.On;
        }
 
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            GL.ClearColor(Color.DarkViolet);
           
        }

        
        protected override void OnResize(EventArgs e) {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

      
        protected override void OnUpdateFrame(FrameEventArgs e) {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();
            MouseState mouse = OpenTK.Input.Mouse.GetState();

            
            if (keyboard[OpenTK.Input.Key.Escape]) {
                Exit();
                return;
            }
            

            if (mouse[OpenTK.Input.MouseButton.Left]) {

                if (showCube == true) {
                    showCube = false;
                }
                else {
                    showCube = true;
                }
            }


            // la right click/hold button, cubul va urmari cursorul de-alungul axei OY

            if (mouse[OpenTK.Input.MouseButton.Right]) {

                MouseState ar = OpenTK.Input.Mouse.GetCursorState();


                for (int i = 0; i < cubeYCoords.Length; i++) {
                    cubeYCoords[i] = -((ar.Y / 50f) + oldCubeYCoords[i]) + 6;
                    
                }
            }
        }

      
        protected override void OnRenderFrame(FrameEventArgs e) {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(15, 20, 15, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            angle += rotation_speed * (float)e.Time;
            GL.Rotate(angle, 0.0f, 1.0f, 0.0f);

            KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();
            
            if (showCube == true) {


                // la key down cubul va urca 
                if (keyboard[OpenTK.Input.Key.Up]) {

                    DrawCube(stepUp);
                }

                // la key down coboara
                else if (keyboard[OpenTK.Input.Key.Down]) {

                    
                    DrawCube(stepDown);
                }

                else DrawCube();
                DrawAxes_OLD();



            }

            SwapBuffers();
            
        }

        private void DrawAxes_OLD() {
            GL.Begin(PrimitiveType.Lines);

            
            GL.Color3(Color.Transparent);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 0);

            GL.Color3(Color.Transparent);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 0);

            GL.Color3(Color.Transparent);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 0);


            GL.End();
        }

     
        private void DrawCube(float upDown=0f){

            GL.Begin(BeginMode.Quads);

            // valoarea y a fiecarui vertex se modifica daca este apasata vreo tasta(up/down)
            // sau daca se apasa/se tine apasat right mouse button
            GL.Color3(Color.Orange);
            GL.Vertex3(-1.0f, cubeYCoords[0] += upDown, -1.0f);
            GL.Vertex3(-1.0f, cubeYCoords[1] += upDown, -1.0f);
            GL.Vertex3(1.0f, cubeYCoords[2] += upDown, -1.0f);
            GL.Vertex3(1.0f, cubeYCoords[3] += upDown, -1.0f);

            GL.Color3(Color.Green);
            GL.Vertex3(-1.0f, cubeYCoords[4] += upDown, -1.0f);
            GL.Vertex3(1.0f, cubeYCoords[5] += upDown, -1.0f);
            GL.Vertex3(1.0f, cubeYCoords[6] += upDown, 1.0f);
            GL.Vertex3(-1.0f, cubeYCoords[7] += upDown, 1.0f);

            GL.Color3(Color.Silver);

            GL.Vertex3(-1.0f, cubeYCoords[8] += upDown, -1.0f);
            GL.Vertex3(-1.0f, cubeYCoords[9] += upDown, 1.0f);
            GL.Vertex3(-1.0f, cubeYCoords[10] += upDown, 1.0f);
            GL.Vertex3(-1.0f, cubeYCoords[11] += upDown, -1.0f);

            GL.Color3(Color.Red);
            GL.Vertex3(-1.0f, cubeYCoords[12] += upDown, 1.0f);
            GL.Vertex3(1.0f, cubeYCoords[13] += upDown, 1.0f);
            GL.Vertex3(1.0f, cubeYCoords[14] += upDown, 1.0f);
            GL.Vertex3(-1.0f, cubeYCoords[15] += upDown, 1.0f);

            GL.Color3(Color.Blue);
            GL.Vertex3(-1.0f, cubeYCoords[16] += upDown, -1.0f);
            GL.Vertex3(-1.0f, cubeYCoords[17] += upDown, 1.0f);
            GL.Vertex3(1.0f, cubeYCoords[18] += upDown, 1.0f);
            GL.Vertex3(1.0f, cubeYCoords[19] += upDown, -1.0f);

            GL.Color3(Color.Yellow);
            GL.Vertex3(1.0f, cubeYCoords[20] += upDown, -1.0f);
            GL.Vertex3(1.0f, cubeYCoords[21] += upDown, -1.0f);
            GL.Vertex3(1.0f, cubeYCoords[22] += upDown, 1.0f);
            GL.Vertex3(1.0f, cubeYCoords[23] += upDown, 1.0f);

            GL.End();


        }


    


            [STAThread]
        static void Main(string[] args) {

            
            using (SimpleWindow3D example = new SimpleWindow3D()) {
            
                example.Run(30.0, 0.0);
            }
        }
    }

}

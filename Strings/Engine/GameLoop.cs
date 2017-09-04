using OpenTK;
using OpenTK.Graphics.ES11;
using System.Diagnostics;
using Android.Util;

namespace Strings.Engine
{
    public static class GameLoop
    {
        
        /// <summary>
        /// 初始化时
        /// </summary>
        internal static void OnInit(float aspec)
        {
            stopwatch.Start();

            GameLoop.aspec = aspec;
        }

        /// <summary>
        /// 进行一帧逻辑更新
        /// </summary>
        internal static void OnUpdate()
        {
            if (stopwatch.ElapsedMilliseconds > 100) stopwatch.Restart();

            GL.MatrixMode(All.Projection);
            GL.LoadIdentity();
            GL.Ortho(aspec, -aspec, 1, -1, 0, 1);

            GL.MatrixMode(All.Modelview);
            //GL.LoadIdentity();
            GL.Rotate(3.0f, 0.0f, 0.0f, 1.0f);

            GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.VertexPointer(2, All.Float, 0, square_vertices);
            GL.EnableClientState(All.VertexArray);
            GL.ColorPointer(4, All.UnsignedByte, 0, square_colors);
            GL.EnableClientState(All.ColorArray);

            GL.DrawArrays(All.TriangleStrip, 0, 4);

            Log.Debug("GameLoopStopwatch", stopwatch.ElapsedMilliseconds.ToString());
            stopwatch.Restart();
        }

        static float[] square_vertices = {
            -0.5f, -0.5f,
            0.5f, -0.5f,
            -0.5f, 0.5f,
            0.5f, 0.5f,
        };

        static byte[] square_colors = {
            255, 255,   0, 255,
            0,   255, 255, 255,
            0,     0,    0,  0,
            255,   0,  255, 255,
        };
        static float aspec;
        static Stopwatch stopwatch = new Stopwatch();
    }
}
using OpenTK;
using OpenTK.Graphics.ES11;
using System.Diagnostics;
using System.Collections.Concurrent;
using Android.Util;

namespace Strings.Engine
{
    static class GameLoop
    {
        
        /// <summary>
        /// 初始化时
        /// </summary>
        internal static void OnInit(float aspec)
        {
            stopwatch.Start();
            GameLoop.aspec = aspec;

            Root.Attach(new Game.DemoObject());
        }

        /// <summary>
        /// 进行一帧逻辑更新
        /// </summary>
        internal static void OnUpdate()
        {
            //Event
            while (!EventList.IsEmpty)
            {

            }

            //Rendering
            GL.ClearColor(0, 0, 0, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.MatrixMode(All.Projection);
            GL.LoadIdentity();
            GL.Ortho(aspec, -aspec, 1, -1, 0, 1);
            GL.MatrixMode(All.Modelview);
            GL.LoadIdentity();

            //Update and Draw
            float ms = stopwatch.ElapsedMilliseconds;
            if (ms > 96) ms = 16;
            Root.OnUpdate(ms/1000.0f);
            stopwatch.Restart();

            Root.OnDraw();
        }



        public static GameObjectList Root { get; private set; } = new GameObjectList();
        public static ConcurrentQueue<Android.Views.MotionEvent> EventList =
            new ConcurrentQueue<Android.Views.MotionEvent>();

        static float aspec;
        static Stopwatch stopwatch = new Stopwatch();
    }
}
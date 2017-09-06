using OpenTK;
using OpenTK.Graphics.ES11;
using System.Diagnostics;
using System.Collections.Concurrent;
using Android.Util;
using Android.App;

namespace Strings.Engine
{
    static class GameLoop
    {
        
        /// <summary>
        /// 初始化时
        /// </summary>
        internal static void OnInit(float width,float height,Activity context)
        {
            stopwatch.Start();

            Context = context;

            halfWidth = width / 2;
            halfHeight = height / 2;
            aspec = width / height;

            Root.Attach(new Game.DemoObject());
        }

        public static Vector2 MapScreenPosToGame(Vector2 screenPos)
        {
            screenPos.X /= halfWidth;
            screenPos.Y /= halfHeight;
            screenPos -= new Vector2(1, 1);
            screenPos.X *= -aspec;
            return screenPos;
        }

        /// <summary>
        /// 进行一帧逻辑更新
        /// </summary>
        internal static void OnUpdate()
        {
            //Event
            while (!EventList.IsEmpty)
            { 
                if(EventList.TryDequeue(out TouchEvent eve))
                    Root.OnTouched(eve);
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

        public static Activity Context { get; private set; }

        public static GameObjectList Root { get; private set; } = new GameObjectList()
        {
            ListenTouchEvent = true
        };

        public static ConcurrentQueue<TouchEvent> EventList = new ConcurrentQueue<TouchEvent>();

        static float aspec;
        static float halfWidth, halfHeight;
        static Stopwatch stopwatch = new Stopwatch();
    }
}
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
        internal static void OnInit(float width, float height, Activity context)
        {
            stopwatch.Start();

            Context = context;

            halfWidth = width / 2;
            halfHeight = height / 2;
            aspec = width / height;

            Matrix4 m;
            Matrix4.CreateOrthographic(640 * aspec, 640, 0, 1,out m);
            Camera = m;

            Root.Attach(new Game.DemoObject());
        }

        public static Matrix4 Camera { get; private set; }

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
                if (EventList.TryDequeue(out TouchEvent eve))
                    Root.OnTouched(eve);
            }

            //Rendering
            GL.ClearColor(0, 1, 1, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //Update and Draw
            float ms = stopwatch.ElapsedMilliseconds;
            if (ms > 96) ms = 16;
            Root.OnUpdate(ms / 1000.0f);
            stopwatch.Restart();

            Root.OnDraw();
        }

        internal static void OnPaused()
        {
            //stopwatch.Stop();
            Root.OnPaused();
        }

        internal static void OnResume()
        {
            Root.OnResume();
            //stopwatch.Start();
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
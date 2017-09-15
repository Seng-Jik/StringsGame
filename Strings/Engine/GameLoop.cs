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

            Root.Attach(new Game.Startup());
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
            try
            {
                //Event
                while (!EventList.IsEmpty)
                {
                    if (EventList.TryDequeue(out TouchEvent eve))
                        Root.OnTouched(eve);
                }

                //Rendering
                GL.ClearColor(0, 0, 0, 0);
                GL.Clear(ClearBufferMask.ColorBufferBit);


                GL.MatrixMode(All.Projection);
                GL.LoadIdentity();
                GL.Ortho(640 * aspec, -640 * aspec, 640, -640, 0, 1);
                GL.MatrixMode(All.Modelview);
                GL.LoadIdentity();

                GL.EnableClientState(All.VertexArray);
                GL.EnableClientState(All.ColorArray);

                //Update and Draw
                float ms = stopwatch.ElapsedMilliseconds;
                if (ms > 96) ms = 16;
                Root.OnUpdate(ms / 1000.0f);
                while (!ActionQueue.IsEmpty)
                {
                    System.Action task;
                    if (ActionQueue.TryDequeue(out task))
                        task();
                }
                stopwatch.Restart();

                Root.OnDraw();
            }
            catch(System.Exception ex)
            {
                Log.Error("Game Exception", ex.ToString());
            }
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
        static public ConcurrentQueue<System.Action> ActionQueue { get; } = new ConcurrentQueue<System.Action>();
    }
}
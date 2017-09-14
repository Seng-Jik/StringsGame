using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.ES11;
using Android.Util;

using Strings.Engine;

namespace Strings.Game
{
    class DemoObject : GameObject
    {
        Android.Media.MediaPlayer m = Android.Media.MediaPlayer.Create(GameLoop.Context, Resource.Raw.demotest);

        public DemoObject()
        {
            //m.Start();
            
            
        }

        public override void Kill()
        {
            Died = true;
        }

        public override void OnDraw()
        {

            float[] v = {
                -300.5f, -300.5f,
                300.5f, -300.5f,
                300.5f, 300.5f
            };
            float[] col = {
                0,1,0,1,
                1,0,0,1,
                0,0,1,1
            };



            GL.VertexPointer(2, All.Float, 0, v);
            GL.ColorPointer(4, All.Float, 0, col);
            GL.DrawArrays(All.Triangles, 0, 3);

        }

        public override void OnUpdate(float time)
        {
            rotate += time * 90;
        }

        public override void OnTouched(TouchEvent te)
        {
            base.OnTouched(te);

            switch(te.Action)
            {
                case TouchEvent.TouchAction.Down:
                    fingers.Add(te.Id, te.Pos);
                    break;
                case TouchEvent.TouchAction.Up:
                    fingers.Remove(te.Id);
                    break;
                case TouchEvent.TouchAction.Cancel:
                    fingers.Clear();
                    break;
                case TouchEvent.TouchAction.Motion:
                    fingers[te.Id] = te.Pos;
                    break;
            }


            colored = fingers.Count > 0;

            Log.Debug("Touch Pos", te.Pos.ToString());
        }

        public override void OnPaused()
        {
            base.OnPaused();
            m.Pause();
        }

        public override void OnResume()
        {
            base.OnResume();
            m.Start();
        }

        float rotate = 0;
        bool colored = false;

        Dictionary<int, Vector2> fingers = new Dictionary<int, Vector2>();
    }
}
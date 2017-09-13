using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.ES11;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;

using Strings.Engine;

namespace Strings.Game
{
    class DemoObject : GameObject
    {
        Android.Media.MediaPlayer m = Android.Media.MediaPlayer.Create(GameLoop.Context, Resource.Raw.demotest);
        Engine.Renderer.Shader directPass = new Engine.Renderer.Shader("DirectPass", "White");

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
            directPass.Use();

            float[] v = { -0.5f, -0.5f, 0.5f, -0.5f, 0.5f, 0.5f };
            GL.EnableClientState(All.VertexArray);
            GL.VertexPointer(6, All.Float, 2, v);
            GL.DisableClientState(All.VertexArray);

            GL.DrawArrays(All.Triangles, 0, 3);

            Engine.Renderer.Shader.Unuse();
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
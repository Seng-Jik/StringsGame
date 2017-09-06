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
        public DemoObject()
        {
            Android.Media.MediaPlayer m = Android.Media.MediaPlayer.Create(GameLoop.Context, Resource.Raw.demotest);
            m.Start();
        }

        public override void Kill()
        {
            Died = true;
        }

        public override void OnDraw()
        {
            GL.Rotate(rotate, 0, 0, 1);

            GL.VertexPointer(2, All.Float, 0, square_vertices);
            GL.EnableClientState(All.VertexArray);

            if (colored)
            {
                GL.ColorPointer(4, All.UnsignedByte, 0, square_colors);
                GL.EnableClientState(All.ColorArray);
            }

            GL.DrawArrays(All.TriangleStrip, 0, 4);
            if (colored)
            {
                GL.ColorPointer(4, All.UnsignedByte, 0, IntPtr.Zero);
                GL.DisableClientState(All.ColorArray);
            }


            GL.LoadIdentity();



            List<float> verts = new List<float>();
            foreach(var i in fingers)
            {
                verts.Add(i.Value.X - 0.25f);   verts.Add(i.Value.Y - 0.25f);
                verts.Add(i.Value.X - 0.25f);   verts.Add(i.Value.Y + 0.25f);
                verts.Add(i.Value.X + 0.25f);   verts.Add(i.Value.Y + 0.25f);
            }

            var vertsArr = verts.ToArray();
            GL.VertexPointer(2, All.Float, 0, vertsArr);
            GL.DrawArrays(All.Triangles, 0, fingers.Count * 3);

            GL.VertexPointer(2, All.Float, 0, IntPtr.Zero);
            GL.DisableClientState(All.VertexArray);

            

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

        float rotate = 0;
        bool colored = false;

        Dictionary<int, Vector2> fingers = new Dictionary<int, Vector2>();
    }
}
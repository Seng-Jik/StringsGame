using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using OpenTK;
using OpenTK.Graphics.ES11;

namespace Strings.Engine
{
    public static class Renderer
    {
        //DrawState
        public enum BlendMode { Off,Normal,Addtive};
        public static void SetBlendMode(BlendMode mode)
        {

            switch(mode)
            {
                case BlendMode.Off:
                    GL.Disable(All.Blend);
                    break;
                case BlendMode.Normal:
                    GL.Enable(All.Blend);
                    GL.BlendFunc(All.SrcAlpha, All.OneMinusDstAlpha);
                    break;
                case BlendMode.Addtive:
                    GL.Enable(All.Blend);
                    GL.BlendFunc(All.SrcAlpha, All.One);
                    break;
            }
        }

        //Draw
        public static void FillBoxes(Box2[] vert, Vector4[] color)
        {
            //Vertex
            v.Clear();
            for (int i = 0; i < vert.Length; ++i)
            {
                v.Add(vert[i].Left); v.Add(vert[i].Top);
                v.Add(vert[i].Right); v.Add(vert[i].Top);
                v.Add(vert[i].Right); v.Add(vert[i].Bottom);

                v.Add(vert[i].Right); v.Add(vert[i].Bottom);
                v.Add(vert[i].Left); v.Add(vert[i].Bottom);
                v.Add(vert[i].Left); v.Add(vert[i].Top);
            }

            var vArr = v.ToArray();

            GL.DisableClientState(All.ColorArray);

            GL.VertexPointer(2, All.Float, 0, vArr);
            GL.DrawArrays(All.Triangles, 0, 3);

            GL.EnableClientState(All.ColorArray);
        }

        public static void FillColoredBoxes(Box2[] vert, Vector4[] color)
        {
            //Vertex
            v.Clear();
            for(int i = 0;i < vert.Length;++i)
            {
                v.Add(vert[i].Left); v.Add(vert[i].Top);
                v.Add(vert[i].Right); v.Add(vert[i].Top);
                v.Add(vert[i].Right); v.Add(vert[i].Bottom);

                v.Add(vert[i].Right); v.Add(vert[i].Bottom);
                v.Add(vert[i].Left); v.Add(vert[i].Bottom);
                v.Add(vert[i].Left); v.Add(vert[i].Top);
            }

            var vArr = v.ToArray();

            v.Clear();
            for(int i = 0;i < vert.Length;++i)
            {
                for (int j = 0; j < 6; ++j)
                {
                    v.Add(color[i].X);
                    v.Add(color[i].Y);
                    v.Add(color[i].Z);
                    v.Add(color[i].W);
                }
            }

            var vCol = v.ToArray();

            GL.VertexPointer(2, All.Float, 0, vArr);
            GL.ColorPointer(4, All.Float, 0, vCol);
            GL.DrawArrays(All.Triangles, 0, 3);
        }

        public static void DrawColorBoxes(Box2[] vert, Vector4[] color)
        {

        }

        public static void DrawLines(Box2[] vert,Vector4[] color)
        {

        }

        static List<float> v = new List<float>();
    }
}
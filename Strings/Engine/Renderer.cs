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
        public static void FillBoxes(Box2[] vert)
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
            GL.DrawArrays(All.Triangles, 0, 6 * vert.Length);

            GL.EnableClientState(All.ColorArray);
        }

        public static void FillBoxes(Box2[] vert, Vector4[] color)
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
            GL.DrawArrays(All.Triangles, 0, 6 * vert.Length);
        }

        public static void DrawBoxes(Box2[] vert)
        {
            //Vertex
            v.Clear();
            for (int i = 0; i < vert.Length; ++i)
            {
                v.Add(vert[i].Left); v.Add(vert[i].Top);
                v.Add(vert[i].Right); v.Add(vert[i].Top);

                v.Add(vert[i].Right); v.Add(vert[i].Top);
                v.Add(vert[i].Right); v.Add(vert[i].Bottom);

                v.Add(vert[i].Right); v.Add(vert[i].Bottom);
                v.Add(vert[i].Left); v.Add(vert[i].Bottom);

                v.Add(vert[i].Left); v.Add(vert[i].Bottom);
                v.Add(vert[i].Left); v.Add(vert[i].Top);
            }

            var vArr = v.ToArray();

            GL.DisableClientState(All.ColorArray);

            GL.VertexPointer(2, All.Float, 0, vArr);
            GL.DrawArrays(All.Lines, 0, 8 * vert.Length);

            GL.EnableClientState(All.ColorArray);
        }

        public static void DrawBoxes(Box2[] vert, Vector4[] color)
        {
            //Vertex
            v.Clear();
            for (int i = 0; i < vert.Length; ++i)
            {
                v.Add(vert[i].Left); v.Add(vert[i].Top);
                v.Add(vert[i].Right); v.Add(vert[i].Top);

                v.Add(vert[i].Right); v.Add(vert[i].Top);
                v.Add(vert[i].Right); v.Add(vert[i].Bottom);

                v.Add(vert[i].Right); v.Add(vert[i].Bottom);
                v.Add(vert[i].Left); v.Add(vert[i].Bottom);

                v.Add(vert[i].Left); v.Add(vert[i].Bottom);
                v.Add(vert[i].Left); v.Add(vert[i].Top);
            }
            var vArr = v.ToArray();

            v.Clear();
            for (int i = 0; i < vert.Length; ++i)
            {
                for (int j = 0; j < 8; ++j)
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
            GL.DrawArrays(All.Lines, 0, 8 * vert.Length);
        }

        public static void DrawLines(Vector2[] linesPoint,float rot = 0,float scale = 1)
        {
            GL.Scale(scale, scale, 0);
            GL.Rotate(rot, 0, 0, 1);
           
            GL.VertexPointer(2, All.Float, 0, linesPoint);
            GL.DisableClientState(All.ColorArray);
            GL.DrawArrays(All.Lines, 0, linesPoint.Length);
            GL.EnableClientState(All.ColorArray);
            GL.LoadIdentity();
        }

        public static void DrawImage(int imageID,Vector2 pos, Vector4 color, float zoom = 1)
        {
            GL.Enable(All.Texture2D);
            GL.EnableClientState(All.TextureCoordArray);
            GL.Translate(pos.X, pos.Y, 0);
            GL.Enable(All.Blend);

            GL.DisableClientState(All.ColorArray);

            GL.BlendFunc(All.SrcAlpha, All.OneMinusSrcAlpha);

            var success = imageDic.TryGetValue(imageID, out Image i);
            if (!success)
            {
                LoadImage(imageID);
                imageDic.TryGetValue(imageID, out i);
            }
            i.Size /= 2;
            i.Size *= zoom;

            GL.BindTexture(All.Texture2D, i.TexHandle);
            Vector2[] p =
            {
                pos - i.Size,
                new Vector2(pos.X - i.Size.X,pos.Y + i.Size.Y),
                new Vector2(pos.X + i.Size.X,pos.Y - i.Size.Y),

                new Vector2(pos.X + i.Size.X,pos.Y - i.Size.Y),
                pos + i.Size,
                new Vector2(pos.X - i.Size.X,pos.Y + i.Size.Y)
            };

            //Vector4[] col = { color, color, color, color, color, color };
            GL.Color4(color.X, color.Y, color.Z, color.W);

            GL.TexCoordPointer(2, All.Float, 0, texCoord);
            GL.VertexPointer(2, All.Float, 0, p);
            //GL.ColorPointer(4, All.ColorArray, 0, col);
            GL.DrawArrays(All.Triangles, 0, 6);

            GL.EnableClientState(All.ColorArray);

            GL.LoadIdentity();
            GL.Disable(All.Blend);
            GL.DisableClientState(All.TextureCoordArray);
            GL.Disable(All.Texture2D);
        }

        public static void ReloadResource()
        {
            imageDic.Clear();
        }

        private static void LoadImage(int res)
        {
            var stream = GameLoop.Context.Resources.OpenRawResource(res);
            var sst = new SSTReader(new System.IO.BinaryReader(stream));

            Image i;
            GL.GenBuffers(1, out i.TexHandle);
            var texSize = sst.Size;
            i.Size = texSize;
            GL.BindTexture(All.Texture2D,i.TexHandle);
            GL.TexImage2D(All.Texture2D, 0, (int)All.Rgba, (int)texSize.X, (int)texSize.Y, 0, All.Rgba, All.UnsignedByte, sst.Data);
            GL.TexParameter(All.Texture2D, All.TextureMagFilter, (int)All.Linear);
            GL.TexParameter(All.Texture2D, All.TextureMinFilter, (int)All.Linear);

            imageDic[res] = i;
        }

        struct Image
        {
            public int TexHandle;
            public Vector2 Size;
        }

        static Dictionary<int, Image> imageDic = new Dictionary<int, Image>();
        static List<float> v = new List<float>();


        readonly static Vector2[] texCoord =
         {
                new Vector2(1,0),
                new Vector2(1,1),
                new Vector2(0,0),

                new Vector2(0,0),
                new Vector2(0,1),
                new Vector2(1,1)
        };
    }
}
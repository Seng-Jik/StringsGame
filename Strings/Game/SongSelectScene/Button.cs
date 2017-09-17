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
using Strings.Engine;
using Strings.Game.GameObjects;
using OpenTK;

namespace Strings.Game.SongSelectScene
{
    class Button : GameObject
    {
        public Button(int id,Action down)
        {
            imageID = id;
            this.down = down;

            btnSp = new Sprite(imageID) { KillWhenAlphaIs0 = true };
            btnDown = new Sprite(Resource.Raw.press) { KillWhenAlphaIs0 = true };
            btnDown.Alpha.Func = x => (float)Math.Sin(System.Math.PI / 2 * x);

            size = Renderer.GetImageSize(id);
        }

        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);

            btnSp.Alpha.Value = 0;
            btnSp.Zoom.Value = 1.5f;
            btnDown.Alpha.Value = 0;
            btnDown.Zoom.Value = 1.5f;
            btnSp.Alpha.Lerp(0.5f, 1);

            parent.Attach(btnSp);
            parent.Attach(btnDown);
        }

        public override void Kill()
        {
            base.Kill();

            btnSp.Alpha.Lerp(0.5f, 0);
            btnDown.Alpha.Lerp(0.5f, 0);

            btnSp.Kill();
            btnDown.Kill();
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            btnDown.PosX.Value = PosX.Value;
            btnDown.PosY.Value = PosY.Value;
        }

        public override void OnTouched(TouchEvent te)
        {
            base.OnTouched(te);

            if (!Enabled) return;

            if(te.Action == TouchEvent.TouchAction.Down)
            {
                var s = size * btnSp.Zoom.Value;
                var lt = new Vector2(PosX.Value, PosY.Value) - new Vector2(s.X / 2, s.Y / 2);
                var rb = lt + s;
                var clickBox = new Box2(lt, rb);

                if (Utils.PointInBox(te.Pos,clickBox))
                {
                    btnDown.Alpha.Lerp(0.1F, 1);
                    pushed = true;
                }
            }
            else if(te.Action == TouchEvent.TouchAction.Up)
            {
                var s = size * btnSp.Zoom.Value;
                var lt = new Vector2(PosX.Value, PosY.Value) - new Vector2(s.X / 2, s.Y / 2);
                var rb = lt + s;
                var clickBox = new Box2(lt, rb);

                if (pushed)
                {
                    btnDown.Alpha.Value = 1F;
                    btnDown.Alpha.Lerp(0.8F, 0);
                }

                if (Utils.PointInBox(te.Pos, clickBox))
                {
                    down();
                }

                pushed = false;
            }
        }

        /*public override void OnDraw()
        {
            base.OnDraw();

            var s = size * btnSp.Zoom.Value;
            var lt = new Vector2(PosX.Value, PosY.Value) - new Vector2(s.X / 2, s.Y / 2);
            var rb = lt + s;
            Box2[] b = { new Box2(lt, rb) };
            Renderer.DrawBoxes(b);
        }*/

        public Lerper PosX { get => btnSp.PosX; }
        public Lerper PosY { get => btnSp.PosY; }
        public Lerper Alpha { get => btnSp.Alpha;  }

        public bool Enabled = true;

        Sprite btnSp;
        Sprite btnDown;
        int imageID;
        Vector2 size;

        Action down;

        bool pushed = false;
    }
}
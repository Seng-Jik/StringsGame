using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.ES11;
using Android.Util;

using Strings.Engine;

namespace Strings.Game
{
    class DemoObject : GameObject
    {
        public DemoObject()
        {
        }

        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);

            parent.Attach(
                new GameObjects.Task(
                    () => parent.Attach(new GameObjects.BGMPlayer(Resource.Raw.bwv846, 120))
                    )
                );
        }

        public override void Kill()
        {
            Died = true;
        }

        public override void OnDraw()
        {

            Box2[] b = {
                new Box2(-300, -300, -100, -100),
                new Box2(100,-300,300,-100)
            };
            

            Vector4[] col =
            {
                new Vector4(0.5F,1,0.5F,1.0F),
                new Vector4(1.0F,0,0,1)
            };

            Renderer.FillBoxes(b);
            Renderer.DrawBoxes(b, col);

            b[0].Top += 400;b[0].Bottom += 400;
            b[1].Top += 400;b[1].Bottom += 400;
            Renderer.FillBoxes(b,col);
            Renderer.DrawBoxes(b);
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

        float rotate = 0;
        bool colored = false;

        Dictionary<int, Vector2> fingers = new Dictionary<int, Vector2>();
    }
}
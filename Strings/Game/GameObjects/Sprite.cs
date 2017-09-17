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
using OpenTK;

namespace Strings.Game.GameObjects
{
    class Sprite : GameObject
    {
        public Lerper Alpha { get; } = new Lerper();
        public Lerper PosX { get; } = new Lerper();
        public Lerper PosY { get; } = new Lerper();
        public Lerper Zoom { get; } = new Lerper();

        int imageID;
        
        public Sprite(int imageID)
        {
            this.imageID = imageID;

            Zoom.Value = 1;
            Alpha.Value = 1;
        }

        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);
            parent.Attach(Alpha);
            parent.Attach(PosX);
            parent.Attach(PosY);
            parent.Attach(Zoom);
            Zoom.Func = x => (float)Math.Sqrt(1 - (x - 1) * (x - 1));
            Alpha.Func = Zoom.Func;
        }

        public override void Kill()
        {
            base.Kill();
            Alpha.Kill();
            PosX.Kill();
            PosY.Kill();
            Zoom.Kill();
        }

        public override void OnDraw()
        {
            base.OnDraw();
            Renderer.DrawImage(
                imageID,
                new Vector2(PosX.Value, PosY.Value),
                new Vector4(1,1,1,Alpha.Value),Zoom.Value,
                Rotate
                );

        }

        public override bool Died
        {
            get => KillWhenAlphaIs0 ? base.Died && Alpha.Value <= 0: base.Died;
        }

        public bool KillWhenAlphaIs0 { get; set; } = false;

        public float Rotate = 0;
    }
}
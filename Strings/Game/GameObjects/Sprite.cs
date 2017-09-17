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
        }

        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);
            parent.Attach(Alpha);
            parent.Attach(PosX);
            parent.Attach(PosY);
            parent.Attach(Zoom);
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
            Renderer.DrawImage(imageID, new OpenTK.Vector2(0, 0));

        }
    }
}
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

namespace Strings.Game.TitleScene
{
    class Start : Sprite
    {
        public Start() : base(Resource.Raw.start)
        {
            PosY.Value = 400.0F;
            KillWhenAlphaIs0 = true;
        }

        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);
            parent.Attach(mainAlpha);
            mainAlpha.Lerp(3, 1);
            mainAlpha.Kill();
        }

        public override void OnUpdate(double deltaTime)
        {
            base.OnUpdate(deltaTime);

            time += deltaTime;
            Alpha.Value = mainAlpha.Value * (0.25F * ((float)Math.Sin(time * 4) + 1.0F) + 0.5F);
        }

        public override void Kill()
        {
            base.Kill();

            Parent.Attach(mainAlpha);
            mainAlpha.Lerp(0.5F, 0);
            mainAlpha.Kill();
        }

        Lerper mainAlpha = new Lerper();

        double time = 0;
    }
}
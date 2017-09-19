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

namespace Strings.Game.GameScene
{
    class SideLine : GameObject
    {
        public override void OnDraw()
        {
            base.OnDraw();

            lines[0] = new Vector2(800, -640);
            lines[1] = new Vector2(800, -640 + 1280 * lineProgress.Value);

            lines[2] = new Vector2(-800, 640);
            lines[3] = new Vector2(-800, 640 - 1280 * lineProgress.Value);

            lines[4] = new Vector2(0, 640 * lineProgress.Value);
            lines[5] = new Vector2(0, -640 * lineProgress.Value);

            Renderer.DrawLines(lines);
        }

        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);
            parent.Attach(lineProgress);
            lineProgress.Value = 0;
            lineProgress.Lerp(0.5F, 1);
            lineProgress.Func = x => (float)Math.Sin(Math.PI / 2 * x);
        }

        public override void Kill()
        {
            base.Kill();

            lineProgress.Lerp(0.5f, 0);
            lineProgress.Kill();
        }

        public override bool Died
        {
            get => base.Died && lineProgress.Died;
        }

        Lerper lineProgress = new Lerper();
        Vector2[] lines = new Vector2[6];
    }
}
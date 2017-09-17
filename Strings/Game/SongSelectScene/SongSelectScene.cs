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

namespace Strings.Game.SongSelectScene
{
    class SongSelectScene : GameObjectList
    {
        public SongSelectScene()
        {
            ListenTouchEvent = true;
        }
        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);

            disc = new Sprite(Resource.Raw.disc);
            disc.Zoom.Value = 0;
            disc.Zoom.Lerp(1, 1.5F);
            disc.Alpha.Value = 0;
            disc.Alpha.Lerp(1, 1);
            disc.PosX.Func = x => (float)Math.Sin(System.Math.PI / 2 * x);

            Attach(new Task(() => disc.PosX.Lerp(0.5F, 200), 1.0f));
            Attach(disc);

            Action lastButton = () =>
            {
                last = new Button(Resource.Raw.last, () => { });
                last.PosX.Value = -100f;
                last.Alpha.Value = 0;
                last.Alpha.Lerp(1, 1);
                last.PosY.Func = disc.PosX.Func;
                last.PosY.Lerp(0.5F, 120);
                Attach(last);
            };

            Action nextButton = () =>
            {
                next = new Button(Resource.Raw.next, () => { });
                next.PosX.Value = -220f;
                next.Alpha.Value = 0;
                next.Alpha.Lerp(1, 1);
                next.PosY.Func = disc.PosX.Func;
                next.PosY.Lerp(0.5F, 120);
                Attach(next);
            };

            Action okButton = () =>
            {
                ok = new Button(Resource.Raw.ok, () => { });
                ok.PosX.Value = -340f;
                ok.Alpha.Value = 0;
                ok.Alpha.Lerp(1, 1);
                ok.PosY.Func = disc.PosX.Func;
                ok.PosY.Lerp(0.5F, 120);
                Attach(ok);
            };

            Attach(new Task(lastButton, 1f));
            Attach(new Task(nextButton, 0.75f));
            Attach(new Task(okButton, 0.5f));
        }

        Sprite disc;
        Button last, next, ok;
    }
}
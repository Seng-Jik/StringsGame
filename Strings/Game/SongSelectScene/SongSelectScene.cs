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
        public SongSelectScene(int songID)
        {
            ListenTouchEvent = true;

            curSel = songID;

            disc = new SongInfo.Disc(songID);
        }

        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);

            Attach(disc);

            Func<float,float> func = x => (float)Math.Sin(Math.PI / 2 * x);

            Action lastButton = () =>
            {
                last = new Button(Resource.Raw.last, LastSong);
                last.PosX.Value = -100f;
                last.Alpha.Value = 0;
                last.Alpha.Lerp(1, 1);
                last.PosY.Func = func;
                last.PosY.Lerp(0.5F, 220);
                Attach(last);
            };

            Action nextButton = () =>
            {
                next = new Button(Resource.Raw.next, NextSong);
                next.PosX.Value = -350f;
                next.Alpha.Value = 0;
                next.Alpha.Lerp(1, 1);
                next.PosY.Func = func;
                next.PosY.Lerp(0.5F, 220);
                Attach(next);
            };

            Action okButton = () =>
            {
                ok = new Button(Resource.Raw.ok, OK);
                ok.PosX.Value = -600f;
                ok.Alpha.Value = 0;
                ok.Alpha.Lerp(1, 1);
                ok.PosY.Func = func;
                ok.PosY.Lerp(0.5F, 220);
                Attach(ok);
            };

            Attach(new Task(lastButton, 1f));
            Attach(new Task(nextButton, 0.75f));
            Attach(new Task(okButton, 0.5f));
        }

        void NextSong()
        {
            DisableButtonForHalfSec();
            CurSel++;
        }

        void LastSong()
        {
            DisableButtonForHalfSec();
            CurSel--;

        }

        void OK()
        {
            EnableButtons(false);
            Func<float, float> func = x => (float)System.Math.Sqrt(1 - (x - 1) * (x - 1));

            Action lastButton = () =>
            {
                last.PosY.Func = func;
                last.Alpha.Lerp(0.5F, 0);
                last.PosY.Lerp(0.8F, -100);
            };

            Action nextButton = () =>
            {
                next.PosY.Func = func;
                next.Alpha.Lerp(0.5F, 0);
                next.PosY.Lerp(0.8F, -100);
            };

            Action okButton = () =>
            {
                ok.PosY.Func = func;
                ok.Alpha.Lerp(0.5F, 0);
                ok.PosY.Lerp(0.8F, -100);
            };

            lastButton();
            Attach(new Task(nextButton, 0.25f));
            Attach(new Task(okButton, 0.5f));

            disc.EntryEffectKill();
            Attach(new Task(() => Kill(), 1.5f));

            Parent.Attach(new Task(() => Parent.Attach(new GameScene.GameScene(curSel)), 2.0F));
        }

        void DisableButtonForHalfSec()
        {
            EnableButtons(false);
            Parent.Attach(new Task(() => EnableButtons(true), 0.75f));
        }

        void EnableButtons(bool enabled)
        {
            last.Enabled = next.Enabled = ok.Enabled = enabled;
        }

        SongInfo.Disc disc;
        Button last, next, ok;

        int CurSel
        {
            get => curSel;
            set
            {
                curSel = value;

                if (curSel < 0) curSel = 2;
                if (curSel > 2) curSel = 0;

                disc.SetSong(curSel);
            }
        }

        int curSel;
    }
}
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

namespace Strings.Game.ScoreScene
{
    class ScoreScene : GameObjectList
    {
        public ScoreScene(ScoreInfo info)
        {
            this.info = info;
            ListenTouchEvent = true;

            perfect = new ScoreGroup(Resource.Raw.perfect, -400, info.Perfect, false);
            great = new ScoreGroup(Resource.Raw.great, -250, info.Great, false);
            good = new ScoreGroup(Resource.Raw.good, -100, info.Good, false);
            miss = new ScoreGroup(Resource.Raw.miss, 50, info.Miss, false);
            maxCombo = new ScoreGroup(Resource.Raw.maxcombo, 200, info.MaxCombo, false,0.5f,1);

            float allNote = info.Perfect + info.Great + info.Good + info.Miss;
            float rateA = info.Perfect / allNote;
            float rateB = info.Great / allNote;
            float rateC = info.Good / allNote;
            float rateVal = rateA + rateB * 0.75f + rateB * 0.5f;
            rate = new ScoreGroup(Resource.Raw.rate, 350, (int)rateVal, true,0.5f, 1);
        }

        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);

            BGMPlayer bgm = new BGMPlayer(Resource.Raw.over);
            bgm.Volume.Value = 1;
            bgm.KillFadeOutTime = 2;
            Attach(bgm);
            Attach(new Task(() => clicked = false, 3));

            Attach(perfect);
            Action greatA = 
                () => Attach(great);
            Action goodA = 
                () => Attach(good);
            Action missA = 
                () => Attach(miss);
            Action maxComboA =
                () => Attach(maxCombo);
            Action rateA =
                () => Attach(rate);

            Attach(new Task(greatA, 0.1f));
            Attach(new Task(goodA, 0.2f));
            Attach(new Task(missA, 0.3f));
            Attach(new Task(maxComboA, 0.4f));
            Attach(new Task(rateA, 0.5F));
        }

        public override void OnTouched(TouchEvent te)
        {
            if (clicked) return;
            clicked = true;
            if(te.Action == TouchEvent.TouchAction.Down)
            {
                Parent.Attach(new Task(() => Kill(),2));
                Parent.Attach(new Task(
                    () =>
                    Parent.Attach(new SongSelectScene.SongSelectScene(info.SongID))
                    ,2));

                rate.Kill();
                Attach(new Task(() => maxCombo.Kill(), 0.1f));
                Attach(new Task(() => miss.Kill(), 0.2f));
                Attach(new Task(() => good.Kill(), 0.3f));
                Attach(new Task(() => great.Kill(), 0.4f));
                Attach(new Task(() => perfect.Kill(), 0.5f));
            }
        }

        ScoreInfo info;
        bool clicked = true;

        ScoreGroup perfect,great,good,miss,maxCombo,rate;
    }
}
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
    class ScoreGroup : GameObjectList
    {
        public ScoreGroup(int title, int y, int number, bool perc,float zoomA = 1,float zoomB = 1.5f)
        {
            this.zoomA = zoomA;
            this.zoomB = zoomB;

            Func<float, float> func = x => (float)Math.Sin(System.Math.PI / 2 * x);

            this.title = new Sprite(title);
            this.title.Zoom.Value = zoomA;
            this.title.Zoom.Func = func;
            this.title.Alpha.Value = 0;
            this.title.PosX.Value = 400;
            this.title.PosY.Value = y;
            this.title.KillWhenAlphaIs0 = true;

            this.number = new Sprite[3];
            var splited = SplitNumber(number);
            for (int i = 0; i < 3; ++i)
            {
                this.number[i] = GetNumberSp(splited[i]);
                this.number[i].PosY.Value = y;
                this.number[i].PosX.Value = -100 - 80 * i;
                this.number[i].KillWhenAlphaIs0 = true;
            }

            if (perc)
            {
                this.perc = new Sprite(Resource.Raw.perc);
                this.perc.PosY.Value = y;
                this.perc.PosX.Value = -100 - 80 * 3;
            }
        }

        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);
            Attach(title);
            
            title.Zoom.Lerp(0.5f,zoomB);
            title.Alpha.Lerp(0.5f, 1);

            var showTime = 0.0f;
            foreach(var i in number)
            {
                i.Alpha.Value = 0;
                i.Alpha.Lerp(0.5f, 1);
                i.Zoom.Value = 0.5f;
                i.Zoom.Lerp(0.5f, 1);

                Action show = () => Attach(i);
                var task = new Task(show, showTime);
                showTime += 0.1f;
                Attach(task);
            }

            if (perc != null)
            {
                perc.Alpha.Value = 0;
                perc.Alpha.Lerp(0.5f, 1);
                perc.Zoom.Value = 0.5f;
                perc.Zoom.Lerp(0.5f, 1);

                Action percshow = () => Attach(perc);
                var perctask = new Task(percshow, showTime);
                Attach(perctask);
            }
        }

        public override void Kill()
        {
            base.Kill();

            title.Zoom.Lerp(0.5f, zoomA);
            title.Alpha.Lerp(0.5f, 0);

            foreach (var i in number)
            {
                i.Alpha.Lerp(0.5f, 0);
                i.Zoom.Lerp(0.5f, 0);
            }

            if (perc != null)
            {
                perc.Alpha.Lerp(0.5f, 0);
                perc.Zoom.Lerp(0.5f, 0);
            }
        }

        static Sprite GetNumberSp(int number)
        {
            int[] resID =
            {
                Resource.Raw.zero,
                Resource.Raw.one,
                Resource.Raw.two,
                Resource.Raw.three,
                Resource.Raw.four,
                Resource.Raw.five,
                Resource.Raw.six,
                Resource.Raw.seven,
                Resource.Raw.eight,
                Resource.Raw.nine
            };
            return new Sprite(resID[number]);
        }

        static int[] SplitNumber(int number)
        {
            int[] ret = new int[3];
            ret[0] = number / 100;
            ret[1] = number / 10 % 10;
            ret[2] = number % 10;
            return ret;
        }

        Sprite title;
        float zoomA,zoomB;

        Sprite[] number;
        Sprite perc;
    }
}
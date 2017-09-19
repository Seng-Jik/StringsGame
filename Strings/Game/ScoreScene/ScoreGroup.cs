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
            
        }

        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);
            Attach(title);
            
            title.Zoom.Lerp(0.5f,zoomB);
            title.Alpha.Lerp(0.5f, 1);
        }

        public override void Kill()
        {
            base.Kill();

            title.Zoom.Lerp(0.5f, zoomA);
            title.Alpha.Lerp(0.5f, 0);
        }

        Sprite title;
        float zoomA,zoomB;
    }
}
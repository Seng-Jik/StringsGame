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
    class Lerper : GameObject
    {
        public float Value
        {
            get => value;
            set
            {
                this.value = value;
                lerping = false;
            }
        }

        public override bool Died
        {
            get => base.Died && !lerping;
        }

        public void Lerp(float time,float val)
        {
            lerping = true;
            nowTime = 0;
            allTime = time;
            target = val;
            begin = Value;
        }

        public Func<float, float> Func { get; set; } = x => x;

        public override void OnUpdate(double time)
        {
            if(lerping)
            {
                nowTime += time;
                double l = nowTime / allTime;
                if(l >= 1)
                {
                    lerping = false;
                    Value = target;
                }
                else
                {
                    value = Func((float)l) * (target - begin) + begin;
                }
            }
        }

        bool lerping = false;
        double nowTime, allTime;
        float begin;
        float target;
        float value;
    }
}
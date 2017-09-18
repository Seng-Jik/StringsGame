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

namespace Strings.Game.GameObjects
{
    class Task : Engine.GameObject
    {
        public Task(Action task,float time = 0)
        {
            this.time = time;
            this.task = task;
        }

        public override void OnUpdate(double deltaTime)
        {
            base.OnUpdate(deltaTime);
            time -= deltaTime;
            if (time <= 0)
            {
                task();
                Kill();
            }
        }

        double time;
        Action task;
    }
}
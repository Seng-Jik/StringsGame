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

namespace Strings.Game
{
    class Startup : GameObject
    {
        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);

            //Game Startup
            parent.Attach(new TitleScene.TitleScene());
            //parent.Attach(new SongSelectScene.SongSelectScene(2));
            //parent.Attach(new DemoObject());
            Kill();
        }
    }
}
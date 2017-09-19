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
            /*ScoreScene.ScoreInfo info = new ScoreScene.ScoreInfo(0)
            {
                Perfect = 10,
                Great = 20,
                Good = 30,
                Miss = 40,
                MaxCombo = 50
            };
            parent.Attach(new ScoreScene.ScoreScene(info));*/
            parent.Attach(new TitleScene.TitleScene());
            //parent.Attach(new GameScene.GameScene(1));
            //parent.Attach(new SongSelectScene.SongSelectScene(2));
            //parent.Attach(new DemoObject());
            Kill();
        }
    }
}
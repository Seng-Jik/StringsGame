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

namespace Strings.Game.TitleScene
{

    /// <summary>
    /// 标题页面的场景
    /// </summary>
    class TitleScene : GameObjectList
    {
        public TitleScene()
        {
            KillSelfWhenEmpty = true;
        }

        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);

            var bgm = new GameObjects.BGMPlayer(Resource.Raw.bwv846, 0);
            Attach(bgm);
            Attach(new GameObjects.Task(() => bgm.Kill(), 10));
            Attach(new GameObjects.Task(() => parent.Attach(new DemoObject()), 10));
        }
    }
}
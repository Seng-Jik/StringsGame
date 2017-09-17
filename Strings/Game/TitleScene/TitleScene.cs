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

            var bgm = new BGMPlayer(Resource.Raw.bwv846, 0);
            Attach(bgm);
            Attach(new StarDrawer());
            Attach(new Sprite(Resource.Raw.start));
        }

        public override void OnTouched(TouchEvent te)
        {
            base.OnTouched(te);

            if(te.Action == TouchEvent.TouchAction.Down & !touched)
            {
                touched = true;

                Parent.Attach(new Task(() => Parent.Attach(new DemoObject()),5));
                Kill();
            }
        }

        bool touched = false;
    }
}
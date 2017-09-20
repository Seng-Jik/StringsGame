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

            var bgm = new BGMPlayer(Resource.Raw.bwv846);
            bgm.Volume.Value = 1.0F;
            Attach(bgm);
            Attach(new StarDrawer());

            title = new Sprite(Resource.Raw.title);
            title.Alpha.Value = 0;
            title.Alpha.Lerp(1.5f, 1);
            title.Zoom.Value = 0.5f;
            title.Zoom.Lerp(1.5f, 2);
            title.KillWhenAlphaIs0 = true;
            Attach(title);
            Attach(new Start());

            gLogo = new Sprite(Resource.Raw.aji);
            gLogo.PosY.Value = 200;
            gLogo.Alpha.Value = 0;
            gLogo.Alpha.Lerp(2, 1);
            gLogo.KillWhenAlphaIs0 = true;
            gLogo.Zoom.Value = 1.5F;
            Attach(gLogo);
        }

        public override void OnTouched(TouchEvent te)
        {
            base.OnTouched(te);

            if(te.Action == TouchEvent.TouchAction.Down & !touched)
            {
                touched = true;

                title.Alpha.Lerp(1, 0);
                title.Zoom.Lerp(1, 4);
                Parent.Attach(new Task(() => Parent.Attach(new SongSelectScene.SongSelectScene(0)),0.25F));
                //Parent.Attach(new DemoObject());
                Kill();
            }
        }

        public override void Kill()
        {
            base.Kill();

            gLogo.Alpha.Lerp(0.5F, 0);
        }

        bool touched = false;
        Sprite title;

        Sprite gLogo;
    }
}
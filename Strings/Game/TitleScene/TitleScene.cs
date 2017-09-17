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

            title = new Sprite(Resource.Raw.title);
            title.Alpha.Value = 0;
            title.Alpha.Lerp(1.5f, 1);
            title.Zoom.Value = 0.5f;
            title.Zoom.Lerp(1.5f, 2);
            title.KillWhenAlphaIs0 = true;
            Attach(title);
            Attach(new Start());
        }

        public override void OnTouched(TouchEvent te)
        {
            base.OnTouched(te);

            if(te.Action == TouchEvent.TouchAction.Down & !touched)
            {
                touched = true;

                title.Alpha.Lerp(1, 0);
                title.Zoom.Lerp(1, 4);
                Parent.Attach(new Task(() => Parent.Attach(new SongSelectScene.SongSelectScene()),0.25F));
                
                Kill();
            }
        }

        bool touched = false;
        Sprite title;
    }
}
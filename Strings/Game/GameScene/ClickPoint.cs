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
using OpenTK;

namespace Strings.Game.GameScene
{
    class ClickPoint : GameObject
    {
        public ClickPoint(BeatmapEditor.BeatMap.Note.HandEnum hand,int clickPos,float ms)
        {
            this.ms = ms;
            handMul = hand == BeatmapEditor.BeatMap.Note.HandEnum.Left ? -1 : 1;
            switch (clickPos)
            {
                case 0:
                    heightTop = 640;
                    heightBottom = 320;
                    break;
                case 1:
                    heightTop = 320;
                    heightBottom = 0;
                    break;
                case 2:
                    heightTop = 0;
                    heightBottom = -320;
                    break;
                case 3:
                    heightTop = -320;
                    heightBottom = -640;
                    break;
            }
        }

        public override void OnDraw()
        {
            base.OnDraw();

            double progress = 1 - ((ms - ((GameScene)Parent).TimeMs.ElapsedMilliseconds) / 2000.0f);
            float x = (float)progress * 800 * handMul;

            line[0] = new Vector2(x, heightTop);
            line[1] = new Vector2(x, heightBottom);

            if(render)
               Renderer.DrawLines(line);
        }

        public override void OnUpdate(double deltaTime)
        {
            base.OnUpdate(deltaTime);


            if (((GameScene)Parent).TimeMs.ElapsedMilliseconds > ms)
            {
                render = false;
            }

            if (((GameScene)Parent).TimeMs.ElapsedMilliseconds > ms + 200)
            {
                (Parent as GameScene).NoteClicked(GameScene.NoteClickType.Miss, handMul);
                Died = true;
            }
        }

        public override void OnTouched(TouchEvent te)
        {
            base.OnTouched(te);

            if(te.Action == TouchEvent.TouchAction.Down)
            {
                if (handMul > 0 && te.Pos.X < 0) return;
                else if (handMul < 0 && te.Pos.X > 0) return;
                    
                if (te.Pos.Y <= heightTop && te.Pos.Y >= heightBottom)
                {

                    var timeOffset = Math.Abs(((GameScene)Parent).TimeMs.ElapsedMilliseconds - ms);
                    if (timeOffset <= 200)
                    {
                        var type = GameScene.NoteClickType.Good;
                        if (timeOffset <= 100)
                            type = GameScene.NoteClickType.Perfect;
                        else if (timeOffset <= 150)
                            type = GameScene.NoteClickType.Great;
                        (Parent as GameScene).NoteClicked(type, handMul);
                        Kill();
                    }
                }
            }
        }

        bool render = true;
        Vector2[] line = new Vector2[2];
        float heightTop, heightBottom;
        readonly float ms;
        int handMul;
    }
}
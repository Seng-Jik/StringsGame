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

namespace Strings.Game.ScoreScene
{
    struct ScoreInfo
    {
        public int Perfect, Great, Good, Miss, MaxCombo;
        public readonly int SongID;

        public ScoreInfo(int songID)
        {
            Perfect = 0;
            Great = 0;
            Good = 0;
            MaxCombo = 0;
            Miss = 0;
            SongID = songID;
        }
    }
}
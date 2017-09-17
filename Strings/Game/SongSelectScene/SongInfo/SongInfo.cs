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

namespace Strings.Game.SongSelectScene.SongInfo
{
    struct Song
    {
        public int MusicID, CoverID, InfoID;
    }

    static class SongInfo
    {
        static SongInfo()
        {
            songs.Add(new Song {
                MusicID = Resource.Raw.kizuna,
                CoverID = Resource.Raw.coverkizuna,
                InfoID = Resource.Raw.ifokizuna});
            songs.Add(new Song {
                MusicID = Resource.Raw.wind,
                CoverID = Resource.Raw.coverwind,
                InfoID = Resource.Raw.ifowind});
            songs.Add(new Song {
                MusicID = Resource.Raw.bwv578,
                CoverID = Resource.Raw.coverbwv,
                InfoID = Resource.Raw.ifobwv});
        }

        static List<Song> songs = new List<Song>();

        public static List<Song> Songs { get => songs; }
    }
}
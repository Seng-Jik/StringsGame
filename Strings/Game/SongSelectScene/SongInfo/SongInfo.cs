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
    public struct Song
    {
        public int MusicID, CoverID, InfoID, NoteID;
    }

    public static class SongInfo
    {
        static SongInfo()
        {
            songs.Add(new Song
            {
                MusicID = Resource.Raw.kizuna,
                CoverID = Resource.Raw.coverkizuna,
                InfoID = Resource.Raw.ifokizuna,
                NoteID = Resource.Raw.noteskizuna
            });
            songs.Add(new Song
            {
                MusicID = Resource.Raw.wind,
                CoverID = Resource.Raw.coverwind,
                InfoID = Resource.Raw.ifowind,
                NoteID = Resource.Raw.noteswind
            });
            songs.Add(new Song
            {
                MusicID = Resource.Raw.bwv578,
                CoverID = Resource.Raw.coverbwv,
                InfoID = Resource.Raw.ifobwv,
                NoteID = Resource.Raw.notesbwv
            });
        }

        static List<Song> songs = new List<Song>();

        public static List<Song> Songs { get => songs; }
    }
}
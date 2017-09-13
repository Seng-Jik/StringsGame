using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;

namespace Strings.Engine.Platform
{
    // the ConfigurationChanges flags set here keep the EGL context
    // from being destroyed whenever the device is rotated or the
    // keyboard is shown (highly recommended for all GL apps)
    [Activity(
                    Label = "Strings",
                    ConfigurationChanges = ConfigChanges.KeyboardHidden,
                    ScreenOrientation = ScreenOrientation.Landscape,
                    MainLauncher = true,
                    Icon = "@mipmap/icon")]
    class MainActivity : Activity
    {
        GLView view;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RequestWindowFeature(WindowFeatures.NoTitle);
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

            // Create our OpenGL view, and display it
            view = new GLView(this);
            SetContentView(view);

            
            
           
        }

        protected override void OnPause()
        {
            // never forget to do this!
            base.OnPause();
            view.Pause();

            GameLoop.OnPaused();
        }

        protected override void OnResume()
        {
            // never forget to do this!
            base.OnResume();
            view.Resume();

            GameLoop.OnResume();
        }
    }
}
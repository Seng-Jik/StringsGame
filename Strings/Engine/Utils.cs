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
using OpenTK;

namespace Strings.Engine
{
    static class Utils
    {
        public static bool PointInBox(Vector2 point, Box2 box) =>
            point.X >= box.Left && point.X <= box.Right && 
            point.Y >= box.Top && point.Y <= box.Bottom;
           
    }
}
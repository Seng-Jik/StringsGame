using OpenTK;

namespace Strings.Engine
{
    public struct TouchEvent
    {
        public enum TouchAction
        {
            Up,Down,Motion,Cancel
        }

        public TouchAction Action;
        public Vector2 Pos;
        public int Id;
    }
}
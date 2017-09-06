using OpenTK;

namespace Strings.Engine
{
    struct TouchEvent
    {
        public enum TouchAction
        {
            Up,Down,Motion
        }

        public TouchAction Action;
        public Vector2 Pos;
    }
}
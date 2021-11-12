using System;



namespace dots
{
    public enum Key
    {
        UP,
        LEFT,
        DOWN,
        RIGHT,
        ENTER,
        CONTROL,
        RAITING,
        UNKNOWN,
        EXIT
    }

    public class InputReader
    {
        public static Key GetKey()
        {
            char c = Console.ReadKey().KeyChar;
            switch (c)
            {
                case 'w':
                    return Key.UP;
                case 'a':
                    return Key.LEFT;
                case 's':
                    return Key.DOWN;
                case 'd':
                    return Key.RIGHT;
                case 'f':
                    return Key.ENTER;
                case 'q':
                    return Key.EXIT;
                case 'r':
                    return Key.RAITING;
                case 'c':
                    return Key.CONTROL;
                default:
                    return Key.UNKNOWN;
            }
        }
    }
}
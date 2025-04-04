using System;

namespace Mahjong
{
    internal static class SuitExtension
    {
        internal static Suit ToSuit(this char c)
        {
            return (Suit)Enum.Parse(typeof(Suit), c.ToString());
        }
    }
}
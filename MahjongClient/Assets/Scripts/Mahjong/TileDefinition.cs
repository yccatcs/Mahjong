using System;
using System.Collections.Generic;
using Mahjong.Constants;

namespace Mahjong
{
    internal static class TileDefinition
    {
        internal readonly static Dictionary<Suit, char> SuitTable = new()
        {
            { Suit.m, 'm' },
        };

        private static Dictionary<Tile, int> m_tileCount = null;

        internal static Dictionary<Tile, int> GetTileCount()
        {
            if (m_tileCount != null)
                return m_tileCount;

            m_tileCount = new();
            (int min, int max) range;
            const int countPerTile = GameConfig.CountPerTile;
            const int countAkadora = Akadora.Count;
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                if (suit == Suit.z)
                    range = ((int)Honour.Wind.Ton, (int)Honour.Dragon.Chun);
                else
                    range = (Terminal.Li, Terminal.Kyuu);

                foreach (int number in Generic.RangeInclusive(range.min, range.max))
                {
                    if (suit != Suit.z && number == Akadora.Uu)
                    {
                        m_tileCount.Add(new Tile(Akadora.Uu, suit), countPerTile - countAkadora);
                        m_tileCount.Add(new Tile(Akadora.DoraUu, suit), countAkadora);
                    }
                    else
                    {
                        m_tileCount.Add(new Tile(number, suit), countPerTile);
                    }
                }
            }
            return m_tileCount;
        }

        internal static IEnumerable<Tile> GetTiles()
        {
            foreach ((Tile tile, int count) in GetTileCount())
            {
                for (int i = 0; i < count; ++i)
                {
                    yield return tile;
                }
            }
        }
    }
}
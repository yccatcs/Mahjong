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

        internal static (int begin, int end) GetNumberRange()
        {
            return (Terminal.Li, Terminal.Kyuu);
        }

        internal static (int begin, int end) GetWindRange()
        {
            return (Honour.Wind.Ton, Honour.Wind.Pei);
        }

        internal static (int begin, int end) GetDragonRange()
        {
            return (Honour.Dragon.Haku, Honour.Dragon.Chun);
        }

        internal static (int begin, int end) GetRange(Suit suit)
        {
            if (suit == Suit.z)
                return (Honour.Wind.Ton, Honour.Dragon.Chun);
            else
                return GetNumberRange();
        }

        internal static (int begin, int end) GetSequentialRange(Tile tile)
        {
            if (tile.IsWind())
                return GetWindRange();
            if (tile.IsDragon())
                return GetDragonRange();
            return GetNumberRange();
        }

        internal static Tile GetNextTile(Tile tile)
        {
            return new Tile(Generic.GetNextInRangeInclusive(tile.GetNumber(), GetSequentialRange(tile)), tile.GetSuit());
        }

        internal static Dictionary<Tile, int> GetTileCount()
        {
            if (m_tileCount != null)
                return m_tileCount;

            m_tileCount = new();
            const int countPerTile = GameConfig.CountPerTile;
            const int countAkadora = Akadora.Count;
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (int number in Generic.RangeInclusive(GetRange(suit)))
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

        internal static IEnumerable<Tile> GetTilesType()
        {
            foreach ((Tile tile, _) in GetTileCount())
            {
                if (tile.IsAkadora())
                    continue;
                yield return tile;
            }
        }
    }
}
using System.Collections.Generic;

namespace Mahjong
{
    /// <summary>
    /// 杠子
    /// </summary>
    internal abstract class Kantsu : TileGroup
    {
        internal Kantsu(Tile tile) : base(tile)
        {
        }

        internal Kantsu(Dictionary<Tile, int> tiles) : base(tiles)
        {
        }

        internal Kantsu(List<Tile> tiles) : base(tiles)
        {
        }

        internal const int Count = 4;

        internal override int GetCount()
        {
            return Kantsu.Count;
        }

        internal override bool IsValid()
        {
            if (!base.IsValid())
                return false;
            if (!IsAllEqual())
                return false;
            return true;
        }
    }
}
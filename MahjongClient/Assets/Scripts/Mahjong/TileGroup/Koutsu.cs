using System.Collections.Generic;

namespace Mahjong
{
    /// <summary>
    /// 刻子
    /// </summary>
    internal abstract class Koutsu : TileGroup
    {
        protected Koutsu(Tile tile) : base(tile)
        {
        }

        protected Koutsu(Dictionary<Tile, int> tiles) : base(tiles)
        {
        }

        protected Koutsu(List<Tile> tiles) : base(tiles)
        {
        }

        internal const int Count = 3;

        internal override int GetCount()
        {
            return Koutsu.Count;
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
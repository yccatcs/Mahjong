using System.Collections.Generic;

namespace Mahjong
{
    /// <summary>
    /// 顺子
    /// </summary>
    internal abstract class Shuntsu : TileGroup
    {
        internal Shuntsu(Dictionary<Tile, int> tiles) : base(tiles)
        {
        }

        internal Shuntsu(List<Tile> tiles) : base(tiles)
        {
        }

        internal const int Count = 3;
        
        internal override int GetCount()
        {
            return Shuntsu.Count;
        }

        internal override bool IsValid()
        {
            if (!base.IsValid())
                return false;
            if (!IsSequential())
                return false;
            return true;
        }
    }
}
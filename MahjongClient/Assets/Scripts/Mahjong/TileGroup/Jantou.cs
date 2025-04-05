using System.Collections.Generic;

namespace Mahjong
{
    /// <summary>
    /// 雀头
    /// </summary>
    internal class Jantou : TileGroup
    {
        internal Jantou(Tile tile) : base(tile)
        {
        }

        internal Jantou(Dictionary<Tile, int> tiles) : base(tiles)
        {
        }

        internal Jantou(List<Tile> tiles) : base(tiles)
        {
        }

        internal const int Count = 2;

        internal override int GetCount()
        {
            return Jantou.Count;
        }

        internal override bool IsOpen()
        {
            return false;
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
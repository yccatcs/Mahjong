using System.Collections.Generic;

namespace Mahjong
{
    /// <summary>
    /// 暗刻
    /// </summary>
    internal class Minkou : Koutsu
    {
        internal Minkou(Tile tile) : base(tile)
        {
        }

        internal Minkou(Dictionary<Tile, int> tiles) : base(tiles)
        {
        }

        internal Minkou(List<Tile> tiles) : base(tiles)
        {
        }

        internal override bool IsOpen()
        {
            return true;
        }
    }
}
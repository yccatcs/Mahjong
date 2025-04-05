using System.Collections.Generic;

namespace Mahjong
{
    /// <summary>
    /// 暗刻
    /// </summary>
    internal class Ankou : Koutsu
    {
        internal Ankou(Tile tile) : base(tile)
        {
        }

        internal Ankou(Dictionary<Tile, int> tiles) : base(tiles)
        {
        }

        internal Ankou(List<Tile> tiles) : base(tiles)
        {
        }

        internal override bool IsOpen()
        {
            return false;
        }
    }
}
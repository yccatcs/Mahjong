using System.Collections.Generic;

namespace Mahjong
{
    /// <summary>
    /// 明杠
    /// </summary>
    internal class Minkan : Kantsu
    {
        internal Minkan(Tile tile) : base(tile)
        {
        }

        internal Minkan(Dictionary<Tile, int> tiles) : base(tiles)
        {
        }

        internal Minkan(List<Tile> tiles) : base(tiles)
        {
        }

        internal override bool IsOpen()
        {
            return true;
        }
    }
}
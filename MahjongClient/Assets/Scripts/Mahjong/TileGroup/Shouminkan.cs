using System.Collections.Generic;

namespace Mahjong
{
    /// <summary>
    /// 小明杠
    /// </summary>
    internal class Shouminkan : Kantsu
    {
        internal Shouminkan(Tile tile) : base(tile)
        {
        }

        internal Shouminkan(Dictionary<Tile, int> tiles) : base(tiles)
        {
        }

        internal Shouminkan(List<Tile> tiles) : base(tiles)
        {
        }

        internal override bool IsOpen()
        {
            return true;
        }
    }
}
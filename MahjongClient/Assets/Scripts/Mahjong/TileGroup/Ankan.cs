using System.Collections.Generic;

namespace Mahjong
{
    /// <summary>
    /// 暗杠
    /// </summary>
    internal class Ankan : Kantsu
    {
        internal Ankan(Tile tile) : base(tile)
        {
        }

        internal Ankan(Dictionary<Tile, int> tiles) : base(tiles)
        {
        }

        internal Ankan(List<Tile> tiles) : base(tiles)
        {
        }

        internal override bool IsOpen()
        {
            return false;
        }
    }
}
using System.Collections.Generic;

namespace Mahjong
{
    /// <summary>
    /// 明顺
    /// </summary>
    internal class Minjun : Shuntsu
    {
        internal Minjun(Dictionary<Tile, int> tiles) : base(tiles)
        {
        }

        internal Minjun(List<Tile> tiles) : base(tiles)
        {
        }

        internal override bool IsOpen()
        {
            return true;
        }
    }
}
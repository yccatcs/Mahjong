using System.Collections.Generic;

namespace Mahjong
{
    /// <summary>
    /// 暗顺
    /// </summary>
    internal class Anjun : Shuntsu
    {
        internal Anjun(Dictionary<Tile, int> tiles) : base(tiles)
        {
        }

        internal Anjun(List<Tile> tiles) : base(tiles)
        {
        }

        internal override bool IsOpen()
        {
            return false;
        }
    }
}
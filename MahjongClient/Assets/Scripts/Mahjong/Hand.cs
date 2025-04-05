using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace Mahjong
{
    /// <summary>
    /// 手牌
    /// </summary>
    internal class Hand
    {
        /// <summary>
        /// 手牌
        /// </summary>
        private List<Tile> m_tiles;

        /// <summary>
        /// 摸牌
        /// </summary>
        private Tile m_tileDraw;

        /// <summary>
        /// 
        /// </summary>
        private List<TileGroup> m_tileGroups;

        internal Hand(IEnumerable<Tile> tiles, Tile tileDraw)
        {
            m_tiles = new(tiles);
            m_tileDraw = tileDraw;
            m_tileGroups = new();
        }

        internal Hand(string str, string strDraw)
        {
            m_tiles = new();
            for (int index = 0; index < str.Length - 1; index += 2)
            {
                m_tiles.Add(new Tile(str.Substring(index, 2)));
            }
            if (strDraw.Length == 2)
                m_tileDraw = new(strDraw);
            m_tileGroups = new();
        }

        internal List<TileGroup> GetTileGroups()
        {
            return m_tileGroups;
        }

        internal Dictionary<Tile, int> GetTileDictionary()
        {
            return m_tiles.ToTiles().AddTile(m_tileDraw);
        }

        public override string ToString()
        {
            List<Tile> tiles = m_tiles;
            tiles.Sort();
            return string.Join(string.Empty, tiles.Append(m_tileDraw));
        }
    }
}
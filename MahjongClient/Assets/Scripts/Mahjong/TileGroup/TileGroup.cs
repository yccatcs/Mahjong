using System.Collections.Generic;
using System.Linq;

namespace Mahjong
{
    internal abstract class TileGroup
    {
        protected List<Tile> m_tiles;

        protected TileGroup()
        {
            m_tiles = new();
        }

        protected TileGroup(Tile tile)
        {
            SetTiles(tile);
        }

        protected TileGroup(Dictionary<Tile, int> tiles) : this()
        {
            SetTiles(tiles);
        }

        protected TileGroup(List<Tile> tiles)
        {
            SetTiles(tiles);
        }

        public override string ToString()
        {
            return string.Join(string.Empty, m_tiles);
        }

        public override bool Equals(object obj)
        {
            if (obj is not TileGroup other)
                return false;
            List<Tile> tile = new(m_tiles);
            List<Tile> tileOther = new(other.m_tiles);
            if (tile.Count != tileOther.Count)
                return false;
            tile.Sort();
            tileOther.Sort();
            for (int index = 0; index < tile.Count; ++index)
            {
                if (!tile[index].IsSame(tileOther[index]))
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return string.Join(string.Empty, m_tiles).GetHashCode();
        }

        internal void SetTiles(Tile tile)
        {
            m_tiles = tile.Repeat(GetCount()).ToList();
        }

        internal void SetTiles(Dictionary<Tile, int> tiles)
        {
            foreach ((Tile tile, int count) in tiles)
            {
                m_tiles.AddRange(tile.Repeat(count).ToList());
            }
        }

        internal void SetTiles(List<Tile> tiles)
        {
            m_tiles = tiles;
        }

        /// <summary>
        /// 数量
        /// </summary>
        internal abstract int GetCount();

        internal Dictionary<Tile, int> GetTileDictionary()
        {
            return m_tiles.ToTiles();
        }

        /// <summary>
        /// 是否合法
        /// </summary>
        internal virtual bool IsValid()
        {
            if (m_tiles.Count != GetCount())
                return false;
            return true;
        }

        /// <summary>
        /// 是否视为副露
        /// </summary>
        internal abstract bool IsOpen();

        protected bool IsSequential()
        {
            List<Tile> tileSorted = m_tiles;
            tileSorted.Sort();
            Tile tilePrevious = null;
            foreach (Tile tile in tileSorted)
            {
                if (tilePrevious != null && !tilePrevious.GetNext().IsSame(tile))
                    return false;
                tilePrevious = tile;
            }
            return true;
        }

        protected bool IsAllEqual()
        {
            return m_tiles.Count(tile => tile.IsSame(m_tiles.First())) == GetCount();
        }
    }
}
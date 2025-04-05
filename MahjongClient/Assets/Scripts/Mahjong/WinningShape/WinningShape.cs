using System.Collections.Generic;
using System.Linq;

namespace Mahjong
{
    internal class WinningShape
    {
        private Jantou m_jantou;
        private List<TileGroup> m_tileGroups;

        internal const int TileGroupCount = 4;

        internal WinningShape()
        {
            m_jantou = null;
            m_tileGroups = new();
        }

        internal WinningShape(Jantou jantou)
        {
            m_jantou = jantou;
            m_tileGroups = new();
        }
        
        internal WinningShape(Jantou jantou, List<TileGroup> tileGroups)
        {
            m_jantou = jantou;
            m_tileGroups = tileGroups;
        }

        internal void SetJantou(Jantou jantou)
        {
            m_jantou = jantou;
        }

        internal void PushTileGroup(TileGroup tileGroup)
        {
            m_tileGroups.Add(tileGroup);
        }

        internal TileGroup PopTileGroup()
        {
            if (m_tileGroups.Count <= 0)
                return null;
            TileGroup tileGroup = m_tileGroups.Last();
            m_tileGroups.RemoveAt(m_tileGroups.Count - 1);
            return tileGroup;
        }

        internal int GetCount()
        {
            return m_tileGroups.Count;
        }

        internal bool IsComplete()
        {
            return m_tileGroups.Count == TileGroupCount;
        }

        public override string ToString()
        {
            return $"{m_jantou} | {string.Join(" ", m_tileGroups)}";
        }
    }
}
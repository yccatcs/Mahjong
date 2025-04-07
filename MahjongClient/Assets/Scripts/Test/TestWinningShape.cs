using System.Collections.Generic;
using System.Linq;
using Mahjong.UI;
using UnityEngine;

namespace Mahjong.Test
{
    internal static class TestWinningShape
    {
        private static List<Tile> m_tiles;
        private static int m_cur;
        private static Hand m_hand;
        private static UIHand m_uiHand;

        internal static Tile Take()
        {
            return m_tiles.ElementAt(m_cur++);
        }

        internal static IEnumerable<Tile> Take(int count)
        {
            IEnumerable<Tile> result = m_tiles.Skip(m_cur).Take(count);
            m_cur += count;
            return result;
        }

        internal static void InitHand(UIHand uiHand = null)
        {
            Debug.Log($"[Mahjong][Test][TestWinningShape] InitHand");
            m_uiHand = uiHand;
            m_tiles = TileDefinition.GetTiles().RandomShuffle().ToList();
            Debug.Log(string.Join(string.Empty, m_tiles));
            m_cur = 0;
            List<Tile> tilesSorted = new(Take(14));
            tilesSorted.Sort();
            m_hand = new(tilesSorted.Take(13), tilesSorted.Last());
            if (uiHand != null)
                uiHand.Init(m_hand, tile => RemoveFromHand(tile));
            Debug.Log(m_hand);
        }

        internal static void RemoveFromHand(Tile tile)
        {
            if (m_hand != null)
            {
                List<Tile> tiles = m_hand.GetTiles();
                if (tiles.Remove(tile))
                    tiles.Add(m_hand.GetTileDraw());
                tiles.Sort();
                m_hand.SetTileDraw(Take());
                if (m_uiHand != null)
                    m_uiHand.Init(m_hand, RemoveFromHand);
                Debug.Log(m_hand);
                
                foreach (Tile tenpai in m_hand.GetTileDictionary(false).GetTenpai())
                {
                    Debug.LogError($"Tenpai: {tenpai}");
                    foreach (WinningShape winningShape in m_hand.GetTileDictionary(false).AddTile(tenpai, true).GetWinningShape())
                    {
                        Debug.Log(winningShape);
                    }
                }

                foreach (WinningShape winningShape in m_hand.GetWinningShape())
                {
                    Debug.LogError($"WinningShape: {winningShape}");
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mahjong.UI
{
    internal class UIHand : MonoBehaviour
    {
        [SerializeField] private RectTransform m_contentRectTransform;
        [SerializeField] private GameObject m_tileView;
        [SerializeField] private GameObject m_split;

        private List<(GameObject go, UITileView tileView)> m_tiles = new();
        private (GameObject go, UITileView tileView) m_tileHand = new();

        (GameObject go, UITileView tileView) InstantiateTileView(Tile tile, ref int count, Action<Tile> onClickUITileView = null)
        {
            GameObject go = Instantiate(m_tileView, m_contentRectTransform);
            UITileView tileView = go.GetComponent<UITileView>();
            tileView.Init(tile, onClickUITileView);
            go.transform.SetSiblingIndex(count++);
            return (go, tileView);
        }

        internal void Init(Hand hand, Action<Tile> onClickUITileView = null)
        {
            foreach ((GameObject go, _) in m_tiles)
            {
                Destroy(go);
            }
            Destroy(m_tileHand.go);

            int count = 0;
            foreach (Tile tile in hand.GetTiles())
            {
                m_tiles.Add(InstantiateTileView(tile, ref count, onClickUITileView));
            }
            m_split.transform.SetSiblingIndex(count++);
            m_tileHand = InstantiateTileView(hand.GetTileDraw(), ref count, onClickUITileView);
        }
    }
}
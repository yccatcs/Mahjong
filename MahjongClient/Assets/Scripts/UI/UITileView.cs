using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Mahjong.UI
{
    internal class UITileView : MonoBehaviour
    {
        [SerializeField] private Texture2D m_texture2D;
        [SerializeField] private Image m_image;

        private static Dictionary<string, Sprite> m_dictionarySprite = null;

        private Tile m_tile;

        internal Action<Tile> onClick;

        private void LoadDictionarySprite()
        {
            if (m_dictionarySprite == null)
            {
                m_dictionarySprite = new();
                foreach (Sprite sprite in Resources.LoadAll<Sprite>(m_texture2D.name))
                {
                    m_dictionarySprite.Add(sprite.name.Split('_').Last(), sprite);
                }
            }
        }

        private Sprite GetSprite(string key)
        {
            LoadDictionarySprite();
            return m_dictionarySprite.TryGetValue(key, out Sprite sprite) ? sprite : null;
        }

        internal void Init(Tile tile, Action<Tile> onClick)
        {
            m_tile = tile;
            this.onClick = onClick;
            m_image.sprite = GetSprite(tile.ToString());
        }

        public void OnClick()
        {
            Debug.Log($"OnClick {m_tile}");
            onClick?.Invoke(m_tile);
        }
    }
}
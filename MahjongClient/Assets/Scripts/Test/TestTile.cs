using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mahjong.Test
{
    internal static class TestTile
    {
        internal static void PrintAllTiles()
        {
            Debug.Log($"[Mahjong][Test][TestTile] PrintAllTiles");
            foreach (Tile tile in TileDefinition.GetTiles())
            {
                Debug.Log(tile);
            }
        }

        internal static void PrintSortedAllTiles()
        {
            Debug.Log($"[Mahjong][Test][TestTile] PrintSortedAllTiles");
            List<Tile> tiles = TileDefinition.GetTiles().ToList();
            tiles.Sort();
            foreach (Tile tile in tiles)
            {
                Debug.Log(tile);
            }
        }

        internal static void PrintAllTilesRandom()
        {
            Debug.Log($"[Mahjong][Test][TestTile] PrintAllTilesRandom");
            Debug.Log(Random.Range(0, 100));
            foreach (Tile tile in TileDefinition.GetTiles().RandomShuffle())
            {
                Debug.Log(tile);
            }
        }

        internal static IEnumerable<Tile> GetRandomTiles(int count)
        {
            foreach (Tile tile in TileDefinition.GetTiles().RandomShuffle())
            {
                if (count-- <= 0)
                    break;
                yield return tile;
            }
        }

        internal static void PrintRandomTiles(int count = 14)
        {
            Debug.Log($"[Mahjong][Test][TestTile] PrintRandomTiles({count})");
            List<Tile> tiles = GetRandomTiles(count).ToList();
            Debug.Log(string.Join(string.Empty, tiles));
        }

        internal static void PrintSortedRandomTiles(int count = 14)
        {
            Debug.Log($"[Mahjong][Test][TestTile] PrintSortedRandomTiles({count})");
            List<Tile> tiles = GetRandomTiles(count).ToList();
            tiles.Sort();
            Debug.Log(string.Join(string.Empty, tiles));
        }
    }
}
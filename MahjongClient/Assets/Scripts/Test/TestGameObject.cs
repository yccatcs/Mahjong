using UnityEngine;

namespace Mahjong.Test
{
    internal class TestGameObject : MonoBehaviour
    {
        internal void Start()
        {
            DoTestTile();
        }

        internal void DoTestTile()
        {
            // TestTile.PrintAllTiles();
            // TestTile.PrintSortedAllTiles();
            // TestTile.PrintAllTilesRandom();
            // TestTile.PrintRandomTiles();
            TestTile.PrintSortedRandomTiles();
        }

        internal void Update()
        {
            
        }
    }
}
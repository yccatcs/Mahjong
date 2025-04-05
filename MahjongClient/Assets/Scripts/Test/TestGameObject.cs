using UnityEngine;

namespace Mahjong.Test
{
    internal class TestGameObject : MonoBehaviour
    {
        internal void Start()
        {
            DoTestTile();
            DoTestHand();
        }

        internal void DoTestTile()
        {
            // TestTile.PrintAllTiles();
            // TestTile.PrintSortedAllTiles();
            // TestTile.PrintAllTilesRandom();
            // TestTile.PrintRandomTiles();
            TestTile.PrintSortedRandomTiles();
        }

        internal void DoTestHand()
        {
            // TestHand.PrintHand();
            // TestHand.PrintHandDictionary();
            // TestHand.PrintHandByStringInput();
            TestHand.PrintWinningShapeByStringInput();
            TestHand.PrintTenpai();
        }

        internal void Update()
        {
            
        }
    }
}
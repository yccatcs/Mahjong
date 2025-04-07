using Mahjong.UI;
using UnityEngine;

namespace Mahjong.Test
{
    internal class TestGameObject : MonoBehaviour
    {
        internal void Start()
        {
            DoTestTile();
            DoTestHand();
            DoTestWinningShape();
        }

        internal void DoTestTile()
        {
            // TestTile.PrintAllTiles();
            // TestTile.PrintSortedAllTiles();
            // TestTile.PrintAllTilesRandom();
            // TestTile.PrintRandomTiles();
            // TestTile.PrintSortedRandomTiles();
        }

        internal void DoTestHand()
        {
            // TestHand.PrintHand(m_hand);
            // TestHand.PrintHandDictionary();
            // TestHand.PrintHandByStringInput();
            TestHand.PrintWinningShapeByStringInput();
            TestHand.PrintTenpai();
        }

        [SerializeField] private UIHand m_hand;
        internal void DoTestWinningShape()
        {
            TestWinningShapeRefresh();
        }

        public void TestWinningShapeRefresh()
        {
            TestWinningShape.InitHand(m_hand);
        }

        internal void Update()
        {
            
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Mahjong.UI;
using UnityEngine;

namespace Mahjong.Test
{
    internal static class TestHand
    {
        internal static void PrintHand(UIHand uiHand = null)
        {
            Debug.Log($"[Mahjong][Test][TestHand] PrintHand");
            List<Tile> tiles = TestTile.GetRandomTiles(14).ToList();
            List<Tile> tilesSorted = new(tiles.Take(13));
            tilesSorted.Sort();
            Hand hand = new(tilesSorted, tiles.Last());
            if (uiHand != null)
                uiHand.Init(hand);
            Debug.Log(hand);
        }
        
        internal static void PrintHandDictionary()
        {
            Debug.Log($"[Mahjong][Test][TestHand] PrintHandDictionary");
            List<Tile> tiles = TestTile.GetRandomTiles(14).ToList();
            Hand hand = new(tiles.Take(13), tiles.Last());
            Debug.Log(hand);
            foreach ((Tile tile, int count) in hand.GetTileDictionary())
            {
                Debug.Log($"{tile}: {count}");
            }
        }
        
        internal static void PrintHandByStringInput()
        {
            Debug.Log($"[Mahjong][Test][TestHand] PrintHandByStringInput");
            string str = "1m1m1m2m3m4m5m5m6m7m8m9m9m";
            string strDraw = "9m";
            Hand hand = new(str, strDraw);
            Debug.Log(hand);
            foreach ((Tile tile, int count) in hand.GetTileDictionary())
            {
                Debug.Log($"{tile}: {count}");
            }
        }

        internal static void PrintWinningShapeByStringInput(UIHand uiHand = null)
        {
            Debug.Log($"[Mahjong][Test][TestHand] PrintWinningShapeByStringInput");
            string str = "1m1m1m1m2m2m2m2m3m3m3m3m0m";
            string strDraw = "5m";
            Hand hand = new(str, strDraw);
            if (uiHand != null)
                uiHand.Init(hand);
            Debug.Log(hand);
            foreach (WinningShape winningShape in hand.GetWinningShape())
            {
                Debug.Log(winningShape);
            }
        }

        internal static void PrintTenpai()
        {
            Debug.Log($"[Mahjong][Test][TestHand] PrintTenpai");
            string str = "1m2m3m4m0m6m4p0p6p8p8p0s6s";
            Hand hand = new(str, string.Empty);
            Debug.Log(hand);
            foreach (Tile tile in hand.GetTileDictionary().GetTenpai())
            {
                Debug.Log($"Tenpai: {tile}");
                foreach (WinningShape winningShape in hand.GetTileDictionary().AddTile(tile, true).GetWinningShape())
                {
                    Debug.Log(winningShape);
                }
            }
        }
    }
}
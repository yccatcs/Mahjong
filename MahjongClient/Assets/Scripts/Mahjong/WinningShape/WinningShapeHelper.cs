using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Tiles = System.Collections.Generic.Dictionary<Mahjong.Tile, int>;

namespace Mahjong
{
    internal static class WinningShapeHelper
    {
        internal static Tiles AddTile(this Tiles tiles, Tile tile, bool bCopy = false)
        {
            if (tile == null)
                return tiles;
            if (bCopy)
                tiles = new(tiles);
            tiles.TryAdd(tile, 0);
            ++tiles[tile];
            return tiles;
        }

        internal static Tiles ToTiles(this List<Tile> listTile)
        {
            Tiles tiles = new();
            foreach (Tile tile in listTile)
            {
                tiles.AddTile(tile);
            }
            return tiles;
        }

        internal static Tile GetNotAkadora(this Tile tile)
        {
            if (tile.IsAkadora())
                return new Tile(tile.GetNumber(), tile.GetSuit());
            return tile;
        }

        private static Tiles RemoveTile(this Tiles tiles, List<Tile> listTile, bool bCopy = true)
        {
            if (bCopy)
                tiles = new(tiles);
            foreach (Tile tile in listTile)
            {
                tiles.TryAdd(tile, 0);
                --tiles[tile];
            }
            return tiles;
        }

        internal static (List<Tile> listTile, Tiles tilesRemain) TryRemoveTile(this Tiles tiles, Tile tileToRemove, int removeCount = 1)
        {
            tileToRemove = tileToRemove.GetNotAkadora();
            List<Tile> listTileToRemove = new();
            foreach ((Tile tile, int count) in tiles)
            {
                if (tile.IsSame(tileToRemove))
                {
                    int remove = Mathf.Min(removeCount, count);
                    listTileToRemove.AddRange(tile.Repeat(remove));
                    removeCount -= remove;
                    if (removeCount == 0)
                        break;
                }
            }
            if (removeCount <= 0)
                return (listTileToRemove, tiles.RemoveTile(listTileToRemove));
            return (null, tiles);
        }


        internal static bool IsEmpty(this Tiles tiles)
        {
            foreach ((_, int count) in tiles)
            {
                if (count != 0)
                    return false;
            }
            return true;
        }

        internal static bool IsValid(this Tiles tiles)
        {
            foreach ((_, int count) in tiles)
            {
                if (count < 0)
                    return false;
            }
            return true;
        }

        internal static IEnumerable<(Jantou jantou, Tiles tiles)> FindJantou(this Tiles tiles, HashSet<TileGroup> ignores = null)
        {
            ignores ??= new();
            foreach ((Tile tile, _) in tiles)
            {
                (List<Tile> jantouTiles, Tiles tilesRemain) = tiles.TryRemoveTile(tile, Jantou.Count);
                Jantou jantou = new(jantouTiles ?? new());
                if (jantou.IsValid() && !ignores.Contains(jantou) && tilesRemain.IsValid())
                    yield return (jantou, tilesRemain);
            }
        }

        internal static IEnumerable<(TileGroup mentsu, Tiles tiles)> FindMentsu(this Tiles tiles, HashSet<TileGroup> ignores = null)
        {
            ignores ??= new();
            foreach ((Tile tile, _) in tiles)
            {
                {
                    (List<Tile> ankouTiles, Tiles tilesRemain) = tiles.TryRemoveTile(tile, Koutsu.Count);
                    Ankou ankou = new(ankouTiles ?? new());
                    if (ankou.IsValid() && !ignores.Contains(ankou) && tilesRemain.IsValid())
                        yield return (ankou, tilesRemain);
                }
                if (!tile.IsHonour() && tile.GetNumber() <= TileDefinition.GetNumberRange().end - Shuntsu.Count + 1)
                {
                    List<Tile> anjunTiles = new();
                    Tiles tilesRemain = new(tiles);
                    Tile tileCurrent = tile;
                    for (int i = 0; i < Shuntsu.Count; ++i)
                    {
                        (List<Tile> anjunTile, Tiles tilesRemainNew) = tilesRemain.TryRemoveTile(tileCurrent);
                        if (anjunTile == null)
                            break;
                        anjunTiles.AddRange(anjunTile);
                        tilesRemain = tilesRemainNew;
                        tileCurrent = tileCurrent.GetNext();
                    }
                    Anjun anjun = new(anjunTiles);
                    if (anjun.IsValid() && !ignores.Contains(anjun) && tilesRemain.IsValid())
                        yield return (anjun, tilesRemain);
                }
            }
        }

        private static IEnumerable<WinningShape> FindWinningShape(WinningShape winningShape, Tiles tiles, HashSet<TileGroup> ignores = null)
        {
            if (winningShape.IsComplete() && tiles.IsEmpty())
            {
                yield return winningShape;
            }
            else
            {
                ignores ??= new();
                ignores = new(ignores);
                foreach ((TileGroup mentsu, Tiles tilesRemain) in tiles.FindMentsu(ignores))
                {
                    winningShape.PushTileGroup(mentsu);
                    foreach (WinningShape shape in FindWinningShape(winningShape, tilesRemain, ignores))
                    {
                        yield return shape;
                    }
                    winningShape.PopTileGroup();
                    ignores.Add(mentsu);
                }
            }
        }
        
        internal static IEnumerable<WinningShape> GetWinningShape(this Tiles tiles, List<TileGroup> tileGroups = null, HashSet<TileGroup> ignores = null)
        {
            tileGroups ??= new();
            ignores ??= new();
            ignores = new(ignores);
            foreach ((Jantou jantou, Tiles tilesWithoutJantou) in tiles.FindJantou(ignores))
            {
                foreach (WinningShape winningShape in FindWinningShape(new(jantou, tileGroups), tilesWithoutJantou))
                {
                    yield return winningShape;
                }
                ignores.Add(jantou);
            }
        }
        
        internal static IEnumerable<WinningShape> GetWinningShape(this Hand hand)
        {
            return GetWinningShape(hand.GetTileDictionary(), hand.GetTileGroups());
        }

        internal static IEnumerable<Tile> GetTenpai(this Tiles tiles)
        {
            foreach (Tile tile in TileDefinition.GetTilesType())
            {
                if (tiles.AddTile(tile, true).GetWinningShape().Count() > 0)
                    yield return tile;
            }
        }
    }
}
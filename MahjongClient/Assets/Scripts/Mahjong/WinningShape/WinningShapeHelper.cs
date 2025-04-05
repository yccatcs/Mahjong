using System.Collections.Generic;
using System.Linq;
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

        internal static Tiles RemoveTileGroup(this Tiles tiles, TileGroup tileGroup, bool bCopy = true)
        {
            if (bCopy)
                tiles = new(tiles);
            foreach ((Tile tile, int count) in tileGroup.GetTileDictionary())
            {
                tiles.TryAdd(tile, 0);
                tiles[tile] -= count;
            }
            return tiles;
        }

        internal static bool IsEmpty(this Tiles tiles)
        {
            foreach ((Tile tile, int count) in tiles)
            {
                if (tiles[tile] != 0)
                    return false;
            }
            return true;
        }

        internal static bool IsValid(this Tiles tiles)
        {
            foreach ((Tile tile, int count) in tiles)
            {
                if (tiles[tile] < 0)
                    return false;
            }
            return true;
        }

        internal static IEnumerable<(Jantou jantou, Tiles tiles)> FindJantou(this Tiles tiles)
        {
            foreach ((Tile tile, int count) in tiles)
            {
                if (count >= Jantou.Count)
                {
                    Jantou jantou = new(tile);
                    Tiles tilesRemain = tiles.RemoveTileGroup(jantou);
                    if (tilesRemain.IsValid())
                        yield return (jantou, tilesRemain);
                }
            }
        }

        internal static IEnumerable<(TileGroup mentsu, Tiles tiles)> FindMentsu(this Tiles tiles, HashSet<TileGroup> ignores = null)
        {
            ignores ??= new();
            foreach ((Tile tile, int count) in tiles)
            {
                if (count >= Koutsu.Count)
                {
                    Ankou ankou = new(tile);
                    Tiles tilesRemain = tiles.RemoveTileGroup(ankou);
                    if (!ignores.Contains(ankou) && tilesRemain.IsValid())
                        yield return (ankou, tilesRemain);
                }
                if (!tile.IsHonour() && tile.GetNumber() <= TileDefinition.GetNumberRange().end - Shuntsu.Count + 1)
                {
                    Anjun anjun = new(new List<Tile>() { tile, tile.GetNext(), tile.GetNext().GetNext() });
                    Tiles tilesRemain = tiles.RemoveTileGroup(anjun);
                    if (!ignores.Contains(anjun) && tilesRemain.IsValid())
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
        
        internal static IEnumerable<WinningShape> GetWinningShape(this Tiles tiles, List<TileGroup> tileGroups = null)
        {
            tileGroups ??= new();
            foreach ((Jantou jantou, Tiles tilesWithoutJantou) in tiles.FindJantou())
            {
                foreach (WinningShape winningShape in FindWinningShape(new(jantou, tileGroups), tilesWithoutJantou))
                {
                    yield return winningShape;
                }
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
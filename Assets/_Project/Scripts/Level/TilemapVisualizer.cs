using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.WSA;

namespace dragoni7
{
    public class TilemapVisualizer : MonoBehaviour
    {
        [SerializeField] private Tilemap floorTilemap, wallTilemap, backgroundTilemap;

        [SerializeField] private TileBase floorTile, wallTop, backgroundTile;

        public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
        {
            PaintTiles(floorPositions, floorTilemap, floorTile);
        }

        private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
        {
            foreach (var position in positions)
            {
                PaintSingleTile(tilemap, tile, position);
            }
        }

        private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
        {
            var tilePosition = tilemap.WorldToCell((Vector3Int)position);
            tilemap.SetTile(tilePosition, tile);
        }

        public void PaintSingleBasicWall(Vector2Int wallPosition)
        {
            PaintSingleTile(wallTilemap, wallTop, wallPosition);
        }

        public void PaintBackground(IEnumerable<Vector2Int> floorPositions)
        {
            for (int x = -300; x < 300; x++)
            {
                for (int y = -300; y < 300; y++)
                {
                    Vector2Int position = new Vector2Int(x, y);
                    if (!floorPositions.Contains(position))
                    {
                        PaintSingleTile(backgroundTilemap, backgroundTile, position);
                    }
                }
            }
        }

        public void Clear()
        {
            floorTilemap.ClearAllTiles();
            wallTilemap.ClearAllTiles();
        }
    }
}

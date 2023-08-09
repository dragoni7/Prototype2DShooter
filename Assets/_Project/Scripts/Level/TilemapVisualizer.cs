using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace dragoni7
{
    public class TilemapVisualizer : MonoBehaviour
    {
        [SerializeField] private Tilemap floorTilemap, wallTilemap, doorTilemap;

        [SerializeField] private TileBase floorTile, wallTop, doorTile;

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
        public void PaintSingleBasicDoor(Vector2Int wallPosition)
        {
            PaintSingleTile(doorTilemap, doorTile, wallPosition);
        }

        public void Clear()
        {
            floorTilemap.ClearAllTiles();
            wallTilemap.ClearAllTiles();
        }
    }
}

using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHelper: MonoBehaviour {
	public static void SetTiles(Tilemap tileMap, Tile tile, RectInt bounds) {
		foreach (var boundCell in bounds.allPositionsWithin) {
			tileMap.SetTile((Vector3Int) boundCell, tile);
		}
	}
	
	public static Details GetResourceDetailsFromMap(Vector3Int position, Tilemap tileMap) {
		TileBase tileBase = tileMap.GetTile(position);
		if (tileBase) {
			foreach (GameObject resourceInstance in Helpers.GetResources()) {
				Details details = resourceInstance.GetComponent<Details>();
				if (details.IsOverlapping(position)) {
					return details;
				}
			}
		}
		return null;
	}
}
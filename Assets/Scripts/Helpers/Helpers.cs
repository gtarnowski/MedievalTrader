using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Helpers : MonoBehaviour {
	public static GameObject[] GetResources() {
		return GameObject.FindGameObjectsWithTag("Resource");
	}

	public static GameObject[] GetBuildings() {
		return GameObject.FindGameObjectsWithTag("Building");
	}
	
	public static Details GetResourceDetailsFromMap(Vector3Int position, Tilemap tileMap) {
		TileBase tileBase = tileMap.GetTile(position);
		if (tileBase) {
			foreach (GameObject resourceInstance in GetResources()) {
				Details details = resourceInstance.GetComponent<Details>();
				if (details.IsOverlapping(position)) {
					return details;
				}
			}
		}
		return null;
	}
}
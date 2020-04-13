using UnityEngine;
using UnityEngine.Tilemaps;

public class InjectResources : MonoBehaviour {
	public Tilemap resourcesMap;
	public GameObject resourcePrefab;

	void Start() {
		Units resources = GetComponent<GameData>().GetResources();
		CreateResourcesPrefabs(resources);
	}

	private void CreateResourcesPrefabs(Units units) {
		foreach (var position in resourcesMap.cellBounds.allPositionsWithin) {
			TileBase tile = resourcesMap.GetTile(position);
			if (tile != null) {
				Unit unit = Units.FindUnit(units.resources, tile.name);
						
				if (unit != null) {
					GameObject resourceInstance = Instantiate(resourcePrefab);
					Details details = resourceInstance.GetComponent<Details>();
					details.SetPosition(position);
					details.SetUnit(unit);
				}
			}
		}
	}
}
using UnityEngine;
using UnityEngine.Tilemaps;

public class InjectResources : MonoBehaviour {
	public Tilemap resourcesMap;
	public GameObject resourcePrefab;
	public GameObject injectedResources;

	void Start() {
		Units resources = GameData.GetUnitByType(ModelType.Resources);
		CreateResourcesPrefabs(resources);
	}

	private void CreateResourcesPrefabs(Units units) {
		foreach (var position in resourcesMap.cellBounds.allPositionsWithin) {
			TileBase tile = resourcesMap.GetTile(position);
			if (tile != null) {
				Unit unit = Units.FindUnit(units.resources, tile.name);
				if (unit != null) {
					GameObject resourceInstance = Instantiate(resourcePrefab);
					resourceInstance.transform.SetParent(injectedResources.transform);
					Details details = resourceInstance.GetComponent<Details>();
					details.SetUnit(unit);
					details.SetPosition(position);
				}
			}
		}
	}
	
	
}
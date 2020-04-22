using UnityEngine;
using UnityEngine.Tilemaps;

public class Building : MonoBehaviour {
	public RectInt buildingMapBounds;
	public Tilemap buildingTileMap;
	
	public void SetInitialValues(RectInt mapBounds, Tilemap tileMap, Unit unit) {
		buildingMapBounds = mapBounds;
		buildingTileMap = tileMap;

		Economy.DecreaseCoins(unit.price);
		Details details = GetComponent<Details>();
		details.SetUnit(unit);
		details.SetDimension(mapBounds);
	}
}
using UnityEngine;
using UnityEngine.Tilemaps;

public class Building : MonoBehaviour {
	private RectInt buildingMapBounds;

	private int totalResources;
	private int resourcesLeft;
	
	public bool isProducing = false;

	private int productionSize = 1;
	private float productionTime = 0;
	private float productionSpeed = 2;

	private const int MaxProductionSize = 3;

	private void Update() {
		if (!isProducing) return;
		productionTime += Time.deltaTime;

		if (productionTime >= productionSpeed && resourcesLeft > 0) {
			FinalizeProduction();
			productionTime = 0;
		}
	}

	private void FinalizeProduction() {
		ReduceResources();
		ReduceCoins();
		AddToWarehouse();
	}

	private void ReduceCoins() {
		Details details = GetComponent<Details>();
		Economy.DecreaseCoins(details.unit.price);
	}

	private void AddToWarehouse() {
		
	}

	private void ReduceResources() {
		resourcesLeft -= productionSize;
	}

	private void OnIncreaseProductionSize() {
		if (productionSize < MaxProductionSize) {
			productionSize += 1;
		}
	}
	
	public void OnToggleProduction() {
		isProducing = !isProducing;
	}

	private void CalculateTotalResources(Tilemap tileMap) {
		int resources = 0;
		foreach (var boundCell in buildingMapBounds.allPositionsWithin) {
			Details details = TileHelper.GetResourceDetailsFromMap((Vector3Int) boundCell, tileMap);
			if (details) {
				totalResources += details.unit.count;
			}
			GameStore.SetBuildingPosition(boundCell);
		}
		totalResources = resources;
		resourcesLeft = resources;
	}

	public void SetInitialValues(RectInt mapBounds, Tilemap tileMap, Unit unit) {
		buildingMapBounds = mapBounds;
		
		
		Economy.DecreaseCoins(unit.price);
		Details details = GetComponent<Details>();
		details.SetUnit(unit);
		details.SetDimension(mapBounds);
		CalculateTotalResources(tileMap);
	}
}
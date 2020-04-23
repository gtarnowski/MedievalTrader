using System;
using System.Collections.Generic;
using UnityEngine;

public class Mines : MonoBehaviour {
	public bool isWorking = false;

	private int productionSize = 1;
	private float productionTime = 0;
	private float productionSpeed = 2;

	private const int MaxProductionSize = 3;
	public List<MineResource> totalResources = new List<MineResource>();

	public Unit selectedOption;
	public MineResource resource;
	private void Start() {
		CalculateTotalResources();
	}

	private void Update() {
		if (!isWorking) return;
		productionTime += Time.deltaTime;

		if (productionTime >= productionSpeed && resource.count > 0) {
			FinalizeProduction();
			productionTime = 0;
		}
	}

	private void FinalizeProduction() {
		ReduceResources();
		ReduceCoins();
		
		WarehouseDistance warehouseDistance = Warehouses.FindClosestWarehouse(GetComponent<Details>().dimensions);
		print("Distance: " + warehouseDistance.distance);
	}
	
	private void ReduceCoins() {
		Economy.DecreaseCoins(selectedOption.price);
	}
	
	private void ReduceResources() {
		resource.count -= 1;
	}
	
	private void OnIncreaseProductionSize() {
		if (productionSize < MaxProductionSize) {
			productionSize += 1;
		}
	}
	
	public void OnConfirmProduction() {
		if (selectedOption == null) return;
		resource = totalResources.Find(res => res.id == selectedOption.id);
		// Find selected mine option in available resources and set as default

		if (resource != null) {
			// confirm production
			isWorking = true;
		}
	}

	private void CalculateTotalResources() {
		Building building = GetComponent<Building>();
		Details currentBuildingDetails = GetComponent<Details>();
		// Search for resources in building bounds/area
		foreach (var boundCell in building.buildingMapBounds.allPositionsWithin) {
			// Get details of each tile in bounds
			Details resourceDetails = TileHelper.GetResourceDetailsFromMap((Vector3Int) boundCell, building.buildingTileMap);
			// Check for any resources
			if (resourceDetails && resourceDetails.unit.buildingId == currentBuildingDetails.unit.id) {
				// Add specified resource to dictionary
				if (!totalResources.Exists(resource => resource.id == resourceDetails.unit.id)) {
					totalResources.Add(new MineResource {id = resourceDetails.unit.id, count = resourceDetails.unit.count});
				} else {
					// Pick resource by id and add resources count
					MineResource includedResource =
						totalResources.Find(resource => resource.id == resourceDetails.unit.id);
					includedResource.count += resourceDetails.unit.count;
				}
			}
		}
	}

	public void OnSetSelectedOption(Unit unit) => selectedOption = unit;
}

[Serializable]
public class MineResource {
	public int id;
	public int count;
}


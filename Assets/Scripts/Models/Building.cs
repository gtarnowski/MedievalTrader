using System;
using UnityEngine;

public class Building : MonoBehaviour {
	private RectInt buildingMapBounds;
	private Unit buildingUnit;

	private int totalResources;
	private int resourcesLeft;
	
	public bool isProducing = false;

	private int productionSize = 1;
	private float productionTime = 0;
	private float productionSpeed = 2;

	private const int MaxProductionSize = 3;

	private Unit currentProductionUnit;

	private void Start() {
		Economy.DecreaseCoins(buildingUnit.price);
	}

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
		Economy.DecreaseCoins(currentProductionUnit.price);
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

	public void SetInitialValues(RectInt mapBounds, Unit unit, int resources) {
		buildingMapBounds = mapBounds;
		buildingUnit = unit;

		totalResources = resources;
		resourcesLeft = resources;
	}
}
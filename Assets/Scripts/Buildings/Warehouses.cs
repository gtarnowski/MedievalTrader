using System;
using System.Collections.Generic;
using UnityEngine;

public class Warehouses : MonoBehaviour {
	public int range = 10;
	public List<WarehouseSlot> materialsSlots = new List<WarehouseSlot>();
	public List<WarehouseSlot> productsSlots = new List<WarehouseSlot>();
	
	private bool observe = false;
	private List<GameObject> counterButtons = new List<GameObject>();

	private static int distanceFromTargetToTarget = 12;
	private void Update() {
		if (!observe) return;

		RefreshCountersOnButtons();
	}

	public void OnChangeStorePreferences(Unit unit) {
		if (Utils.IsMaterial((unit.type))) {
			
		} else {
			
		}

	}

	public void AddToWarehouse(Unit unit) {
		AddToSlot(unit, Utils.IsMaterial(unit.type) ? materialsSlots : productsSlots);

		observe = true;
	}

	private void AddToSlot(Unit unit, List<WarehouseSlot> slots) {
		if (!IsInWarehouse(slots, unit)) {
			slots.Add(new WarehouseSlot { id = unit.id,  } );
		} else {
			WarehouseSlot warehouseSlot =
				slots.Find(slot => slot.id == unit.id);
			warehouseSlot.count += 1;
		}
	}
	
	public void GetFromWarehouse() {}

	public void AssignButtons(List<GameObject> buttons) {
		counterButtons = buttons;
	}
	
	private bool IsInWarehouse(List<WarehouseSlot> slots, Unit unit) {
		return slots.Exists(slot => slot.id == unit.id);
	}

	private void RefreshCountersOnButtons() {
		observe = false;
	}

	public static WarehouseDistance FindClosestWarehouse(RectInt target) {
		WarehouseDistance warehousesInRange = null;
		// loop on poaitions of target
		foreach(Vector2Int minePosition in target.allPositionsWithin) {

			// Loop on all warehouses located in a game
			foreach(GameObject warehouse in Helpers.GetWarehouses()) {
				RectInt warehouseDimensions = warehouse.GetComponent<Details>().dimensions;

				// loop on their positions
				foreach(var warehousePosition in warehouseDimensions.allPositionsWithin) {
					float distanceToWarehouse = Vector3.Distance((Vector3Int) warehousePosition, (Vector3Int)minePosition);
					// 10 tiles from target to target
					if (distanceToWarehouse <= distanceFromTargetToTarget) {
						// if there is any saved closest warahouse and the current, on the loop is closer to target
						// Then override varaibles
						if ((warehousesInRange != null && warehousesInRange.distance > distanceToWarehouse) || warehousesInRange == null) {
							warehousesInRange = new WarehouseDistance();
							warehousesInRange.warehouse = warehouse;
							warehousesInRange.distance = distanceToWarehouse;		
						}
					}
				}
			}	
		}
		return warehousesInRange;
	}
}

public class WarehouseSlot {
	public int id;
	public bool blocked;
	public int count;
}

public class WarehouseDistance {
	public GameObject warehouse;
	public float distance;
}
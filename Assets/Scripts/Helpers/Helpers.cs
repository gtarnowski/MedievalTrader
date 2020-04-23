using UnityEngine;
using System.Collections.Generic;


public class Helpers : MonoBehaviour {
	public static GameObject[] GetResources() {
		return GameObject.FindGameObjectsWithTag("Resource");
	}

	public static GameObject[] GetBuildings() {
		return GameObject.FindGameObjectsWithTag("Building");
	}

	public static List<GameObject> GetWarehouses() {
		GameObject[] buildings = Helpers.GetBuildings();
		List<GameObject> warehouses = new List<GameObject>();
		foreach(GameObject building in buildings) {
			
			if (Utils.IsWarehouse(building.GetComponent<Details>().unit.category)) {
				warehouses.Add(building);
			}
		}
		return warehouses;
	}
}
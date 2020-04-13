using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypes : MonoBehaviour {
	private Units buildings;
	[Header("General")]
	public GameObject buttonPrefab;
	public GameObject gridBuildMode;
	
	[Header("Layouts")]
	public GameObject minesLayout;
	public GameObject facilitiesLayout;
	public GameObject warehousesLayout;
	public GameObject transportationLayout;
	void Start() {
		// Get buildings from library, picked from JSON
		buildings = GameData.GetUnitByType(ModelType.Buildings);
		foreach (var building in buildings.buildings) {
			// Get Layout to inject button
			GameObject buildingTypeLayout = GetLayout(building.type);
			if (!buildingTypeLayout) return;

			// Clone button and insert to selected building type
			GameObject button = Instantiate(buttonPrefab, buildingTypeLayout.transform, true);
			
			button.GetComponent<Button>().onClick.AddListener(() =>
				// Set type to grid building mode
				gridBuildMode.GetComponent<GridBuildMode>().SetSelectedBuilding(building)
			);
		}
	}
	
	private GameObject GetLayout(string layoutName) {
		if (layoutName == minesLayout.name) return minesLayout;
		if (layoutName == facilitiesLayout.name) return facilitiesLayout;
		if (layoutName == warehousesLayout.name) return warehousesLayout;
		if (layoutName == transportationLayout.name) return transportationLayout;
		return null;
	}
}
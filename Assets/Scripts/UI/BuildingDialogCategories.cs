using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDialogCategories : MonoBehaviour {
	private Units buildings;
	[Header("General")]
	public GameObject buttonPrefab;
	public BuildingModeDialog buildingModeDialog;
	public GridBuildMode gridBuildMode;
	
	[Header("Layouts")]
	public GameObject minesLayout;
	public GameObject farmsLayout;
	public GameObject industryBuildingsLayout;
	public GameObject warehousesLayout;
	public GameObject stablesLayout;

	private Image currentButtonImage;
	private Color gray = new Color(
		(float) 0.4745098,
		(float)0.4745098,
		(float)0.4745098,
		(float) 0.2980392
	);

	private GameObject selectedCategory;
	
	void Start() {
		// Get buildings from library, picked from JSON
		buildings = GameData.GetUnitByType(ModelType.Buildings);
		
		foreach (var building in buildings.buildings) {
			// Get Layout to inject button
			GameObject buildingTypeLayout = GetLayout(building.category);
			// Clone button and insert to selected building type
			GameObject button = Instantiate(buttonPrefab, buildingTypeLayout.transform, true);
			button.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
			// buildingModeDialog.SelectFirstBuildingInCategory();
			gridBuildMode.SetSelectedBuilding(building);
			button.GetComponent<Button>().onClick.AddListener(() => {
				gridBuildMode.SetSelectedBuilding(building);
				gridBuildMode.ResetBuildBound();
				gridBuildMode.RefreshBuildBound();
				
				buildingModeDialog.currentBuildingCategory.transform.GetChild(0).GetComponent<Image>().color = gray;
				if (currentButtonImage) {
					currentButtonImage.color = gray;
				}
				
				currentButtonImage = button.GetComponent<Image>();
				currentButtonImage.color = Color.red;
			});
		}
	}

	private void Update() {
		print(selectedCategory.name);
	}

	private GameObject GetLayout(string layoutName) {
		if (layoutName == minesLayout.name) return minesLayout;
		if (layoutName == farmsLayout.name) return farmsLayout;
		if (layoutName == industryBuildingsLayout.name) return industryBuildingsLayout;
		if (layoutName == warehousesLayout.name) return warehousesLayout;
		if (layoutName == stablesLayout.name) return stablesLayout;
		return null;
	}

	public void OnSelectCategory(GameObject category) {
		selectedCategory = category;
	}
}
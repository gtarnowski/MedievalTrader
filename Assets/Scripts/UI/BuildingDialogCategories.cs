using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDialogCategories : MonoBehaviour {
	private Units buildings;
	[Header("General")]
	public GameObject buttonPrefab;
	public BuildingModeDialog buildingModeDialog;
	public GridBuildMode gridBuildMode;
	public List<GameObject> buildingsPrefabs;
	
	[Header("Layouts")]
	public GameObject minesLayout;
	public GameObject farmsLayout;
	public GameObject industryBuildingsLayout;
	public GameObject warehousesLayout;
	public GameObject stablesLayout;

	private Image currentButtonImage;

	private GameObject selectedCategory;
	
	void Start() {
		// Get buildings from library, picked from JSON
		buildings = GameData.GetUnitByType(ModelType.Buildings);
		
		gridBuildMode.SetSelectedBuilding(buildings.buildings[0]);
		int index = 0;
		foreach (var building in buildings.buildings) {
			// Get Layout to inject button
			GameObject buildingTypeLayout = GetLayout(building.category);
			// Clone button and insert to selected building type
			GameObject button = Instantiate(buttonPrefab, buildingTypeLayout.transform, true);
			button.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
			// buildingModeDialog.SelectFirstBuildingInCategory();
			if (index == 0) {
				button.GetComponent<Image>().color = Color.red;
			}
			// Load icon sprite and assign to icon button
			Sprite buttonIconSprite = GameSprites.GetIconByName(building.spriteName);
			button.transform.GetChild(0).GetComponent<Image>().sprite = buttonIconSprite;

			button.GetComponent<Button>().onClick.AddListener(() => {
				gridBuildMode.SetSelectedBuilding(building);
				gridBuildMode.ResetBuildBound();
				gridBuildMode.RefreshBuildBound();
				gridBuildMode.buildingPrefab = buildingsPrefabs.Find((prefab) => prefab.name == building.category);
				buildingModeDialog.currentBuildingCategory.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
				if (currentButtonImage) {
					currentButtonImage.color = Color.gray;
				}
				
				currentButtonImage = button.GetComponent<Image>();
				currentButtonImage.color = Color.red;
			});
			index++;
		}
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
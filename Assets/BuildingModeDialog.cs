using UnityEngine;
using UnityEngine.UI;

public class BuildingModeDialog : MonoBehaviour {
	public GridBuildMode gridBuildMode;
	public GameObject currentBuildingCategory;
	public Image currentButtonImage;

	public BuildingDialogCategories buildingDialogCategories;
	private Units buildings;

	void Awake() {
		buildings = GameData.GetUnitByType(ModelType.Buildings);

		OnSetCategoryButtonImage(currentButtonImage);
		OnSetBuildingCategory(currentBuildingCategory);
	}

	public void OnSetCategoryButtonImage(Image buttonImage) {
		currentButtonImage.color = Color.white;
		currentButtonImage = buttonImage;
		currentButtonImage.color = Color.red;
	}

	public void OnSetBuildingCategory(GameObject buildingCategory) {
		currentBuildingCategory.SetActive(false);
		currentBuildingCategory = buildingCategory;
		currentBuildingCategory.SetActive(true);
		buildingDialogCategories.OnSelectCategory(buildingCategory);
	}
	
	public void SelectFirstBuildingInCategory() {
		int buildingIndex = 0;
		for (int i = 0; i < buildings.buildings.Length; i++) {
			if (currentBuildingCategory.name == buildings.buildings[i].category) {
				buildingIndex = i;
				break;
			}
		}
		gridBuildMode.SetSelectedBuilding(buildings.buildings[buildingIndex]);
		if (currentBuildingCategory.transform.childCount > 0) {
			Image image = currentBuildingCategory.transform.GetChild(0).GetComponent<Image>();
			image.color = Color.red;
		}
	}
}
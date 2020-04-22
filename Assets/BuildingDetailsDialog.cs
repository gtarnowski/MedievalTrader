using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDetailsDialog : MonoBehaviour {
	public GameObject optionsWithCategoriesGameObject;
	public GameObject optionsIconsGameObject;
	public GameObject productsIconsGameObject;
	public GameObject materialsIconsGameObject;
	
	public GameObject buttonPrefab;
	
	public List<Unit> options;
	
	private GameObject building;
	private Details details;
	public List<GameObject> buttons = new List<GameObject>();

	public void OnConfirmDialog() {
		GetComponent<ManageDialog>().OnToggleDialog(true);
		building.GetComponent<Mines>().OnConfirmProduction();
	}

	public void OnSetHoveredBuilding(GameObject gameObject) {
		foreach (var button in buttons) {
			Destroy(button);
		}
		buttons = new List<GameObject>();
		building = gameObject;
		SetInitialValues();
	}

	private void SetInitialValues() {
		details = building.GetComponent<Details>();
		GetComponent<ManageDialog>().dialogTitle.text = details.unit.name;
		
		if (details.unit.category == BuildingCategory.Mines.ToString()) {
			GetOptionsByCollection(GameData.GetUnitByType(ModelType.Materials).materials, false);
		} else if (details.unit.category == BuildingCategory.Warehouses.ToString()) {
			GetOptionsByCollection(GameData.GetUnitByType(ModelType.Products).products, true);
			GetOptionsByCollection(GameData.GetUnitByType(ModelType.Materials).materials, true);
		}

		RenderOptionIcons();
	}

	private void RenderOptionIcons() {
		foreach (var option in options) {
			GameObject button = Instantiate(
				buttonPrefab,
				GetObjectLayoutByBuildingCategory(details.unit.category, option),
				true
			);
			button.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
			buttons.Add(button);

			button.GetComponent<Button>().onClick.AddListener(() => {
				CallActionByBuildingCategory(details.unit.category, option);
			});
		}
	}


	private void GetOptionsByCollection(Unit[] collection, bool isWarehouse) {
		if (!isWarehouse) {
			options = new List<Unit>();
		}
		foreach (var collectionItem in collection) {
			if (collectionItem.buildingId == details.unit.id || isWarehouse) {
				options.Add(collectionItem);
			}
		}
	}

	private void CallActionByBuildingCategory(string category, Unit option) {
		if (category == BuildingCategory.Warehouses.ToString()) {
			Warehouses warehouses = building.GetComponent<Warehouses>();
			warehouses.OnChangeStorePreferences();
		}
		if (category == BuildingCategory.Mines.ToString()) {
			Mines mines = building.GetComponent<Mines>();
			mines.OnSetSelectedOption(option);
			return;
		}
		if (category == BuildingCategory.IndustryBuildings.ToString()) {
			return;
		}
		return;
	}

	private Transform GetObjectLayoutByBuildingCategory(string category, Unit option) {
		if (category == BuildingCategory.Warehouses.ToString()) {
			optionsWithCategoriesGameObject.SetActive(true);
			optionsIconsGameObject.SetActive(false);
			
			if (option.type == UnitType.Material.ToString()) {
				return materialsIconsGameObject.transform;
			}

			return productsIconsGameObject.transform;
		}
		if (category == BuildingCategory.Mines.ToString()) {
			optionsWithCategoriesGameObject.SetActive(false);
			optionsIconsGameObject.SetActive(true);
			return optionsIconsGameObject.transform;
		}

		return null;
	} 
}
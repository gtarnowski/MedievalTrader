using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VSCodeEditor;

public class BuildingDetailsDialog : MonoBehaviour {
	public GameObject optionsWithCategoriesGameObject;
	public GameObject optionsIconsGameObject;
	public GameObject productsIconsGameObject;
	public GameObject materialsIconsGameObject;
	
	public GameObject buttonPrefab;
	public GameObject counterButtonPrefab;
	
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
		
		options = new List<Unit>();
		if (Utils.IsMine(details.unit.category)) {
			GetOptionsByCollection(GameData.GetUnitByType(ModelType.Materials).materials, false);
		}

		if (Utils.IsWarehouse(details.unit.category)) {
			GetOptionsByCollection(GameData.GetUnitByType(ModelType.Products).products, true);
			GetOptionsByCollection(GameData.GetUnitByType(ModelType.Materials).materials, true);
		}

		RenderOptionIcons();
	}

	private void RenderOptionIcons() {
		foreach (var option in options) {
			GameObject button = Instantiate(
				details.unit.category == nameof(BuildingCategory.Warehouses) ? counterButtonPrefab : buttonPrefab,
				GetObjectLayoutByBuildingCategory(details.unit.category, option),
				true
			);
			button.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
			buttons.Add(button);

			button.GetComponent<Button>().onClick.AddListener(() => {
				CallActionByBuildingCategory(details.unit.category, option);
			});
		}

		if (Utils.IsWarehouse(details.unit.category)) {
			GetComponent<Warehouses>().AssignButtons(buttons);
		}
	}


	private void GetOptionsByCollection(Unit[] collection, bool isWarehouse) {
		foreach (var collectionItem in collection) {
			if (collectionItem.buildingId == details.unit.id || isWarehouse) {
				options.Add(collectionItem);
			}
		}
	}

	private void CallActionByBuildingCategory(string category, Unit option) {
		if (Utils.IsWarehouse(category)) {
			Warehouses warehouses = building.GetComponent<Warehouses>();
			warehouses.OnChangeStorePreferences(option);
			return;
		}
		
		Mines mines = building.GetComponent<Mines>();
		mines.OnSetSelectedOption(option);
	}

	private Transform GetObjectLayoutByBuildingCategory(string category, Unit option) {
		if (Utils.IsWarehouse((category))) {
			optionsWithCategoriesGameObject.SetActive(true);
			optionsIconsGameObject.SetActive(false);
			
			if (option.type == UnitType.Material.ToString()) {
				return materialsIconsGameObject.transform;
			}

			return productsIconsGameObject.transform;
		}
		
		optionsWithCategoriesGameObject.SetActive(false);
		optionsIconsGameObject.SetActive(true);
		return optionsIconsGameObject.transform;
	}
}
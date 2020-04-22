using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MapInspector : MonoBehaviour {
	public Tilemap resourcesTileMap;
	public Tilemap buildingsTileMap;
	
	[Header("Dialogs")]
	public ManageDialog manageDialogBuildingDetails;
	public BuildingDetailsDialog buildingDetailsDialog;
	
	private Details details;
	private bool isOverlapping;
	private GameObject hoveredGameObject; 
	
	void Update() {
		if (!GameStore.GetOpenedDialogType().Equals(DialogType.BuildingModeDialog)) {
			ShowTooltipOnResources();
		}

		if (Input.GetMouseButtonDown((int) MouseButton.LeftMouse) && hoveredGameObject) {
			OnOpenBuildingDetails();			
		}
	}

	private void OnOpenBuildingDetails() {
		Details hoveredBuildingDetails = hoveredGameObject.GetComponent<Details>();
		if (hoveredBuildingDetails.unit.type == UnitType.Building.ToString()) {
			if (!manageDialogBuildingDetails.IsDialogOpened()) {
				manageDialogBuildingDetails.OnToggleDialog(false);	
			}
			buildingDetailsDialog.OnSetHoveredBuilding(hoveredGameObject);
		} else {
			
		}
	}

	private void ShowTooltipOnResources() {
		Vector3Int resourcesCell = MouseMoveController.GetMousePositionOnTileMap(resourcesTileMap);
		Vector3Int buildingsCell = MouseMoveController.GetMousePositionOnTileMap(buildingsTileMap);
		
		// Start inspect from building (it's less of them)
		InspectTile(resourcesCell, Helpers.GetBuildings());

		if (!isOverlapping) {
			// If no result, go to resources then
			InspectTile(buildingsCell, Helpers.GetResources());
		}

		if (details && isOverlapping && !EventSystem.current.IsPointerOverGameObject()) {
			if (details.unit.type == UnitType.Building.ToString()) {
				ShowBuildingTooltip();
			} else {
				ShowResourceTooltip();
			}
		} else {
			Tooltip.OnHideTooltip();
		}
	}
	
	private void InspectTile(Vector3Int cell, GameObject[] collection) {
		// Clean properties on each new cell calculation
		details = null;
		hoveredGameObject = null;
		isOverlapping = false;
		
		// Search for gameObject in selected tile
		foreach (GameObject item in collection) {
			isOverlapping = item.GetComponent<Details>().IsOverlapping(
				new Vector3Int(cell.x, cell.y, 0)
			);
			if (isOverlapping) {
				details = item.GetComponent<Details>();
				hoveredGameObject = item;
				break;
			}
		}
	}

	private void ShowBuildingTooltip() {
		Tooltip.OnShowTooltip(details.unit.name);
	}

	private void ShowResourceTooltip() {
		Tooltip.OnShowTooltip(details.unit.name + ": " + details.unit.count);
	}
}
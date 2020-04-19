using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class MapInspector : MonoBehaviour {
	public Tilemap resourcesTileMap;
	public Tilemap buildingsTileMap;
	
	Details details;
	bool isOverlapping;
	
	void Update() {
		if (!GameStore.GetOpenedDialogType().Equals(DialogType.BuildingModeDialog)) {
			ShowTooltipOnResources();
		}
	}

	private void ShowTooltipOnResources() {
		
		Vector3Int resourcesCell = MouseMoveController.GetMousePositionOnTileMap(resourcesTileMap);
		Vector3Int buildingsCell = MouseMoveController.GetMousePositionOnTileMap(buildingsTileMap);
		
		InspectTile(resourcesCell, Helpers.GetBuildings());
		if (!isOverlapping) {
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
		details = null;
		isOverlapping = false;
		foreach (var item in collection) {
			isOverlapping = item.GetComponent<Details>().IsOverlapping(
				new Vector3Int(cell.x, cell.y, 0)
			);
			if (isOverlapping) {
				details = item.GetComponent<Details>();
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
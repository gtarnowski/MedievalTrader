using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class MapInspector : MonoBehaviour {
	public Tilemap resourcesTileMap;

	// Update is called once per frame
	void Update() {
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3Int currentCell = resourcesTileMap.WorldToCell(worldPosition);

		Details details = null;
		bool isOverlapping = false;

		foreach (var resource in Helpers.GetResources()) {
			isOverlapping = resource.GetComponent<Details>().IsOverlapping(
			new Vector3Int(currentCell.x, currentCell.y, 0)
			);
			if (isOverlapping) {
				details = resource.GetComponent<Details>();
				break;
			}
		}
		
		if (details && isOverlapping && !EventSystem.current.IsPointerOverGameObject()) {
			Tooltip.OnShowTooltip(details.unit.name + ": " + details.unit.quantity);
		} else {
			Tooltip.OnHideTooltip();
		}
	}
}
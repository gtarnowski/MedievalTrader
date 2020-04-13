using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridBuildMode : MonoBehaviour {
	public Tilemap hoverTileMap;
	public Tilemap resourcesMap;
	public Tile hoverTile;
	
	public int hoverSize = 1;
	public bool enableBuildMode;

	private Vector3Int currentCell;
	private Vector3Int previousCell;

	private RectInt currentArea;
	private RectInt previousArea;

	private GameObject[] resources;
	private Unit selectedBuilding;

	void Update() {
		if (!enableBuildMode) return;
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		previousCell = currentCell;
		currentCell = hoverTileMap.WorldToCell(worldPosition);

		previousArea = CreateRectangleArea(previousCell);
		currentArea = CreateRectangleArea(currentCell);

		foreach (Vector2Int previousAreaCell in previousArea.allPositionsWithin) {
			bool includes = false;
			foreach (Vector2Int currentAreaCell in currentArea.allPositionsWithin) {
				GetAreaInfo((Vector3Int) currentAreaCell);
				if (previousAreaCell == currentAreaCell) {
					includes = true;
					break;
				}
				
				hoverTileMap.SetTile((Vector3Int) currentAreaCell, hoverTile);
			}
			if (!includes) hoverTileMap.SetTile((Vector3Int) previousAreaCell, null);
		}
	}

	private RectInt CreateRectangleArea(Vector3Int cell) {
		return new RectInt(cell.x, cell.y, hoverSize, hoverSize);
	}

	public void ToggleBuildMode() {
		enableBuildMode = !enableBuildMode;
	}

	private void GetAreaInfo(Vector3Int position) {
		TileBase tileBase = resourcesMap.GetTile(position);
		if (tileBase) {
			foreach (GameObject resourceInstance in GetResources()) {
				Details details = resourceInstance.GetComponent<Details>();
				if (details.IsOverlapping(position)) {
					return;
				}
			}
		}
	}
	
	private GameObject[] GetResources() {
		return GameObject.FindGameObjectsWithTag("Resource");
	}

	public void SetSelectedBuilding(Unit building) {
		selectedBuilding = building;
	}
}
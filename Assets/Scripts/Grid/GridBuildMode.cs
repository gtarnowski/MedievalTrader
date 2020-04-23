using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


public class GridBuildMode : MonoBehaviour {
	private static GridBuildMode instance;
	
	public ManageDialog manageDialog;

	[Header("Tiles")]
	public Tilemap hoverTileMap;
	public Tilemap resourcesMap;
	public Tilemap buildingsTileMap;
	
	public Tile hoverTile;
	public Tile hoverTileDenied;
	public Tile currentBuildingTile;
	public List<Tile> buildingTiles;

	[Header("Settings")]
	public bool enableBuildMode;
	public GameObject buildingPrefab;
	public GameObject createdBuildings;

	private Vector3Int currentCell;
	private Vector3Int previousCell;

	private RectInt currentArea;
	private RectInt previousArea;
	private RectInt cell;

	private GameObject[] resources;
	private Unit selectedBuildingUnit;

	private bool canBuild = true;
	
	private void Start() => instance = this;

	void Update() {
		if (!enableBuildMode) return;
		RefreshBuildBound();

		if (Input.GetMouseButtonDown((int) MouseButton.LeftMouse)) {
			Build();
		}
	}

	private void Build() {
		if (!canBuild || manageDialog.IsPointerOverBuildingDialog()) return;
		if (!Economy.CanBuy(selectedBuildingUnit.price)) return;

		GameObject newBuildingObject = Instantiate(
			buildingPrefab,
			createdBuildings.transform,
			true
		);
		
		buildingsTileMap.SetTile(currentCell, currentBuildingTile);
		// GameStore.SetBuildingPosition(currentArea);
		newBuildingObject.GetComponent<Building>()
			.SetInitialValues(currentArea, resourcesMap, selectedBuildingUnit);

		foreach(var boundCell in currentArea.allPositionsWithin) {
			// Add building position/bound to list
			GameStore.SetBuildingPosition(boundCell);
		}
	}

	public void RefreshBuildBound() {
		// mark current position as old
		previousCell = currentCell;
		// set new position
		currentCell = MouseMoveController.GetMousePositionOnTileMap(hoverTileMap);
		
		// build rectangles
		previousArea = CreateRectangleArea(previousCell);
		currentArea = CreateRectangleArea(currentCell);
		
		// clear build flag
		canBuild = true;

		bool isInBuildingsPositions = false;
		foreach (Vector2Int previousAreaCell in previousArea.allPositionsWithin) {
			foreach (Vector2Int currentAreaCell in currentArea.allPositionsWithin) {
				isInBuildingsPositions = GameStore.GetAllBuildingsPositions().Contains(currentAreaCell);
				if (isInBuildingsPositions) {
					canBuild = false;
					break;
				}
			}

			hoverTileMap.SetTile((Vector3Int) previousAreaCell, null);
			if (isInBuildingsPositions || !Economy.CanBuy(selectedBuildingUnit.price)) {
				TileHelper.SetTiles(hoverTileMap, hoverTileDenied, currentArea);
			} else {
				hoverTileMap.SetTile(currentCell, currentBuildingTile);
			}
		}
	}
	
	public void ResetBuildBound() {
		foreach (Vector3Int position in hoverTileMap.cellBounds.allPositionsWithin) {
			hoverTileMap.SetTile(position, null);
		}
	}

	private RectInt CreateRectangleArea(Vector3Int cell) {
		int size = selectedBuildingUnit?.size ?? 1;
		return new RectInt(cell.x, cell.y, size, size);
	}
	
	public void SetSelectedBuilding(Unit building) {
		selectedBuildingUnit = building;
		foreach (var tile in buildingTiles) {
			if (tile.name == building.spriteName) {
				currentBuildingTile = tile;
			}
		}
	}

	public static void OnToggleBuildMode() {
		if (instance.enableBuildMode) {
			instance.ResetBuildBound();
		}
		instance.enableBuildMode = !instance.enableBuildMode;
	}
}
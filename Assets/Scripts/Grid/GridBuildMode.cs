using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


public class GridBuildMode : MonoBehaviour {
	private static GridBuildMode instance;
	
	public BuildingDialog buildingDialog;
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
	private Unit selectedBuilding;

	private bool canBuild = true;
	
	public List<Vector2Int> buildingsPositions = new List<Vector2Int>();
	
	private void Start() {
		instance = this;
	}

	void Update() {
		if (!enableBuildMode) return;
		RefreshBuildBound();

		if (Input.GetMouseButtonDown((int) MouseButton.LeftMouse)) {
			Build();
		}
	}

	private void Build() {
		if (!canBuild || buildingDialog.IsPointerOverBuildingDialog()) return;
		GameObject newBuildingObject = Instantiate(buildingPrefab, createdBuildings.transform, true);
		Building newBuilding = newBuildingObject.GetComponent<Building>();
		int resources = 0;
		foreach (Vector2Int currentAreaCell in currentArea.allPositionsWithin) {
			
			Details details = GetBoundsInfo((Vector3Int) currentAreaCell);
			if (details) {
				resources += details.unit.quantity;
			}
			buildingsPositions.Add(currentAreaCell);
		}
		buildingsTileMap.SetTile(currentCell, currentBuildingTile);
		newBuilding.SetInitialValues(currentArea, selectedBuilding, resources);
	}

	public void RefreshBuildBound() {
		// Get mouse position
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		// mark current position as old
		previousCell = currentCell;
		// set new position
		currentCell = hoverTileMap.WorldToCell(worldPosition);
		
		// build rectangles
		previousArea = CreateRectangleArea(previousCell);
		currentArea = CreateRectangleArea(currentCell);
		canBuild = true;
		foreach (Vector2Int previousAreaCell in previousArea.allPositionsWithin) {
			bool includes = false;
			foreach (Vector2Int currentAreaCell in currentArea.allPositionsWithin) {
				bool isInBuildingsPositions = buildingsPositions.Contains(currentAreaCell);
				if (isInBuildingsPositions) {
					canBuild = false;
				}
				if (previousAreaCell == currentAreaCell) {
					includes = true;
					if (isInBuildingsPositions) {
						hoverTileMap.SetTile((Vector3Int) currentAreaCell, hoverTileDenied);
					} else {
						hoverTileMap.SetTile((Vector3Int) currentAreaCell, hoverTile);
					}
					break;
				}

				if (isInBuildingsPositions) {
					hoverTileMap.SetTile((Vector3Int) currentAreaCell, hoverTileDenied);
				} else {
					hoverTileMap.SetTile((Vector3Int) currentAreaCell, hoverTile);
				}
			}
			if (!includes) hoverTileMap.SetTile((Vector3Int) previousAreaCell, null);
		}
	}
	
	public void ResetBuildBound() {
		foreach (var position in hoverTileMap.cellBounds.allPositionsWithin) {
			hoverTileMap.SetTile(position, null);
		}
	}

	private RectInt CreateRectangleArea(Vector3Int cell) {
		int size = selectedBuilding?.size ?? 1;
		return new RectInt(cell.x, cell.y, size, size);
	}

	private Details GetBoundsInfo(Vector3Int position) {
		TileBase tileBase = resourcesMap.GetTile(position);
		if (tileBase) {
			foreach (GameObject resourceInstance in GetResources()) {
				Details details = resourceInstance.GetComponent<Details>();
				if (details.IsOverlapping(position)) {
					return details;
				}
			}
		}
		return null;
	}
	
	private GameObject[] GetResources() {
		return GameObject.FindGameObjectsWithTag("Resource");
	}

	public void SetSelectedBuilding(Unit building) {
		selectedBuilding = building;
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
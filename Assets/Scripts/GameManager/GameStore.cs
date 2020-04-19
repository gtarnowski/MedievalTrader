using System.Collections.Generic;
using UnityEngine;

public class GameStore : MonoBehaviour {
	private static GameStore instance;
	private DialogType openedDialog;
	
	public List<Vector2Int> buildingsPositions = new List<Vector2Int>();
	public List<Vector2Int> resourcesPositions = new List<Vector2Int>();
	private void Awake() {
		instance = this;
	}

	public static void SetOpenedDialog(DialogType dialogType) {
		instance.openedDialog = dialogType;
	}

	public static DialogType GetOpenedDialogType() {
		return instance.openedDialog;
	}

	public static void SetBuildingPosition(Vector2Int position) {
		if (instance.buildingsPositions.Contains(position)) {
			// TODO: Implement remove here
			return;
		} 
		instance.buildingsPositions.Add(position);
	}
	
	public static List<Vector2Int> GetAllBuildingsPositions() {
		return instance.buildingsPositions;	
	}
	
	public static void SetResourcesPosition(Vector2Int position) {
		if (instance.resourcesPositions.Contains(position)) {
			// TODO: Implement remove here
			return;
		} 
		instance.resourcesPositions.Add(position);
	}
	
	public static List<Vector2Int> GetAllResourcesPositions() {
		return instance.resourcesPositions;	
	}
}
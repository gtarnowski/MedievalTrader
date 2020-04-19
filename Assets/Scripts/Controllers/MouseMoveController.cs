using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseMoveController : MonoBehaviour {
	public static MouseMoveController instance;
	

	private void Awake() {
		instance = this;
	}

	public static Vector3 GetMouseWorldPosition() {
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	public static Vector3Int GetMousePositionOnTileMap(Tilemap tileMap) {
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return tileMap.WorldToCell(worldPosition);
	}
}
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Details : MonoBehaviour {
	[Header("Tile Info")]
	public string id;
	public int tileX;
	public int tileY;

	[Header("Other")]
	public int quantityLeft;
	
	private int quantity;
	public Unit unit;

	public TileBase tile;

	public void Start() {
		id = Guid.NewGuid().ToString();
	}

	public void SetPosition(Vector3Int position) {
		tileX = position.x;
		tileY = position.y;
	}

	public void SetUnit(Unit unitInstance) {
		unit = unitInstance;
		quantity = unitInstance.quantity;
		quantityLeft = unitInstance.quantity;
	}

	public Vector3Int GetPosition() {
		return new Vector3Int(tileX, tileY, 0);
	}

	public bool IsOverlapping(Vector3Int position) {
		if (position.x == tileX && position.y == tileY) return true;
		return false;
	}
}
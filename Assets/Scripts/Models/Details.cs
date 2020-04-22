using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Details : MonoBehaviour {
	public string id;
	
	public RectInt dimensions;
	public Unit unit;
	public void Start() => id = Guid.NewGuid().ToString();
	public void SetUnit(Unit unitInstance) => unit = unitInstance;
	
	public void SetPosition(Vector3Int position) {
		if (unit != null) {
			dimensions = new RectInt(
				position.x,
				position.y,
				unit.size,
				unit.size
			);
		}
	}

	public void SetDimension(RectInt bounds) {
		if (!bounds.Equals(null)) {
			dimensions = bounds;
		}
	}

	public bool IsOverlapping(Vector3Int position) {
		bool isOverlappingWithAnyDimensions = false;
		foreach (var dimension in dimensions.allPositionsWithin) {
			if (position.x == dimension.x && position.y == dimension.y) {
				isOverlappingWithAnyDimensions = true;
			}
		}
		return isOverlappingWithAnyDimensions;
	}
	
}
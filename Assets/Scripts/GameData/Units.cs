using UnityEngine;

[System.Serializable]
public class Units {
	public Unit[] materials;
	public Unit[] products;
	public Unit[] resources;
	public Unit[] buildings;
	public Unit[] processes;

	public static Unit FindUnit(Unit[] units, string unitTileName) {
		foreach (var unit in units) {
			if (unit.spriteName == unitTileName) {
				return unit;
			}
		}
		return null;
	}
}
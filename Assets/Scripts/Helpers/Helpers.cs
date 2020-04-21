using UnityEngine;

public class Helpers : MonoBehaviour {
	public static GameObject[] GetResources() {
		return GameObject.FindGameObjectsWithTag("Resource");
	}

	public static GameObject[] GetBuildings() {
		return GameObject.FindGameObjectsWithTag("Building");
	}
}
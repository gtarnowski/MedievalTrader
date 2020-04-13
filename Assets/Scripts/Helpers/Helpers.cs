using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpers : MonoBehaviour {
	public static GameObject[] GetResources() {
		return GameObject.FindGameObjectsWithTag("Resource");
	}
}
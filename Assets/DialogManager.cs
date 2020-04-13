using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogManager : MonoBehaviour {
	public GameObject buildingDialog;
	
	
	public void OnOpenBuildingDialog() {
		if (buildingDialog.activeSelf) return;
		buildingDialog.SetActive(true);
	}
}
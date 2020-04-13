using UnityEngine;

public class GameManager : MonoBehaviour {
	public DialogManager dialogManager;
	void Update() {
		if (Input.GetKey(KeyCode.Escape)) {
			GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().SetDisableController();
			dialogManager.buildingDialog.GetComponent<BuildingDialog>().OnCloseDialog();
		}

		if (Input.GetMouseButtonDown(1)) {
			dialogManager.OnOpenBuildingDialog();
		}
	}
	private void OnGUI() {
		if (Event.current.type == EventType.MouseDown) {
			GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().SetDisableController();
		}
	}
}
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
	public GameObject mainCamera;
	public BuildingDialog buildingDialog;
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			mainCamera.GetComponent<CameraController>().SetDisableController();
			buildingDialog.OnToggleDialog(true);
		}

		if (Input.GetMouseButtonDown((int) MouseButton.RightMouse)) {
			buildingDialog.OnToggleDialog(false);
		}
	}
	private void OnGUI() {
		if (Event.current.type == EventType.MouseDown) {
			mainCamera.GetComponent<CameraController>().SetDisableController();
		}
	}
}
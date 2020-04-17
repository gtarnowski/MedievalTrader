using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
	public GameObject mainCamera;
	public ManageDialog manageDialog;
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			mainCamera.GetComponent<CameraController>().SetDisableController();
			manageDialog.OnToggleDialog(true);
		}

		if (Input.GetMouseButtonDown((int) MouseButton.RightMouse) || Input.GetKeyDown(KeyCode.B)) {
			manageDialog.OnToggleDialog(false);
		}
	}
	private void OnGUI() {
		if (Event.current.type == EventType.MouseDown) {
			mainCamera.GetComponent<CameraController>().SetDisableController();
		}
	}
}
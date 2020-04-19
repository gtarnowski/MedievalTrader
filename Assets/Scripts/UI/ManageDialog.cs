using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManageDialog : MonoBehaviour {
	public TextMeshProUGUI dialogTitle;
	public GameObject defaultDialog;
	public DialogType dialogType;

	private GameObject currentDialog;
	private bool isPointerOverGameObject;

	private void Awake() {
		OnSetCurrentDialog(defaultDialog);
	}

	private void Update() {
		isPointerOverGameObject = EventSystem.current.IsPointerOverGameObject();
	}

	public void OnSetCurrentDialog(GameObject dialog) {
		if (dialog == currentDialog) return;
		dialog.SetActive(true);
		if (currentDialog) {
			currentDialog.SetActive(false);
		}

		currentDialog = dialog;
	}

	// forceClose allows to skip opening dialog and connected action.
	// Dialog can be closed only.
	public void OnToggleDialog(bool forceClose) {
		bool isBuildingModeDialog = dialogType.Equals(DialogType.BuildingModeDialog);
		GameStore.SetOpenedDialog(dialogType);
		Tooltip.OnHideTooltip();
		// hide dialog
		if (gameObject.activeSelf) {
			GameStore.SetOpenedDialog(DialogType.Hidden);
			gameObject.SetActive(false);

			// Toggle off build mode
			if (isBuildingModeDialog) {
				GridBuildMode.OnToggleBuildMode();
			}

			return;
		}

		if (!forceClose) {
			if (isBuildingModeDialog) GridBuildMode.OnToggleBuildMode();
			gameObject.SetActive(true);
		}
	}

	public bool IsPointerOverBuildingDialog() {
		return isPointerOverGameObject;
	}
}
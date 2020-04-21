using UnityEngine;
using UnityEngine.EventSystems;

public class ManageDialog : MonoBehaviour {
	public DialogType dialogType;
	private bool isPointerOverGameObject;

	// private void Awake() => OnSetBuildingCategory(defaultDialog);

	private void Update() {
		isPointerOverGameObject = EventSystem.current.IsPointerOverGameObject();
	}

	// public void OnSetBuildingCategory(GameObject dialog) {
	// 	defaultDialog.SetActive(false);
	// 	GameObject.Find("MinesButton").GetComponent<Image>().color = Color.white;
	// 	
	// 	if (buildingCategoryDialog) {
	// 		buildingCategoryDialog.SetActive(false);
	// 	}
	// 	dialog.SetActive(true);
	//
	// 	buildingCategoryDialog = dialog;
	// }

	// public void OnSetSelectedCategoryButton(Image buttonImage) {
	// 	if (currentButtonImage) {
	// 		currentButtonImage.color = Color.white;
	// 	}
	// 	currentButtonImage = buttonImage;
	// 	currentButtonImage.color = Color.red;
	// }

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

	public bool IsPointerOverBuildingDialog() => isPointerOverGameObject;
}
﻿using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManageDialog : MonoBehaviour {
	public TextMeshProUGUI dialogTitle;
	public DialogType dialogType;
	private bool isPointerOverGameObject;

	private void Update() {
		isPointerOverGameObject = EventSystem.current.IsPointerOverGameObject();
	}
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

	public bool IsDialogOpened() => gameObject.activeSelf;
}
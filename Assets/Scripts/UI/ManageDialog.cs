using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManageDialog : MonoBehaviour {
	public TextMeshProUGUI dialogTitle;
	public GameObject defaultDialog;
	
	private GameObject currentDialog;

	private float offsetX;
	private float offsetY;
	private Vector3 startPosition;

	private bool isPointerOverGameObject;
	private void Awake() {
		startPosition = GetComponent<RectTransform>().anchoredPosition;
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

	public void OnToggleDialog(bool forceClose) {
		// forceClose allows to skip opening dialog and connected action.
		// Dialog can be closed only.
		if (gameObject.activeSelf) {
			// hide dialog
			gameObject.SetActive(false);
			
			// Reset dialog position
			GetComponent<RectTransform>().anchoredPosition = startPosition;

			// Toggle off build mode
			GridBuildMode.OnToggleBuildMode();
			return;
		}

		if (!forceClose) {
			GridBuildMode.OnToggleBuildMode();
			gameObject.SetActive(true);	
		}
	}

	public void OnDragStart() {
		print("currently disabled");
		return;
		var position = transform.position;
		offsetX = position.x - Input.mousePosition.x;
		offsetY = position.y - Input.mousePosition.y;
	}

	public void OnDrag() {
		print("currently disabled");
		return;
		transform.position = new Vector3(
			offsetX + Input.mousePosition.x,
			offsetY + Input.mousePosition.y
		);
	}

	public bool IsPointerOverBuildingDialog() {
		return isPointerOverGameObject;
	}
}
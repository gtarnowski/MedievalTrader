using System;
using UnityEngine;

public class BuildingDialog : MonoBehaviour {
	private GameObject currentDialog;

	private float offsetX;
	private float offsetY;
	private Vector3 startPosition;

	private void Awake() {
		startPosition = GetComponent<RectTransform>().anchoredPosition;
	}

	public void OnSetCurrentDialog(GameObject dialog) { 
		if (dialog == currentDialog) return;
		dialog.SetActive(true);
		if (currentDialog) {
			currentDialog.SetActive(false);
		}
		currentDialog = dialog;
	}

	public void OnCloseDialog() {
		gameObject.SetActive(false);
		GetComponent<RectTransform>().anchoredPosition = startPosition;
	}

	public void OnDragStart() {
		offsetX = transform.position.x - Input.mousePosition.x;
		offsetY = transform.position.y - Input.mousePosition.y;
	}

	public void OnDrag() {
		transform.position = new Vector3(
			offsetX + Input.mousePosition.x,
			offsetY + Input.mousePosition.y
		);
	}
}
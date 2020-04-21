using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour {
	public Camera mainCamera;
	public float zoomMultiplier = 2f;
	public float maxZoom = 20f;
	public float minZoom = 2f;
	public float moveAmount = 10f;
	public float edgeSize = 10f;


	public bool disableController;
	public bool disableEdgeScroll = true;

	private float zoom = 5f;
	
	private float yPos;
	private float xPos;
	private Vector3 position;

	private void Start() {
		disableController = false;
		yPos = mainCamera.transform.position.y;
		xPos = mainCamera.transform.position.x;
		position = new Vector3(xPos, yPos, mainCamera.transform.position.z);
	}

	private void Update() {
		if (disableController) return;
		HandleEdgeScroll();
		HandleScroll();
		HandleKeyboardZoom();
		HandleWSADMove();

		Zoom();
	}

	private void HandleEdgeScroll() {
		if (disableEdgeScroll) return;
		if (Input.mousePosition.x > Screen.width - edgeSize) {
			MoveRight();
		}

		if (Input.mousePosition.x < edgeSize) {
			MoveLeft();
		}

		if (Input.mousePosition.y > Screen.height - edgeSize) {
			MoveUp();
		}

		if (Input.mousePosition.y < edgeSize) {
			MoveDown();
		}
	}

	private void HandleWSADMove() {
		if (Input.GetKey(KeyCode.W)) {
			MoveUp();
		}

		if (Input.GetKey(KeyCode.S)) {
			MoveDown();
		}

		if (Input.GetKey(KeyCode.A)) {
			MoveLeft();
		}

		if (Input.GetKey(KeyCode.D)) {
			MoveRight();
		}

		if (Input.GetKey(KeyCode.C)) {
			position = new Vector3(0, 0, mainCamera.transform.position.z);
			xPos = 0;
			xPos = 0;
		}

		mainCamera.transform.position = position;
	}

	private void MoveRight() {
		xPos += moveAmount * Time.deltaTime;
		position = new Vector3(xPos, yPos, transform.position.z);
	}

	private void MoveLeft() {
		xPos -= moveAmount * Time.deltaTime;
		position = new Vector3(xPos, yPos, transform.position.z);
	}

	private void MoveUp() {
		yPos += moveAmount * Time.deltaTime;
		position = new Vector3(xPos, yPos, transform.position.z);
	}

	private void MoveDown() {
		yPos -= moveAmount * Time.deltaTime;
		position = new Vector3(xPos, yPos, transform.position.z);
	}

	private void Zoom() {
		if (mainCamera.orthographicSize != zoom) {
			mainCamera.orthographicSize = zoom;
		}
	}

	private void HandleScroll() {
		if (Input.mouseScrollDelta.y > 0 && zoom >= minZoom) {
			zoom -= zoomMultiplier * Time.deltaTime * 10f;
		}

		if (Input.mouseScrollDelta.y < 0 && zoom <= maxZoom) {
			zoom += zoomMultiplier * Time.deltaTime * 10f;
		}
	}

	private void HandleKeyboardZoom() {
		if (Input.GetKey(KeyCode.KeypadPlus) && zoom >= minZoom) {
			zoom -= zoomMultiplier * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.KeypadMinus) && zoom <= maxZoom) {
			zoom += zoomMultiplier * Time.deltaTime;
		}
	}

	public void SetDisableController() {
		disableController = !disableController;
	}
}
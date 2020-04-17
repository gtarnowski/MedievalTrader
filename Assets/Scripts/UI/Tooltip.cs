using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

	private static Tooltip instance;
	private Camera uiCamera;

	public RectTransform backgroundRectTransform;
	public Text tooltipText;

	private void Awake() {
		instance = this;
		ShowTooltip("Random Text");
		HideTooltip();
	}

	private void Update() {
		Vector2 localPoint;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			transform.parent.GetComponent<RectTransform>(),
			Input.mousePosition,
			uiCamera, out localPoint
		);
		transform.localPosition = localPoint;
	}

	void ShowTooltip(string text) {
		gameObject.SetActive(true);

		tooltipText.text = text;
		float textPaddingSize = 4f;
		Vector2 backgroundSize = new Vector2(
			tooltipText.preferredWidth + textPaddingSize * 4f,
			tooltipText.preferredHeight + textPaddingSize * 2f
		);
		backgroundRectTransform.sizeDelta = backgroundSize;
	}

	void HideTooltip() {
		gameObject.SetActive(false);
	}

	public static void OnShowTooltip(string tooltipText) {
		instance.ShowTooltip(tooltipText);
	}

	public static void OnHideTooltip() {
		instance.HideTooltip();
	}
}

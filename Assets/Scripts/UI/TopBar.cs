using TMPro;
using UnityEngine;

public class TopBar : MonoBehaviour {
	public TextMeshProUGUI timeText;
	public TextMeshProUGUI coinsText;
	public TextMeshProUGUI alertsText;

	private void Update() {
		timeText.text = GameTime.GetFormattedDate();
		coinsText.text = Economy.GetCoinsValue().ToString();
		alertsText.text = "0";
	}
}
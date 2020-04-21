using UnityEngine;

public class Economy : MonoBehaviour {
	private static Economy instance;
	private int coins = 10000;

	private void Awake() {
		instance = this;
	}
	
	public static void DecreaseCoins(int decreaseValue) {
		instance.coins -= decreaseValue;
	}

	public static void IncreaseCoins(int increaseValue) {
		instance.coins += increaseValue;
	}

	public static int GetCoinsValue() {
		return instance.coins;
	}

	public static bool CanBuy(int cost) {
		return cost <= instance.coins;
	}
} 
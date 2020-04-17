using System;
using UnityEngine;

public class Economy : MonoBehaviour {
	public static Economy instance;
	private int coins = 10000;

	private void Awake() {
		instance = this;
	}

	private void Update() {
		print(instance.coins);
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
} 
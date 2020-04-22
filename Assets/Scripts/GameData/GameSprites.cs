using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSprites : MonoBehaviour {
	private static GameSprites instance;
	public List<Sprite> iconSprites;
	void Start() {
		instance = this;
	}
	public static Sprite GetIconByName(string iconName) =>
		instance.iconSprites.Find(icon => icon.name == iconName);
}
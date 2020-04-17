using System;
using UnityEngine;

public class GameTime : MonoBehaviour {
	public static GameTime instance;
	DateTime dateTime;

	// start date and time

	[Range(1, 4000)] public int year = 1;

	[Range(1, 12)] public int month = 1;

	[Range(1, 31)] public int day = 1;

	[Range(0, 23)] public int hour;

	[Range(0, 59)] public int minute;

	[Range(0, 59)] public int second;

	// scale of 1 is real-time
	[Range(-99999f, 99999f)] public float timeScale = 99999f;

	// Use this for initialization
	void Start() {
		instance = this;
		dateTime = new DateTime(year, 1, 1);
		//Subtract 1 month and 1 day, because we're adding to the dateTime
		//January is the 1st month, but if we add 1 month to dateTime, it will be February
		dateTime = dateTime.AddMonths(month - 1);
		dateTime = dateTime.Add(new TimeSpan(day - 1, hour, minute, second, 0));
	}

	// Update is called once per frame
	void Update() {
		dateTime = dateTime.AddSeconds(Time.deltaTime * Time.timeScale * timeScale);
	}

	public static DateTime GetDate() {
		return instance.dateTime;
	}

	public static string GetFormattedDate() {
		return instance.dateTime.ToString("dd MMMM yyyy");
	}

	public static int GetDayOfMonth() {
		return instance.dateTime.Day;
	}
}
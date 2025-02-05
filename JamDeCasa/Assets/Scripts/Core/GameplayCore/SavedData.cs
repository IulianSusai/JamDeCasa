﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedData
{

	private const string COHORT_NAME_KEY = "CohortName";
	private const string CURRENT_STARTS_KEY = "CurrentStars";
	private const string UNLOCKED_LEVEL_KEY = "UnlockedLevel";
	private const string LAST_LOADED_LEVEL_KEY = "LastLoadedLevel";

	private string cohortName;
	private int currentStars;
	private int unlockedLevel;
	private int lastLoadedLevel;

	public SavedData() {
		Load();
	}

	public string CohortName {
		set {
			cohortName = value;
			PlayerPrefs.SetString(COHORT_NAME_KEY, cohortName);
		}
		get {
			return cohortName;
		}
	}

	public int UnlockedLevel {
		set {
			unlockedLevel = value;
			PlayerPrefs.SetInt(UNLOCKED_LEVEL_KEY, unlockedLevel);
		}

		get {
			return unlockedLevel;
		}
	}

	public int LastPlayedLevel {
		set {
			lastLoadedLevel = value;
			PlayerPrefs.SetInt(LAST_LOADED_LEVEL_KEY, lastLoadedLevel);
		}
		get {
			return lastLoadedLevel;
		}
	}

	public int CurrentStars {
		set {
			currentStars = value;
			PlayerPrefs.SetInt(CURRENT_STARTS_KEY, currentStars);
		}
		get {
			return currentStars;
		}
	}

	public void SetLevelStars(int _level, int _stars) {
		PlayerPrefs.SetInt("Level" + _level, _stars);
	}

	public int GetLevelStars(int _level) {
		return GetSafeInt("Level" + _level);
	}


	private void Load() {
		cohortName = GetSafeString(COHORT_NAME_KEY);
		currentStars = GetSafeInt(CURRENT_STARTS_KEY);
		unlockedLevel = GetSafeInt(UNLOCKED_LEVEL_KEY);
		lastLoadedLevel = GetSafeInt(LAST_LOADED_LEVEL_KEY);
	}

	private float GetSafeFloat(string _key) {
		if (PlayerPrefs.HasKey(_key)) {
			return PlayerPrefs.GetFloat(_key);
		}
		return 0f;
	}

	private int GetSafeInt(string _key) {
		if (PlayerPrefs.HasKey(_key)) {
			return PlayerPrefs.GetInt(_key);
		}
		return 0;
	}

	private bool GetSafeBool(string _key) {
		if (PlayerPrefs.HasKey(_key)) {
			bool retVal = false;
			if (bool.TryParse(PlayerPrefs.GetString(_key), out retVal)) {
				return retVal;
			}
		}
		return false;
	}

	private string GetSafeString(string _key) {
		if (PlayerPrefs.HasKey(_key)) {
			return PlayerPrefs.GetString(_key);
		}
		return "";
	}

}

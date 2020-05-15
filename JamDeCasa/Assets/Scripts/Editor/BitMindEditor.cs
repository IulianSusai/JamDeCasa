using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BitMindEditor : MonoBehaviour
{
    
	[MenuItem("BitMind/Clear Saved Data")]
	public static void ClearSavedData() {
		PlayerPrefs.DeleteAll();
	}

	[MenuItem("BitMind/Settings/GameSettings")]
	public static void SelectGameSettings() {
		BMSettings settings = Resources.Load<BMSettings>("Settings/BMSettings");
		Selection.activeObject = settings;
	}

	[MenuItem("BitMind/Settings/LevelsSettings")]
	public static void SelectLevelsSettings() {
		BMLevels settings = Resources.Load<BMLevels>("Settings/BMLevels");
		Selection.activeObject = settings;
	}

}

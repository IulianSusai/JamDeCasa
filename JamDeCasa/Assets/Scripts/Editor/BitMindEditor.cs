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

}

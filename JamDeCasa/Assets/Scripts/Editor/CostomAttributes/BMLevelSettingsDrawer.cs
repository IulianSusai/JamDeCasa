using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(BMLevelAttribute))]
public class BMLevelSettingsDrawer : PropertyDrawer
{
	List<string> levelOptions = new List<string>();

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

		GetOptions();
		string levelName = property.stringValue;
		int selectedLeveIndex = 0;

		if(string.IsNullOrEmpty(levelName) && levelOptions.Contains(levelName)) {
			selectedLeveIndex = levelOptions.IndexOf(levelName);	
		}

		GUILayout.BeginHorizontal();
		GUILayout.Space(property.depth * 20f);
		selectedLeveIndex = EditorGUI.Popup(position, property.displayName, selectedLeveIndex, levelOptions.ToArray());
		property.stringValue = levelOptions[selectedLeveIndex];
		GUILayout.EndHorizontal();
	}

	void GetOptions() {
		levelOptions.Clear();
		if(levelOptions.Count <= 0) {
			foreach(LevelData level in BMCore.Levels.levelSettings) {
				levelOptions.Add(level.levelSettingsName);
			}
		}
	}
}

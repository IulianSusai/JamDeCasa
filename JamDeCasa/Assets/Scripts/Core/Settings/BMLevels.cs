using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BMSettings/BMLevels")]
public class BMLevels : ScriptableObject
{
	public List<LevelData> levelSettings;

	public LevelData GetLevelData(string _name) {
		for(int i = 0; i < levelSettings.Count; i++) {
			if(levelSettings[i].levelSettingsName == _name) {
				return levelSettings[i];
			}
		}
		Debug.LogError("Level settings " + _name + " not found!");
		return null;
	}

}

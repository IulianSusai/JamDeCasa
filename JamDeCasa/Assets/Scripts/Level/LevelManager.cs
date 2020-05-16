using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager
{
	public string DisplayLevel {
		get {
			return (levelIndex + 1).ToString();
		}
	}
	public Level currentLevel { private set; get; }
	private int levelIndex;

	public void LoadLevel() {
		Level levelPrefab = BMCore.Settings.levelData.GetLevel(levelIndex);
		if(levelPrefab != null) {
			if(currentLevel != null) {
				Object.Destroy(currentLevel.gameObject);
			}
			currentLevel = Object.Instantiate(levelPrefab);
		}
		ActionsController.Instance.SendOnLevelLoaded();
	}

	public void LoadNextLevel() {
		levelIndex++;
		LoadLevel();
	}

	public void LoadPrevLevel() {
		levelIndex = Mathf.Max(0, levelIndex - 1);
		LoadLevel();
	}

	public void SetupNextLevel() {
		levelIndex++;
	}

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData 
{
	public string levelSettingsName;
	public List<LevelChapter> chapters;

	private List<Level> unlockedLevels;

	public Level GetLevel(int _index) {
		if(unlockedLevels == null || unlockedLevels.Count == 0) {
			CreateUnlockedLevelsList();
		}
		return unlockedLevels[_index];
	}

	private void CreateUnlockedLevelsList() {
		unlockedLevels = new List<Level>();
		int currentStars = GameManager.Instance.savedData.CurrentStars;
		foreach(LevelChapter c in chapters) {
			if(currentStars >= c.unlockStars) {
				unlockedLevels.AddRange(c.chapterLevels);
			}
		}
	}

}

[Serializable]
public class LevelChapter
{
	public int unlockStars;
	public List<Level> chapterLevels;
}

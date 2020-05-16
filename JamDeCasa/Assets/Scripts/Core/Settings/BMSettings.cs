using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BMSettings/BMSettings")]
public class BMSettings : ScriptableObject
{
	public bool enableConsoleLogs;
	public bool enableFpsDisplay;
	[Range(0f, 1f)]
	public float gameSpeed;

	[HideInInspector] public BMCohortData cohort { private set; get; }
	[HideInInspector] public LevelData levelData { private set; get; }

	public void SetCohort(BMCohortData _cohort) {
		cohort = _cohort;
		levelData = BMCore.Levels.GetLevelData(cohort.core.levelSettingsName);
	}
}

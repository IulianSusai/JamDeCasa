using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

	public LevelManager levelManager { private set; get; }

	private void Awake() {
		if (Instance == null) {
			Instance = this;
			SetBMSettings();
			RegisterEvents();
		} else {
			Destroy(gameObject);
		}
	}

	private void Start() {
		SetBMCohort();
		SetReferences();
		UIManager.Instance.mainPage.OpenPage();
	}

	private void SetBMSettings() {
		Debug.unityLogger.logEnabled = BMCore.Settings.enableConsoleLogs;
		if (BMCore.Settings.enableFpsDisplay) {
			FPSDisplay fpsDisplay = gameObject.AddComponent<FPSDisplay>();
		}
		Time.timeScale = BMCore.Settings.gameSpeed;
	}

	private void SetBMCohort() {
		bool hasEmptyCohort = string.IsNullOrEmpty(UserData.Instance.savedData.CohortName);
		BMCohortData cohort = hasEmptyCohort ? BMCore.Cohorts.GetRandomAvailableCohort() : BMCore.Cohorts.GetCohort(UserData.Instance.savedData.CohortName);
		UserData.Instance.savedData.CohortName = cohort.cohortName;
		BMCore.Settings.SetCohort(cohort);
	}

	public void SetReferences() {
		levelManager = new LevelManager();
	}

	private void RegisterEvents() {
		ActionsController.Instance.onLevelWin += OnLevelWin;
		ActionsController.Instance.onLevelDie += OnLevelDie;
		ActionsController.Instance.onLevelStart += OnLevelStart;
	}

	public void LoadLevel() {
		levelManager.LoadLevel();
	}

	public void LoadNextLevel() {
		levelManager.LoadNextLevel();
	}


	public void LoadPrevLevel() {
		levelManager.LoadPrevLevel();
	}

	private void OnLevelStart() {
		UIManager.Instance.inGamePage.OpenPage();
	}

	private void OnLevelWin() {
		CheckLevelStars();
		levelManager.SetupNextLevel();
		Invoke("AfterLevelWinDelay", BMCore.Settings.cohort.gameplay.winPageDelay);
	}

	private void OnLevelDie() {
		Invoke("AfterLevelDieDelay", BMCore.Settings.cohort.gameplay.gameOverPageDelay);
	}

	private void AfterLevelWinDelay() {
		UIManager.Instance.winPage.OpenPage();
	}

	private void AfterLevelDieDelay() {
		UIManager.Instance.gameOverPage.OpenPage();
	}

	private void CheckLevelStars() {
		int currentLevel = levelManager.levelIndex;
		int currentStars = ConvertDiesToStars();
		int prevStars = UserData.Instance.savedData.GetLevelStars(currentLevel);
		if(currentStars > prevStars) {
			UserData.Instance.savedData.SetLevelStars(currentLevel, currentStars);
			int diff = currentStars - prevStars;
			UserData.Instance.savedData.CurrentStars += diff;
		}
		Debug.LogError("Stars: " + UserData.Instance.savedData.CurrentStars);
	}

	private int ConvertDiesToStars() {
		int currentDies = PlayerController.Instance.CurrentLevelDies;
		List<StarConditions> conditions = BMCore.Settings.cohort.gameplay.starConditions;
		foreach (StarConditions sc in conditions) {
			if (currentDies <= sc.maxDies) {
				return sc.stars;
			}
		}
		return 0;
	}

}

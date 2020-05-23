using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

	public LevelManager levelManager { private set; get; }
	public SavedData savedData { private set; get; }

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
		SetReferences();
		SetBMCohort();
		UIManager.Instance.MainPage.OpenPage();
	}

	private void SetBMSettings() {
		Debug.unityLogger.logEnabled = BMCore.Settings.enableConsoleLogs;
		if (BMCore.Settings.enableFpsDisplay) {
			FPSDisplay fpsDisplay = gameObject.AddComponent<FPSDisplay>();
		}
		Time.timeScale = BMCore.Settings.gameSpeed;
	}

	private void SetBMCohort() {
		bool hasEmptyCohort = string.IsNullOrEmpty(savedData.CohortName);
		BMCohortData cohort = hasEmptyCohort ? BMCore.Cohorts.GetRandomAvailableCohort() : BMCore.Cohorts.GetCohort(savedData.CohortName);
		savedData.CohortName = cohort.cohortName;
		BMCore.Settings.SetCohort(cohort);
	}

	public void SetReferences() {
		savedData = new SavedData();
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
		UIManager.Instance.InGamePage.OpenPage();
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
		UIManager.Instance.WinPage.OpenPage();
	}

	private void AfterLevelDieDelay() {
		UIManager.Instance.GameOverPage.OpenPage();
	}

	private void CheckLevelStars() {
		int currentLevel = levelManager.levelIndex;
		int currentStars = ConvertDiesToStars();
		int prevStars = savedData.GetLevelStars(currentLevel);
		if(currentStars > prevStars) {
			savedData.SetLevelStars(currentLevel, currentStars);
			int diff = currentStars - prevStars;
			savedData.CurrentStars += diff;
		}
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

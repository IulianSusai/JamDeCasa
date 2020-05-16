using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

	private void Awake() {
		if (Instance == null) {
			Instance = this;
			SetBMSettings();
			RegisterEvents();
		} else {
			Destroy(gameObject);
		}
	}

	[Header("Levels")]
	[SerializeField] private List<Level> levels;
	public Level currentLevel { private set; get; }
	public int currentLevelIndex { private set; get; }

	public int uiCurrentLevel {
		get {
			return currentLevelIndex % levels.Count;
		}
	}

	private void Start() {
		SetBMCohort();
		currentLevelIndex = 0;
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

	private void RegisterEvents() {
		ActionsController.Instance.onLevelWin += OnLevelWin;
		ActionsController.Instance.onLevelDie += OnLevelDie;
		ActionsController.Instance.onLevelStart += OnLevelStart;
	}

	public void LoadLevel() {
		if(currentLevel != null) {
			Destroy(currentLevel.gameObject);
		}
		currentLevel = Instantiate(levels[currentLevelIndex % levels.Count]);
		ActionsController.Instance.SendOnLevelLoaded();
	}

	public void LoadNextLevel() {
		currentLevelIndex++;
		LoadLevel();
	}


	public void LoadPrevLevel() {
		currentLevelIndex = Mathf.Max(0, currentLevelIndex - 1);
		LoadLevel();
	}

	private void OnLevelStart() {
		UIManager.Instance.inGamePage.OpenPage();
	}

	private void OnLevelWin() {
		currentLevelIndex++;
		Invoke("AfterLevelWinDelay", BMCore.Settings.cohort.gameplay.winPageDelay);
	}

	private void OnLevelDie() {
		currentLevelIndex = Mathf.Max(0, currentLevelIndex - 1);
		Invoke("AfterLevelDieDelay", BMCore.Settings.cohort.gameplay.gameOverPageDelay);
	}

	private void AfterLevelWinDelay() {
		UIManager.Instance.winPage.OpenPage();
	}

	private void AfterLevelDieDelay() {
		UIManager.Instance.gameOverPage.OpenPage();
	}
}

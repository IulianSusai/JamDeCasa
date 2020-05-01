using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

	private void Awake() {
		if(Instance == null) {
			Instance = this;
			RegisterEvents();
		} else {
			Destroy(gameObject);
		}
	}

	[Header("Design Info")]
	public float levelMaxTime;
	public float levelSpeedUpMultiplier;
	public float timeObjectPushForce = 10f;
	public float winPageDelay;
	public float gameOverPageDelay;

	public float shakeDuration = 1f;
	public float dieShakeAmount = 0.7f;
	public float dieDecreaseFactor = 1.0f;



	[Header("Levels")]
	[SerializeField] private List<Level> levels;
	public Level currentLevel { private set; get; }
	private int currentLevelIndex;

	private bool canContinue;


	private void Start() {
		currentLevelIndex = 0;
		UIManager.Instance.mainPage.OpenPage();
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
		canContinue = false;
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
		Invoke("AfterLevelWinDelay", winPageDelay);
	}

	private void OnLevelDie() {
		currentLevelIndex = Mathf.Max(0, currentLevelIndex - 1);
		Invoke("AfterLevelDieDelay", gameOverPageDelay);
	}

	private void AfterLevelWinDelay() {
		canContinue = true;
		UIManager.Instance.winPage.OpenPage();
	}

	private void AfterLevelDieDelay() {
		canContinue = true;
		UIManager.Instance.gameOverPage.OpenPage();
	}

	private void Update() {
		if(canContinue && Input.GetKeyDown(KeyCode.Space)) {
			UIManager.Instance.mainPage.OpenPage();
		} else if (!canContinue && PlayerController.Instance.state == PlayerState.Waiting) {
			if (Input.anyKeyDown) {
				ActionsController.Instance.SendOnLevelStart();
			}
		}
	}

}

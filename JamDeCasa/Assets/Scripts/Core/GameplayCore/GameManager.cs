﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

	private void Awake() {
		if(Instance == null) {
			Instance = this;
			SetBMSettings();
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

	public float explosionPower;
	public float explosionUpPower;
	public float explosionRadius;



	[Header("Levels")]
	[SerializeField] private List<Level> levels;
	public Level currentLevel { private set; get; }
	public int currentLevelIndex { private set; get; }

	public int uiCurrentLevel {
		get {
			return currentLevelIndex % levels.Count;
		}
	}

	private bool canContinue;

	private void Start() {
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InGamePage : MenuPage
{
	[SerializeField] private Animation speedUpAnim;
	[SerializeField] private Text levelTime;
	[SerializeField] private List<Image> speedUpImages;

	public override void OpenPage() {
		base.OpenPage();
		speedUpAnim.Stop();
		levelTime.rectTransform.localScale = Vector3.one;
		levelTime.color = Color.white;
		for(int i = 0; i < speedUpImages.Count; i++) {
			speedUpImages[i].gameObject.SetActive(false);
		}
	}

	protected override void RegisterEvents() {
		base.RegisterEvents();
		ActionsController.Instance.onPlayerDie += OnPlayerDie;
		ActionsController.Instance.onLevelDie += HideJoystick;
		ActionsController.Instance.onLevelWin += HideJoystick;
	}

	protected override void UnregisterEvents() {
		base.UnregisterEvents();
		ActionsController.Instance.onPlayerDie -= OnPlayerDie;
		ActionsController.Instance.onLevelDie -= HideJoystick;
		ActionsController.Instance.onLevelWin -= HideJoystick;
	}

	private void OnPlayerDie(GameObject _timeObj) {
		for (int i = 0; i < speedUpImages.Count; i++) {
			speedUpImages[i].gameObject.SetActive(true);
		}
		speedUpAnim.Play();
	}

	private void HideJoystick() {
		UIManager.Instance.SetJoystickActive(false);
	}


	private void Update() {
		if(PlayerController.Instance.state == PlayerState.Playing) {
			TimeSpan timeSpan = TimeSpan.FromSeconds(GameManager.Instance.currentLevel.LevelTime);
			levelTime.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
		}
	}



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePage : MenuPage
{
	[SerializeField] private Text levelTime;

	private void Update() {
		if(PlayerController.Instance.state == PlayerState.Playing) {
			levelTime.text = GameManager.Instance.currentLevel.LevelTime.ToString("N0");
		}
	}



}

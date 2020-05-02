using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainPage : MenuPage
{

	[SerializeField] private Text time;

	public override void OpenPage() {
		base.OpenPage();
		GameManager.Instance.LoadLevel();
		TimeSpan timeSpan = TimeSpan.FromSeconds(GameManager.Instance.currentLevel.LevelTime);
		time.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
	}

}

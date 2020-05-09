using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainPage : MenuPage
{

	[SerializeField] private Text time;

	public override void OpenPage() {
		base.OpenPage();
		GameManager.Instance.LoadLevel();
		TimeSpan timeSpan = TimeSpan.FromSeconds(GameManager.Instance.currentLevel.LevelTime);
		time.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
		//UIManager.Instance.SetJoystickActive(true);
	}

	private void Update() {
		if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
			ActionsController.Instance.SendOnLevelStart();
		}
	}

}

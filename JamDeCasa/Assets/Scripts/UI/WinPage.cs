using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WinPage : MenuPage
{

	public override void OpenPage() {
		base.OpenPage();
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
			UIManager.Instance.MainPage.OpenPage();
		}
	}

	public void NextLevel() {
		GameManager.Instance.LoadNextLevel();
		UIManager.Instance.MainPage.OpenPage();
	}

	public void Retry() {
		GameManager.Instance.LoadLevel();
		UIManager.Instance.MainPage.OpenPage();
	}

}

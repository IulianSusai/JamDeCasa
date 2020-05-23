using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WinPage : MenuPage
{

	[SerializeField] private Text winPageInfo;

	public override void OpenPage() {
		base.OpenPage();
		winPageInfo.text = "Nivelul " + GameManager.Instance.levelManager.DisplayLevel + " terminat!";
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
			UIManager.Instance.MainPage.OpenPage();
		}
	}

}

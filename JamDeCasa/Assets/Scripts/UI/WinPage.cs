using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPage : MenuPage
{

	[SerializeField] private Text winPageInfo;

	public override void OpenPage() {
		base.OpenPage();
		winPageInfo.text = "Nivelul " + (GameManager.Instance.uiCurrentLevel).ToString() + " terminat!";
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPage : MenuPage
{

	public override void OpenPage() {
		base.OpenPage();
		GameManager.Instance.LoadLevel();
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { private set; get; }
	private void Awake() {
		if(Instance == null) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}
	}

	[HideInInspector] public MenuPage currentPage;
	public MenuPage mainPage;
	public MenuPage inGamePage;
	public MenuPage winPage;
	public MenuPage gameOverPage;

	public Joystick joystick;
	
	public void SetJoystickActive(bool _active) {
		joystick.gameObject.SetActive(_active);
		if (!_active) {
			joystick.ResetJoystick();
		}
	}

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsController
{
	
	private ActionsController() {}

	private static ActionsController instance;

	public static ActionsController Instance {
		get {
			if(instance == null) {
				instance = new ActionsController();
			}
			return instance;
		}
	}

	#region Game_Flow


	public Action onLevelLoaded;
	public Action onLevelStart;
	public Action onLevelDie;
	public Action onLevelWin;
	public Action<GameObject> onPlayerDie;


	public void SendOnLevelStart() {
		onLevelStart?.Invoke();
	}

	public void SendOnLevelLoaded() {
		onLevelLoaded?.Invoke();
	}

	public void SendOnLevelDie() {
		onLevelDie?.Invoke();
	}

	public void SendOnLevelWin() {
		onLevelWin?.Invoke();
	}

	public void SendOnPlayerDie(GameObject _timeObj) {
		onPlayerDie?.Invoke(_timeObj);
	}
	#endregion

	#region Input
	public Action<PlayerInputButtonType, bool> onInputButton;

	public void SendOnInputButton(PlayerInputButtonType _button, bool _pressed) {
		onInputButton?.Invoke(_button, _pressed);
	}

	#endregion

}

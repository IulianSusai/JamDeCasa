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


	public Action onLevelLoaded;
	public Action onLevelDie;
	public Action onLevelWin;
	public Action onPlayerDie;

	public void SendOnLevelLoaded() {
		onLevelLoaded?.Invoke();
	}

	public void SendOnLevelDie() {
		onLevelDie?.Invoke();
	}

	public void SendOnLevelWin() {
		onLevelWin?.Invoke();
	}

	public void SendOnPlayerDie() {
		onPlayerDie?.Invoke();
	}

}

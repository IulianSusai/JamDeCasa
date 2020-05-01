using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
	public static PlayerController Instance { private set; get; }
	private void Awake() {
		if(Instance == null) {
			Instance = this;
			RegisterEvents();
		} else {
			Destroy(gameObject);
		}
	}

	[SerializeField] private Character ch;

	private void RegisterEvents() {
		ActionsController.Instance.onLevelLoaded += OnLevelLoaded;
		ActionsController.Instance.onLevelWin += OnLevelWin;
		ActionsController.Instance.onLevelDie += OnLevelDie;
		ActionsController.Instance.onPlayerDie += OnPlayerDie;
	}

	private void OnLevelLoaded() {
		ch.transform.position = GameManager.Instance.currentLevel.playerStartPosition;
		ch.transform.eulerAngles = GameManager.Instance.currentLevel.playerStartRotation;
		ch.gameObject.SetActive(true);
	}

	private void OnLevelWin() {

	}

	private void OnLevelDie() {

	}

	private void OnPlayerDie() {

	}

	private void OnDestroy() {
		ActionsController.Instance.onLevelLoaded -= OnLevelLoaded;
		ActionsController.Instance.onLevelWin -= OnLevelWin;
		ActionsController.Instance.onLevelDie -= OnLevelDie;
		ActionsController.Instance.onPlayerDie -= OnPlayerDie;
	}

}

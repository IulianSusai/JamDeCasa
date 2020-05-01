using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

	private void Awake() {
		if(Instance == null) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}
	}

	[SerializeField] private List<Level> levels;

	public Level currentLevel { private set; get; }
	private int currentLevelIndex;

	private void Start() {
		currentLevelIndex = 0;
		LoadLevel();
	}

	public void LoadLevel() {
		if(currentLevel != null) {
			Destroy(currentLevel.gameObject);
		}
		currentLevel = Instantiate(levels[currentLevelIndex % levels.Count]);
		ActionsController.Instance.SendOnLevelLoaded();
	}

	public void LoadNextLevel() {
		currentLevelIndex++;
		LoadLevel();
	}


	public void LoadPrevLevel() {
		currentLevelIndex--;
		LoadLevel();
	}



}

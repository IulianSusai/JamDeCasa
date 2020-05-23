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
	[SerializeField] private MenuPage mainPagePrefab;
	[SerializeField] private MenuPage inGamePagePrefab;
	[SerializeField] private MenuPage winPagePrefab;
	[SerializeField] private MenuPage gameOverPagePrefab;
	[SerializeField] private MenuPage chapterPagePrefab;
	[SerializeField] private MenuPage levelSelectPagePrefab;

	private MenuPage mainPage;
	private MenuPage inGamePage;
	private MenuPage winPage;
	private MenuPage gameOverPage;
	private MenuPage chapterPage;
	private MenuPage levelSelectPage;

	public MenuPage MainPage {
		get {
			if(mainPage == null) {
				mainPage = Instantiate(mainPagePrefab, transform, false);
			}
			return mainPage;
		}
	}

	public MenuPage InGamePage {
		get {
			if(inGamePage == null) {
				inGamePage = Instantiate(inGamePagePrefab, transform, false);
			}
			return inGamePage;
		}
	}

	public MenuPage WinPage {
		get {
			if (winPage == null) {
				winPage = Instantiate(winPagePrefab, transform, false);
			}
			return winPage;
		}
	}

	public MenuPage GameOverPage {
		get {
			if (gameOverPage == null) {
				gameOverPage = Instantiate(gameOverPagePrefab, transform, false);
			}
			return gameOverPage;
		}
	}

	public MenuPage ChapterPage {
		get {
			if (chapterPage == null) {
				chapterPage = Instantiate(chapterPagePrefab, transform, false);
			}
			return chapterPage;
		}
	}

	public MenuPage LevelSelectPage {
		get {
			if (levelSelectPage == null) {
				levelSelectPage = Instantiate(levelSelectPagePrefab, transform, false);
			}
			return levelSelectPage;
		}
	}


}

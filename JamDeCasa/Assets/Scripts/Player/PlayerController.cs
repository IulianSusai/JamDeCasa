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
	[SerializeField] private Camera cam;
	[SerializeField] private GameObject timeObjectPrefab;
	[SerializeField] private Character chPrefab;

	[HideInInspector] public PlayerState state;

	private Character ch;

	public Transform ChTransform {
		get {
			return ch == null ? null : ch.transform;
		}
	}

	public Vector3 Forward { private set; get; }
	public Vector3 Right { private set; get; }

	private void Start() {
		Forward = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z);
		Forward = Vector3.Normalize(Forward);
		Right = Quaternion.Euler(new Vector3(0, 90, 0)) * Forward;
	}

	private void RegisterEvents() {
		ActionsController.Instance.onLevelLoaded += OnLevelLoaded;
		ActionsController.Instance.onLevelWin += OnLevelWin;
		ActionsController.Instance.onLevelDie += OnLevelDie;
		ActionsController.Instance.onPlayerDie += OnPlayerDie;
		ActionsController.Instance.onLevelStart += OnLevelStart;
	}

	private void OnLevelLoaded() {
		if(ch != null) {
			Destroy(ch.gameObject);
		}
		ch = Instantiate(chPrefab, transform);
		ch.transform.position = GameManager.Instance.currentLevel.playerStartPosition;
		ch.transform.eulerAngles = GameManager.Instance.currentLevel.playerStartRotation;
		ch.gameObject.SetActive(true);
		ch.SetTimeObject(Instantiate(timeObjectPrefab));
		state = PlayerState.Waiting;
	}

	private void OnLevelWin() {
		state = PlayerState.InFinishAnim;
		ch.OnLevelWin();
	}

	private void OnLevelDie() {
		state = PlayerState.Dead;
		ch.OnLevelDie();
	}

	private void OnLevelStart() {
		state = PlayerState.Playing;
	}

	private void OnPlayerDie(GameObject _timeObj) {
		ch.transform.position = GameManager.Instance.currentLevel.playerStartPosition;
		ch.transform.eulerAngles = GameManager.Instance.currentLevel.playerStartRotation;
	}

	private void OnDestroy() {
		ActionsController.Instance.onLevelLoaded -= OnLevelLoaded;
		ActionsController.Instance.onLevelWin -= OnLevelWin;
		ActionsController.Instance.onLevelDie -= OnLevelDie;
		ActionsController.Instance.onPlayerDie -= OnPlayerDie;
		ActionsController.Instance.onLevelStart -= OnLevelStart;
	}

}

public enum PlayerState
{
	Waiting,
	Playing,
	InFinishAnim,
	Dead
}

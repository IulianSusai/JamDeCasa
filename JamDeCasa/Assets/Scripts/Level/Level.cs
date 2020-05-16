using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	public Vector3 playerStartPosition;
	public Vector3 playerStartRotation;
	[SerializeField] private List<Transform> timeObjectSpawnPosition;

	public float LevelTime { private set; get; }

	private bool startLevel;
	private float timeMultiplier;

	private void Awake() {
		ActionsController.Instance.onLevelLoaded += OnLevelLoaded;
		ActionsController.Instance.onLevelStart += OnLevelStart;
		ActionsController.Instance.onPlayerDie += OnPlayerDie;
	}

	private void OnDestroy() {
		ActionsController.Instance.onLevelLoaded -= OnLevelLoaded;
		ActionsController.Instance.onLevelStart -= OnLevelStart;
		ActionsController.Instance.onPlayerDie -= OnPlayerDie;
	}

	private void OnLevelLoaded() {
		LevelTime = BMCore.Settings.cohort.gameplay.levelMaxTime;
	}

	private void OnLevelStart() {
		startLevel = true;
		timeMultiplier = 1;
	}

	private void OnPlayerDie(GameObject _timeObject) {
		if (_timeObject != null) {
			_timeObject.transform.SetParent(transform);
			_timeObject.transform.position = timeObjectSpawnPosition[Random.Range(0, timeObjectSpawnPosition.Count)].position;
		}
		timeMultiplier = BMCore.Settings.cohort.gameplay.levelSpeedUpMultiplier;
	}

	private void Update() {
		if(startLevel && PlayerController.Instance.state == PlayerState.Playing) {
			LevelTime -= Time.deltaTime * timeMultiplier;
			if(LevelTime <= 0f) {
				ActionsController.Instance.SendOnLevelDie();
			}
		}
	}

}

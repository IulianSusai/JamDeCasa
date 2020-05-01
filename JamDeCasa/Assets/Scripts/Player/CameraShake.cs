using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	private Vector3 originalPos;
	private bool inShake;

	private float shakeDuration;

	private void Awake() {
		originalPos = transform.localPosition;
		ActionsController.Instance.onPlayerDie += OnPlayerDie;
		ActionsController.Instance.onLevelDie += OnLevelDie;
	}

	private void OnPlayerDie(GameObject _timeObj) {
		inShake = true;
		shakeDuration = GameManager.Instance.shakeDuration;
	}

	private void OnLevelDie() {
		inShake = true;
		shakeDuration = GameManager.Instance.shakeDuration;
	}
	

	private void Update() {
		if (inShake) {
			if (shakeDuration > 0) {
				transform.localPosition = originalPos + Random.insideUnitSphere * GameManager.Instance.dieShakeAmount;
				shakeDuration -= Time.deltaTime * GameManager.Instance.dieDecreaseFactor;
			} else {
				shakeDuration = 0f;
				transform.localPosition = originalPos;
				inShake = false;
			}
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishObject : MonoBehaviour
{
	[SerializeField] private Transform timeObjectPosition;

	private Animator animator;

	private void Start() {

		animator = GetComponent <Animator>();

		ActionsController.Instance.onLevelDie += OnLevelDie;
		ActionsController.Instance.onLevelWin += OnLevelWin;
	}

	private void OnDestroy() {
		ActionsController.Instance.onLevelDie -= OnLevelDie;
		ActionsController.Instance.onLevelWin -= OnLevelWin;
	}

	public void SetTimeObject(GameObject _timeObj) {
		_timeObj.transform.SetParent(timeObjectPosition.transform);
		_timeObj.transform.localPosition = Vector3.zero;
		_timeObj.transform.localRotation = Quaternion.identity;
	}

	private void OnLevelDie() {
		animator.SetTrigger("LevelDie");
	}

	private void OnLevelWin() {
		animator.SetTrigger("Win");
	}

	private void Update() {
		if (PlayerController.Instance.ChTransform != null && (PlayerController.Instance.state == PlayerState.Playing || PlayerController.Instance.state == PlayerState.Waiting)) {
			transform.LookAt(PlayerController.Instance.ChTransform);
		}
	}

	

}

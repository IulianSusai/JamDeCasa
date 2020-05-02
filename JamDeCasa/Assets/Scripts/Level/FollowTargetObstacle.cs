using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetObstacle : MonoBehaviour
{
	[SerializeField] private float moveSpeed;
	private Transform target;
	private Vector3 startPosition;
	private FollowTargetState state;


	private void Awake() {
		startPosition = transform.position;
		ActionsController.Instance.onLevelStart += OnLevelStart;
		ActionsController.Instance.onPlayerDie += OnPlayeDie;
	}

	private void OnDestroy() {
		ActionsController.Instance.onLevelStart -= OnLevelStart;
		ActionsController.Instance.onPlayerDie -= OnPlayeDie;
	}

	private void OnLevelStart() {
		target = PlayerController.Instance.ChTransform;
		state = FollowTargetState.Follow;
	}

	private void OnPlayeDie(GameObject _timeObj) {
		state = FollowTargetState.Return;
	}

	private void Update() {
		if (state == FollowTargetState.Follow) {
			if (target != null && PlayerController.Instance.state == PlayerState.Playing) {
				Vector3 diff = target.position - transform.position;
				Vector3 dir = new Vector3(diff.x, 0f, diff.z).normalized;
				transform.Translate(dir * moveSpeed * Time.deltaTime);
			}
		} else {
			transform.position = Vector3.MoveTowards(transform.position, startPosition, 2 * moveSpeed * Time.deltaTime);
			if(transform.position == startPosition) {
				state = FollowTargetState.Follow;
			}
		}
	}

	private enum FollowTargetState
	{
		Follow,
		Return
	}
}

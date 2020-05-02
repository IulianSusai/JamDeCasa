using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPointsObstacle : MonoBehaviour
{
	[SerializeField] private float moveStartDelay;
	[SerializeField] private float moveToPointTime;
	[SerializeField] private float moveToNextPointDelay;
	[SerializeField] private AnimationCurve moveToPointCurve;
	[SerializeField] private GameObject movingObject;
	[SerializeField] private List<GameObject> points;

	private bool isMoving;
	private float moveStartTime;
	private int currentPointIndex;

	private Vector3 startPosition;
	private Vector3 endPosition;

	private void Start() {
		currentPointIndex = 0;
		movingObject.transform.position = points[0].transform.position;
		movingObject.transform.LookAt(points[1].transform.position);
	}

	private void Awake() {
		ActionsController.Instance.onLevelStart += OnLevelStarted;
		ActionsController.Instance.onLevelDie += OnLevelDie;
		ActionsController.Instance.onLevelWin += OnLevelWin;
	}

	private void OnDestroy() {
		ActionsController.Instance.onLevelStart -= OnLevelStarted;
		ActionsController.Instance.onLevelDie -= OnLevelDie;
		ActionsController.Instance.onLevelWin -= OnLevelWin;
	}

	private void OnLevelStarted() {
		Invoke("StartMoving", moveStartDelay);
	}

	private void OnLevelDie() {
		isMoving = false;
		CancelInvoke();
	}

	private void OnLevelWin() {
		isMoving = false;
		CancelInvoke();
	}


	private void StartMoving() {
		currentPointIndex++;
		GameObject nextPoint = points[currentPointIndex % points.Count];
		isMoving = true;
		moveStartTime = Time.time;
		startPosition = movingObject.transform.position;
		endPosition = nextPoint.transform.position;
		movingObject.transform.LookAt(nextPoint.transform);
	}

	private void Update() {
		if (isMoving) {
			UpdateMovement();
		}
	}


	private void UpdateMovement() {
		float timeSinceStart = Time.time - moveStartTime;
		float percentage = timeSinceStart / moveToPointTime;
		float curve = moveToPointCurve.Evaluate(percentage);
		movingObject.transform.position = Vector3.Lerp(startPosition, endPosition, percentage);

		if(percentage >= 1.0f) {
			isMoving = false;
			Invoke("StartMoving", moveToNextPointDelay);
		}

	}

}

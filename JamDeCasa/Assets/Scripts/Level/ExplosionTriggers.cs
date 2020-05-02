using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTriggers : MonoBehaviour
{
	[SerializeField] private float rotationSpeed;
	private TriggerExplosionObstacle teo;

	private bool didStart;

	private void Awake() {
		teo = GetComponentInParent<TriggerExplosionObstacle>();
		ActionsController.Instance.onLevelStart += OnLevelStart;
	}

	private void OnDestroy() {
		ActionsController.Instance.onLevelStart -= OnLevelStart;
	}

	private void OnLevelStart() {
		didStart = true;
	}
	
	public void Explode() {
		teo.Explode();
	}


	private void Update() {
		if(PlayerController.Instance.state == PlayerState.Playing) {
			transform.parent.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
		}
	}
}

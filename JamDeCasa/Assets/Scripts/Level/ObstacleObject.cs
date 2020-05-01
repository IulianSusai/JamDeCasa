using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : MonoBehaviour
{

	private Animation anim;

	private void Awake() {
		anim = GetComponent<Animation>();
		ActionsController.Instance.onLevelStart += OnLevelStart;
		ActionsController.Instance.onLevelWin += OnLevelWin;
		ActionsController.Instance.onLevelDie += OnLevelDie;
	}

	private void OnDestroy() {
		ActionsController.Instance.onLevelStart -= OnLevelStart;
		ActionsController.Instance.onLevelWin -= OnLevelWin;
		ActionsController.Instance.onLevelDie -= OnLevelDie;
	}

	private void OnLevelStart() {
		if(anim != null) {
			anim[anim.clip.name].time = 0f;
			anim.Play();
		}
	}

	private void OnLevelWin() {
		if(anim != null) {
			anim.Stop();
		}
	}

	private void OnLevelDie() {
		if (anim != null) {
			anim.Stop();
		}
	}
}

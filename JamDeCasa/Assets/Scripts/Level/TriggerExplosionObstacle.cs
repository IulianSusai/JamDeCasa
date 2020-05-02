using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExplosionObstacle : MonoBehaviour
{
	[SerializeField] private Transform explosionPosition;
	[SerializeField] private List<Rigidbody> rbs;
	[SerializeField] private List<ExplosionTriggers> triggers;

	public void Explode() {
		for(int i = 0; i < rbs.Count; i++) {
			rbs[i].isKinematic = false;
			rbs[i].AddExplosionForce(GameManager.Instance.explosionPower, explosionPosition.position, GameManager.Instance.explosionRadius, GameManager.Instance.explosionUpPower, ForceMode.Impulse);
		}
		for(int i = 0; i < triggers.Count; i++) {
			Destroy(triggers[i].gameObject);
		}
	}

}

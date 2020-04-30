using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	[SerializeField] private float moveSpeed;
	private CharacterController cc;

	private void Awake() {
		cc = GetComponent<CharacterController>();
	}

	private void Update() {
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		Vector3 moveVector = new Vector3(vertical, 0f, -horizontal) * moveSpeed * Time.deltaTime; 
		cc.Move(moveVector);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	[SerializeField] private float moveSpeed;
	[SerializeField] private float turnSmoothTime = 0.2f;
	[SerializeField] private float speedSmoothTime = 0.1f;

	private float speedSmoothVelocity;
	private float turnSmoothVelocity;
	private float currentSpeed;

	private Animator animator;
	private Rigidbody rb;

	private void Awake() {
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
	}

	private void Update() {

		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector2 inputDir = input.normalized;

		if(inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
		}

		float targetSpeed = moveSpeed * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

		//transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

		float animationSpeedPercent = inputDir.magnitude;
		animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

	}

	private void FixedUpdate() {
		rb.MovePosition(rb.position + transform.forward * currentSpeed * Time.fixedDeltaTime);
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag("Obstacle")) {
			ActionsController.Instance.SendOnPlayerDie();
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Finish")) {

		}
	}

}

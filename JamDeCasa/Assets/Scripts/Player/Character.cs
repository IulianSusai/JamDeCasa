using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	[SerializeField] private Transform timeObjectContainer;
	[SerializeField] private float moveSpeed;
	[SerializeField] private float turnSmoothTime = 0.2f;
	[SerializeField] private float speedSmoothTime = 0.1f;

	private float speedSmoothVelocity;
	private float turnSmoothVelocity;
	private float currentSpeed;

	private Animator animator;
	private Rigidbody rb;

	private GameObject timeObject;

	public bool IsHolding {
		get {
			return timeObject != null;
		}
	}

	private void Awake() {
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
	}

	public void SetTimeObject(GameObject _timeObject) {
		timeObject = _timeObject;
		timeObject.transform.SetParent(timeObjectContainer);
		timeObject.transform.localEulerAngles = Vector3.zero;
		timeObject.transform.localPosition = Vector3.zero;
		animator.SetBool("IsHoldingCrate", true);
	}

	public void DropTimeObject() {
		timeObject = null;
		animator.SetBool("IsHoldingCrate", false);
	}

	public void OnLevelDie() {
		SetTimeObjectFree();
		animator.SetTrigger("LevelDie");
	}

	public void OnLevelWin() {
		animator.SetTrigger("Win");
	}

	private void SetTimeObjectFree() {
		if (timeObject != null) {
			timeObject.transform.SetParent(GameManager.Instance.currentLevel.transform);
			Rigidbody tRb = timeObject.AddComponent<Rigidbody>();
			tRb.AddForce(transform.forward * GameManager.Instance.timeObjectPushForce, ForceMode.Impulse);
		}
	}

	private void Update() {
		if(PlayerController.Instance.state == PlayerState.Playing) {
			Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			Vector2 inputDir = input.normalized;

			if (inputDir != Vector2.zero) {
				float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
				transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
			}

			float targetSpeed = moveSpeed * inputDir.magnitude;
			currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

			float animationSpeedPercent = inputDir.magnitude;
			animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
		}
	}

	private void FixedUpdate() {
		if (PlayerController.Instance.state == PlayerState.Playing) {
			rb.MovePosition(rb.position + transform.forward * currentSpeed * Time.fixedDeltaTime);
		}
	}

	private void OnCollisionEnter(Collision collision) {
		if (PlayerController.Instance.state == PlayerState.Playing) {
			if (collision.gameObject.CompareTag("Obstacle")) {
				rb.isKinematic = true;
				ActionsController.Instance.SendOnPlayerDie(timeObject);
				DropTimeObject();
				rb.isKinematic = false;
			} else if (!IsHolding && collision.gameObject.CompareTag("TimeDog")) {
				SetTimeObject(collision.gameObject);
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (PlayerController.Instance.state == PlayerState.Playing) {
			if (IsHolding && other.CompareTag("Finish")) {
				FinishObject fo = other.GetComponent<FinishObject>();
				fo.SetTimeObject(timeObject);
				ActionsController.Instance.SendOnLevelWin();
			}
			if (other.CompareTag("ExplodeTrigger")) {
				ExplosionTriggers et = other.GetComponent<ExplosionTriggers>();
				if (et != null) {
					et.Explode();
				}
			}
		}
	}

}

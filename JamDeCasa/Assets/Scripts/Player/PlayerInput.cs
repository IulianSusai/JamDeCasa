using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{

#if UNITY_EDITOR
	public float Horizontal { get { return Input.GetAxis("Horizontal"); } }
	public float Vertical { get { return Input.GetAxis("Vertical"); } }
#else
	public float Horizontal { private set; get; }
	public float Vertical { private set; get; }
#endif

	private PlayerInputButton leftButton;
	private PlayerInputButton rightButton;
	private PlayerInputButton upButton;
	private PlayerInputButton downButton;

	public PlayerInput() {
		leftButton = new PlayerInputButton(PlayerInputButtonType.Left);
		rightButton = new PlayerInputButton(PlayerInputButtonType.Right);
		upButton = new PlayerInputButton(PlayerInputButtonType.Up);
		downButton = new PlayerInputButton(PlayerInputButtonType.Down);
		ActivateInput();
	}

	public void ActivateInput() {
		Reset();
		ActionsController.Instance.onInputButton += OnInputButton;
	}
	public void DeactivateInput() {
		ActionsController.Instance.onInputButton -= OnInputButton;
	}

	public void Reset() {
#if !UNITY_EDITOR
		Horizontal = 0f;
		Vertical = 0f;
#endif
	}

	private void OnInputButton(PlayerInputButtonType _button, bool _pressed) {

		switch (_button) {
			case PlayerInputButtonType.Left:
				leftButton.pressed = _pressed;
				break;
			case PlayerInputButtonType.Right:
				rightButton.pressed = _pressed;
				break;
			case PlayerInputButtonType.Up:
				upButton.pressed = _pressed;
				break;
			case PlayerInputButtonType.Down:
				downButton.pressed = _pressed;
				break;
		}
#if !UNITY_EDITOR
		Horizontal = rightButton.pressed ? 1f : leftButton.pressed ? -1f : 0f;
		Vertical = upButton.pressed ? 1f : downButton.pressed ? -1f : 0f;
#endif

	}

	private class PlayerInputButton
	{
		public PlayerInputButtonType buttonType;
		public bool pressed;

		public PlayerInputButton(PlayerInputButtonType _buttonType) {
			buttonType = _buttonType;
		}

	}

}

public enum PlayerInputButtonType
{
	Left,
	Right,
	Up,
	Down
}

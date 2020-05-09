using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPlayerInputButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

	[SerializeField] private PlayerInputButtonType buttonType;

	public void OnPointerDown(PointerEventData eventData) {
		ActionsController.Instance.SendOnInputButton(buttonType, true);
	}

	public void OnPointerUp(PointerEventData eventData) {
		ActionsController.Instance.SendOnInputButton(buttonType, false);
	}
}

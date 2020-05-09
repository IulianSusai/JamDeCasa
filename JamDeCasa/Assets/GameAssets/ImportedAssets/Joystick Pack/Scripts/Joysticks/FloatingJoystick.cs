using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    public override void OnPointerDown(PointerEventData eventData)
    {
		if (PlayerController.Instance.state == PlayerState.Waiting) {
			ActionsController.Instance.SendOnLevelStart();
		}
		background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
		base.OnPointerDown(eventData);
    }

	public override void OnPointerUp(PointerEventData eventData)
    {
		background.anchoredPosition = Vector2.zero;
        base.OnPointerUp(eventData);
    }
}
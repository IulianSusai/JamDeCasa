using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInputDebugButton : MonoBehaviour
{
	[SerializeField] private Text uiText;

	public void OnChangeInput() {
		if(PlayerController.Instance.Forward == Vector3.forward) {
			PlayerController.Instance.SetForwardRight();
			uiText.text = "V1";
		} else {
			PlayerController.Instance.SetDefault();
			uiText.text = "V2";
		}
	}

}

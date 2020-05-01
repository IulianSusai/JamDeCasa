using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPage : MonoBehaviour
{
	
	public virtual void OpenPage() {
		if (UIManager.Instance.currentPage != null) {
			UIManager.Instance.currentPage.ClosePage();
		}
		UIManager.Instance.currentPage = this;
		RegisterEvents();
		gameObject.SetActive(true);
	}

	public virtual void ClosePage() {
		UnregisterEvents();
		gameObject.SetActive(false);
	}

	protected virtual void RegisterEvents() { }
	protected virtual void UnregisterEvents() { }

}

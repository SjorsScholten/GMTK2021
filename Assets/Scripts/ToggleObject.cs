using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour {
	private bool _shown = false;
	
	public void Toggle() {
		_shown = !_shown;
		this.gameObject.SetActive(_shown);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;

public class Unit {

	public event Action onGoalReached;
	
	public void Move() {
		
	}

	private void CheckPosition() {
		if (true) {
			onGoalReached?.Invoke();
		}
	}
}
using System;
using UnityEngine;

public class UnitSimulation {
	private Transform _transform;

	public event Action onDestinationReached;
	
	public UnitSimulation(Transform transform) {
		_transform = transform;
	}
	
	public void Move(Vector3 targetPos) {
		Vector3 newPos = Vector3.MoveTowards(_transform.position, targetPos, 2 * Time.deltaTime);
		_transform.position = newPos;
		CheckPosition(targetPos);
	}

	private void CheckPosition(Vector3 targetPos) {
		float distance = Vector3.Distance(_transform.position, targetPos);
		if (distance <= 0.01f) onDestinationReached?.Invoke();
	}
}
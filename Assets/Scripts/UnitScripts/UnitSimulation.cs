using System;
using UnityEngine;

public class UnitSimulation {
	private Transform _transform;
	private Vector3 _velocity = Vector3.zero;
	
	public UnitSimulation(Transform transform) {
		_transform = transform;
	}
	
	public void HandleMove(Vector3 targetPos, float speed, float accelerationMax) {
		Vector3 desiredVelocity = Vector3.ClampMagnitude(targetPos - _transform.position, 1) * speed;
		float maxSpeedChange = accelerationMax * Time.deltaTime;
		
		_velocity.x = Mathf.MoveTowards(_velocity.x, desiredVelocity.x, maxSpeedChange);
		_velocity.z = Mathf.MoveTowards(_velocity.z, desiredVelocity.z, maxSpeedChange);
		
		_transform.position += _velocity * Time.deltaTime;
	}

	public void HandleRotation() {
		_transform.rotation = Quaternion.LookRotation(_velocity);
	}

	public bool HasReachedPosition(Vector3 targetPos) {
		float distance = Vector3.Distance(_transform.position, targetPos);
		return distance <= 0.5f;
	}
}
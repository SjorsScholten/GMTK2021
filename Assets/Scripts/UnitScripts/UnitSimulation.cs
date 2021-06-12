using System;
using UnityEngine;

[Serializable]
public class UnitSimulation {
	private Transform _transform;
	private Vector3 _velocity = Vector3.zero;
	
	public UnitSimulation(Transform transform, Vector3 startPosition) {
		_transform = transform;
		_transform.position = startPosition;
	}
	
	public void HandleMove(Vector3 targetPos, float speed, float accelerationMax) {
		Vector3 targetOffset = targetPos + _transform.TransformDirection(new Vector3(0.8f, 0f, 0f));
		Vector3 desiredVelocity = Vector3.ClampMagnitude(targetOffset - _transform.position, 1) * speed;
		float maxSpeedChange = accelerationMax * Time.deltaTime;
		
		_velocity.x = Mathf.MoveTowards(_velocity.x, desiredVelocity.x, maxSpeedChange);
		_velocity.z = Mathf.MoveTowards(_velocity.z, desiredVelocity.z, maxSpeedChange);
		
		_transform.position += _velocity * Time.deltaTime;
	}

	public void HandleRotation() {
		if(_velocity.magnitude <= 0.01f) return;
		_transform.rotation = Quaternion.LookRotation(_velocity);
	}

	public bool HasReachedPosition(Vector3 targetPos) {
		float distance = Vector3.Distance(_transform.position, targetPos);
		return distance <= 1f;
	}
}
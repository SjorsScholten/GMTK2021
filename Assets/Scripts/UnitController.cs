using System;
using UnityEngine;
using UnityEngine.Events;

public class UnitController : MonoBehaviour {
	private Unit _unit;
	private UnitSimulation _unitSimulation;

	private Vector3 targetPosition;
	private Transform _transform;

	private void Awake() {
		_transform = GetComponent<Transform>();
		_unitSimulation = new UnitSimulation(_transform);
		_unitSimulation.onDestinationReached += () => UpdateTargetPosition();
	}

	private void Update() {
		if (NextAvailable()) { }
		_unitSimulation.Move(targetPosition);
	}
	
	private bool NextAvailable() => true;

	private void UpdateTargetPosition() {
		
	}
}
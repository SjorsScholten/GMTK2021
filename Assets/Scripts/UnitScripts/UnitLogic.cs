using System;
using System.Collections;
using System.Collections.Generic;
using UnitScripts;
using UnityEngine;

public class UnitLogic {
	private readonly UnitSimulation _unitSimulation;
	private readonly Unit _unit;
	
	private Cell[] _path;
	private int _pathIndex;
	private Vector3 _targetPosition;
	
	public event Action onGoalReached;

	public UnitLogic(UnitSimulation unitSimulation, Unit data, Cell[] path) {
		_unitSimulation = unitSimulation;
		_unit = data;
		_path = path;
		_pathIndex = -1;
		UpdateTargetPosition();
	}
	
	public void Move() {
		_unitSimulation.HandleMove(_targetPosition, _unit.speed, _unit.acceleration);
		_unitSimulation.HandleRotation();
		if(_unitSimulation.HasReachedPosition(_targetPosition)) 
			UpdateTargetPosition();
	}
	
	private void UpdateTargetPosition() {
		if (_pathIndex >= _path.Length - 1) {
			onGoalReached?.Invoke();
			return;
		}
		_pathIndex++;
		_targetPosition = _path[_pathIndex].Center;
	}
}
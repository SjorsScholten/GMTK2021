using System;
using System.Collections;
using System.Collections.Generic;
using UnitScripts;
using UnityEngine;

public class UnitLogic {
	private readonly UnitSimulation _unitSimulation;
	private readonly Unit _unit;
	
	private Cell[] _path = null;
	private int _pathIndex = 0;
	private Cell _targetCell = null;
	private Cell _futureCell = null;
	
	public event Action onGoalReached;

	public UnitLogic(UnitSimulation unitSimulation, Unit data, Cell[] path) {
		_unitSimulation = unitSimulation;
		_unit = data;
		_path = path;
		
		UpdateTargetCell();
	}
	
	public void Move() {
		_unitSimulation.HandleMove(_targetCell.Center, _unit.speed, _unit.acceleration);
		_unitSimulation.HandleRotation();

		if (!_unitSimulation.HasReachedPosition(_targetCell.Center)) return;

		if (!CheckFutureCell()) return;
		_pathIndex++;
		UpdateTargetCell();
	}
	
	private void UpdateTargetCell() {
		if (_targetCell) _targetCell.Exit();
		if (_pathIndex > _path.Length - 1) {
			onGoalReached?.Invoke();
			return;
		}
		_targetCell = _path[_pathIndex];
		_targetCell.Enter();
	}

	private bool CheckFutureCell() {
		if (_pathIndex + 1 > _path.Length - 1) {
			_futureCell = null;
			return true;
		}
		_futureCell = _path[_pathIndex + 1];

		return _futureCell.CanPass();
	}
}
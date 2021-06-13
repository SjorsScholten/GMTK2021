using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnitScripts;
using UnityEngine;

[Serializable]
public class UnitLogic {
	private UnitSimulation _unitSimulation;
	private readonly Unit _unit;
	
	private Cell[] _path = null;
	private int _pathIndex = 0;
	
	public event Action onGoalReached;

	public UnitLogic(UnitSimulation unitSimulation, Unit data, Cell[] path) {
		_unitSimulation = unitSimulation;
		_unit = data;
		_path = path;
		
		Initialize();
	}

	private void Initialize() {
		_pathIndex = 0;
		SetTarget(_pathIndex);
	}
	
	public void Move() {
		_unitSimulation.HandleMove(_unit.targetCell.GetPosition(), _unit.speed, _unit.acceleration);
		_unitSimulation.HandleRotation();

		CheckIfTargetReached();
	}

	private void CheckIfTargetReached() {
		Vector3 targetPosition = _unit.targetCell.GetPosition();
		if (_unitSimulation.HasReachedPosition(targetPosition)) {
			if (_pathIndex > _path.Length - 1) {
				onGoalReached?.Invoke();
				return;
			}

			Cell futureCell = _unit.futureCell;
			if(!futureCell) return;
			if (futureCell.CanPass(_unit)) {
				_pathIndex++;
				SetTarget(_pathIndex);
			}
		}
	}

	private void SetTarget(int index) {
		if (_pathIndex > _path.Length - 1) return;
		_unit.targetCell = _path[index];
				
		if(index + 1 >= _path.Length) return;
		_unit.futureCell = _path[index + 1];
	}
}
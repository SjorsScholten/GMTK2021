using System;
using System.Collections;
using System.Collections.Generic;
using UnitScripts;
using UnityEngine;

[Serializable]
public class Road : Cell {
	private int _laneCount = 2;
	private int _slotCount = 3;
	private UnitSlot[] _unitSlots;
	private float _cellSize = 1f;

	private float laneOffset, slotOffset;

	protected override void Awake() {
		base.Awake();
		
		_unitSlots = new UnitSlot[_laneCount * _slotCount];
		
		laneOffset = _cellSize / (_laneCount + 1);
		slotOffset = _cellSize / (_slotCount + 1);
		
		for (int l = 0; l < _laneCount; l++) {
			for (int s = 0; s < _slotCount; s++) {
				UnitSlot unitSlot = new UnitSlot();
				Vector3 position = base.GetPosition();
				unitSlot.position.x = position.x - (_cellSize / 2) + slotOffset * (s + 1);
				unitSlot.position.z = position.z - (_cellSize / 2) + laneOffset * (l + 1);
				
				_unitSlots[l * _slotCount + s] = unitSlot;
			}
		}
	}

	public override Vector3 GetPosition(Vector3 headingDirection = new Vector3()) {
		UnitSlot unitSlot = GetSlot(headingDirection);
		if (unitSlot != null) {
			//unitSlot.occupied = true;
			return unitSlot.position;
		}
		return Vector3.zero;
	}

	public override bool CanPass(Unit unit) {
		UnitSlot unitSlot = GetSlot(unit.HeadingDirection);
		if (unitSlot == null) return false;
		if (unit.slot != null) unit.slot.occupied = false;
		unit.slot = unitSlot;
		unitSlot.occupied = true;
		return true;
	}

	public UnitSlot GetSlot(Vector3 headingDirection) {
		Vector3 orientation = LaneOrientation();
		float dot = Vector3.Dot(headingDirection, orientation);
		int lane = Mathf.RoundToInt(Mathf.Abs(dot));
		for (int s = 0; s < _slotCount; s++) {
			UnitSlot unitSlot = _unitSlots[lane * _slotCount + s];
			if(unitSlot.occupied) continue;
			return unitSlot;
		}

		return null;
	}
	
	private Vector3 LaneOrientation() {
		if (HasNeighborInDirection(Vector3.right) || HasNeighborInDirection(Vector3.left)) 
			return Vector3.right;
		if (HasNeighborInDirection(Vector3.forward) || HasNeighborInDirection(Vector3.back))
			return Vector3.forward;
		return Vector3.zero;
	}
	
}


using System;
using UnityEngine;

[Serializable]
public class UnitSlot {
	//know where the unit is coming from
	//check what slot is available
	//if not the right slot available cant pass
	//working with cells in cells ;_; cell-ception
	
	// x amount of lanes, x amount of slots (lanes * slots)
	// lane orientation based on neighbors (horizontal or vertical)
	// Only roads need to have slots, crossings cant hold units

	public Vector3 position = Vector3.zero;
	public bool occupied = false;

	public void Enter() {
		
	}

	public void Exit() {
		
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnitScripts;
using UnityEngine;

[Serializable]
public class Crossing : Cell {
	public CrossingBooleans canCross;

	public void SetCanBeCrossed()
	{
		List<Cell> horizontal = _neighbours.FindAll(c => c.gridX != gridX);
		List<Cell> vertical = _neighbours.FindAll(c => c.gridY != gridY);
		canCross.HasHorizontal = horizontal.Count > 1;
		canCross.HasVertical = vertical.Count > 1;

		canCross.CanBeCrossedHorizontal = canCross.HasHorizontal;
		canCross.CanBeCrossedVertical = canCross.HasVertical;
	}

	public override bool CanPass(Unit unit) {
		if(unit == null)return false;
		Cell towards = unit.futureCell;
		
		if (!towards) return false;
		
		if (!towards.gridX.Equals(gridX))
		{
			if (canCross.HasHorizontal)
			{
				return canCross.CanBeCrossedHorizontal && towards.CanPass(null);
			}
			return true;
		}
		else
		{
			if (canCross.HasVertical)
			{
				return canCross.CanBeCrossedVertical && towards.CanPass(null);
			}
			return true;
		}
	}
}

public struct CrossingBooleans {
	public bool CanBeCrossedHorizontal;
	public bool CanBeCrossedVertical;
	public bool HasHorizontal;
	public bool HasVertical;
}

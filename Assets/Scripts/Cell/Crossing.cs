using System.Collections;
using System.Collections.Generic;
using UnitScripts;
using UnityEngine;

public class Crossing : Cell, IRoad {
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

	public override bool CanPass(Cell towards = null)
	{
		if (!towards)
			return false;
		if (!towards.gridX.Equals(gridX))
		{
			if (canCross.CanBeCrossedHorizontal)
			{
				return towards.CanPass();
			}
			return false;
		}
		else
		{
			if (canCross.CanBeCrossedVertical)
			{
				return towards.CanPass();
			}
			return false;
		}
	}
}

public struct CrossingBooleans {
	public bool CanBeCrossedHorizontal;
	public bool CanBeCrossedVertical;
	public bool HasHorizontal;
	public bool HasVertical;
}

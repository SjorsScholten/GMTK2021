using System.Collections;
using System.Collections.Generic;
using UnitScripts;
using UnityEngine;

public class Crossing : Cell, IRoad {
	public CrossingBooleans canCross;

	[SerializeField] private GameObject _horizontalIcon;
	[SerializeField] private GameObject _verticalIcon;
	[SerializeField] private GameObject _canvas;

	private Camera _camera;
	
	public void SetCanBeCrossed()
	{
		List<Cell> horizontal = _neighbours.FindAll(c => c.gridX != gridX);
		List<Cell> vertical = _neighbours.FindAll(c => c.gridY != gridY);
		canCross.HasHorizontal = horizontal.Count > 1;
		canCross.HasVertical = vertical.Count > 1;

		canCross.CanBeCrossedHorizontal = canCross.HasHorizontal;
		canCross.CanBeCrossedVertical = canCross.HasVertical;
		
		SetHorizontal((Random.Range(0, 2) == 1));
		Random.InitState(System.DateTime.Now.Millisecond);
		SetVertical((Random.Range(0, 2) == 1));
	}

	public override bool CanPass(Cell towards = null)
	{
		if (!towards)
			return false;
		
		if (!towards.gridX.Equals(gridX))
		{
			if (canCross.HasHorizontal)
			{
				return canCross.CanBeCrossedHorizontal && towards.CanPass();
			}
			return true;
		}
		else
		{
			if (canCross.HasVertical)
			{
				return canCross.CanBeCrossedVertical && towards.CanPass();
			}
			return true;
		}
	}

	public void SetHorizontal(bool enable)
	{
		if (canCross.HasHorizontal)
		{
			canCross.CanBeCrossedHorizontal = enable;
			_horizontalIcon.SetActive(!enable);
		}
	}
	
	public void SetVertical(bool enable)
	{
		if (canCross.HasVertical)
		{
			canCross.CanBeCrossedVertical = enable;
			_verticalIcon.SetActive(!enable);
		}
	}
}

public struct CrossingBooleans {
	public bool CanBeCrossedHorizontal;
	public bool CanBeCrossedVertical;
	public bool HasHorizontal;
	public bool HasVertical;
}

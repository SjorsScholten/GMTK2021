using System;
using UnityEngine;

namespace UnitScripts {
	[Serializable]
	public class Unit {
		public float speed = 5f;
		public float acceleration = 3f;

		public Cell targetCell;
		public Cell futureCell;

		public UnitSlot slot;
		public Vector3 HeadingDirection => targetCell.GridCoord - futureCell.GridCoord;
	}
}
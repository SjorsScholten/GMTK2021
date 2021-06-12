using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnitScripts {
	public class DebugRoad : MonoBehaviour, IPathGenerator {
		public Road[] roadCells;
		
		public IRoad[] GetPath(Vector2 startCoord) {
			return roadCells;
		}
	}
}
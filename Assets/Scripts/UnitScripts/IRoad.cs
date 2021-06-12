using UnityEngine;

namespace UnitScripts {
	public interface IRoad {
		Vector3[] CornerVertices { get; }
		Vector3 Center { get; }
		bool CanPass(Cell towards = null);
	}
}
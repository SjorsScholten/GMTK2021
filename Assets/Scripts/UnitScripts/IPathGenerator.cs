using UnityEngine;

namespace UnitScripts {
	public interface IPathGenerator {
		IRoad[] GetPath(Vector2 startCoord);
	}

	public interface IRoad {
		Vector3[] CornerVertices { get; }
		Vector3 Center { get; }
		bool CanPass();
	}
}
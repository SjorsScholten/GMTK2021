using UnityEngine;

namespace UnitScripts {
	public class Road : MonoBehaviour, IRoad {
		private Transform _transform;
		private Vector3 _center = Vector3.zero;
		
		public Vector3[] CornerVertices {
			get {
				Vector3[] corners = new Vector3[4];
				Vector3 position = _transform.position;
				corners[0] = position + (Vector3.left + Vector3.up) / 2;
				corners[1] = position + (Vector3.right + Vector3.up) / 2;
				corners[2] = position + (Vector3.right + Vector3.down) / 2;
				corners[3] = position + (Vector3.left + Vector3.down) / 2;
				return corners;
			}
		}

		public Vector3 Center {
			get {
				if (_center != Vector3.zero) return _center;
				
				foreach (Vector3 vertice in CornerVertices) _center += vertice;
				_center /= CornerVertices.Length;
				
				return _center;
			}
		}

		private void Awake() {
			_transform = GetComponent<Transform>();
		}
		
		public bool CanPass(Cell towards = null) {
			return true;
		}
	}
}
using System;
using System.Collections.Generic;
using UnitScripts;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class UnitSpawner : MonoBehaviour {
	[SerializeField] private UnitController[] _spawnableUnits;
	[SerializeField] private DebugRoad _debugRoad;

	public UnityEvent onGoalReached;

	private void Start() {
		SpawnUnit();
	}

	[ContextMenu("spawn")]
	public void SpawnUnit() {
		UnitController unit = Instantiate(_spawnableUnits[Random.Range(0, _spawnableUnits.Length)], transform);
		IRoad[] path = _debugRoad.GetPath(Vector2.zero);
		unit.Initialize(path);
		unit.onGoalReached += onGoalReached.Invoke;
	}
}


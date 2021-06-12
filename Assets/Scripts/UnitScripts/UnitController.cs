using System;
using UnitScripts;
using UnityEngine;
using UnityEngine.Events;

public class UnitController : MonoBehaviour {
	[SerializeField] private Unit unit;
	
	private UnitLogic _unitLogic;
	private Transform _transform;

	public event Action onGoalReached;

	private void Awake() {
		gameObject.SetActive(false);
		_transform = GetComponent<Transform>();
	}

	public void Initialize(IRoad[] path) {
		UnitSimulation unitSimulation = new UnitSimulation(_transform);
		_unitLogic = new UnitLogic(unitSimulation, unit, path);
		_unitLogic.onGoalReached += GoalReached;
		gameObject.SetActive(true);
	}

	private void Update() {
		_unitLogic.Move();
	}

	private void GoalReached() {
		onGoalReached?.Invoke();
		GameObject.Destroy(gameObject);
	}
	
}
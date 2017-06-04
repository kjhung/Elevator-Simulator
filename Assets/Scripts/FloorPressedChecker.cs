using UnityEngine;
using System.Collections;

public class FloorPressedChecker : MonoBehaviour {

	bool isThisFloorTheTarget;
	[HideInInspector] int _theNumberBePressedToInt;

	[HideInInspector] GameObject _GameController;
	[HideInInspector] int _numberOfTargets;
	[HideInInspector] int [] _targetFloors;
	[HideInInspector] bool [] _isTargetFloorPressed;
	[HideInInspector] public bool isWaitActive;
	[HideInInspector] public bool _startFloorChecker;

	void Start () {
		_targetFloors = new int [_numberOfTargets];
		_isTargetFloorPressed = new bool[_numberOfTargets];

		_GameController = GameObject.Find ("GameController");
		_numberOfTargets = _GameController.GetComponent<GameController> ().numberOfTargets;
		_targetFloors = _GameController.GetComponent<GameController> ().targetFloors;
		_isTargetFloorPressed = _GameController.GetComponent<GameController> ().isTargetFloorPressed;

		isWaitActive = false;
	}
	
	void Update () {
		_startFloorChecker = GetComponent<RaycastTargetTrigger> ().startFloorChecker;

		if (_startFloorChecker == true) {
			CheckWhichFloorIsPressedCorrectly ();
		}
	}

	// Chech which Floor is pressed correctly.
	void CheckWhichFloorIsPressedCorrectly () {

		_theNumberBePressedToInt = GetComponent<RaycastTargetTrigger> ().theNumberBePressedToInt;
		//Debug.Log (_targetFloors.Length);
		for (int i = 0; i < _targetFloors.Length; i++) {
			//Debug.Log (_theNumberBePressedToInt);
			if (_targetFloors [i] == _theNumberBePressedToInt) {
				_isTargetFloorPressed [i] = true;
				isThisFloorTheTarget = true;
			}
		}
		if (isWaitActive == false) {
			StartCoroutine (TurnOnLight ());
		}
	}

	// Check the light should be turned on or off.
	IEnumerator TurnOnLight () {
		if (isThisFloorTheTarget == true) {
			//GetComponentInChildren<Light> ().enabled = true;
			transform.FindChild("ButtonLight").gameObject.SetActive(true);
		} else {
			isWaitActive = true;
			yield return new WaitForSeconds (5);	// Wait for 5 seconds to prevent from pressing randomly.

			//GetComponentInChildren<Light> ().enabled = false;
			transform.FindChild("ButtonLight").gameObject.SetActive(false);
			isWaitActive = false;
		}
	}

}

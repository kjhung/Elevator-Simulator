using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour {

	RaycastTargetTrigger _RaycastTargetTrigger;
	int _theNumberBePressedToInt;


	// Use this for initialization
	void Start () {
		_RaycastTargetTrigger = GameObject.Find ("Floor01").GetComponent<RaycastTargetTrigger> ();
	}
	
	// Update is called once per frame
	void Update () {
		_theNumberBePressedToInt = _RaycastTargetTrigger.theNumberBePressedToInt;

		if (_theNumberBePressedToInt == 1) {
			SceneManager.LoadScene (1);
		}
	}
}

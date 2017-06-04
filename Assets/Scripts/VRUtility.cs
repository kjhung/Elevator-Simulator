using UnityEngine;
using System.Collections;

using UnityEngine.VR;

public class VRUtility : MonoBehaviour {

	// Use this for initialization
	void Start () {

		VRSettings.renderScale = 1f;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.R)) {
			InputTracking.Recenter ();
		}
	}
}

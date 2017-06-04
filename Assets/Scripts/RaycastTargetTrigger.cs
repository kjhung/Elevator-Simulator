using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.VR; // we need this line here for InputTracking and VRSettings functions

// USAGE: put this on a thing you want the player to look at!
// this code will enable that thing to know if it is being looked at!
[RequireComponent (typeof(Collider)) ] // this thing needs a collider if we should look at it
public class RaycastTargetTrigger : MonoBehaviour {

	bool amIBeingLookedAt = false;
	public float maximumDistance = 1000f;

	//public Light thelightOfTheFloorBePressed;
	public GameObject thelightOfTheFloorBePressed;
	[HideInInspector] public string theNumberBePressed;
	public int theNumberBePressedToInt;

	[HideInInspector] bool timeToTurnLight;
	[HideInInspector] bool countdonwTimerStart;
	[HideInInspector] float countdonwTimer;
	[HideInInspector] float countdownDelay;
	public Image countdownImage;

	public bool startFloorChecker;

	void Start () {
		//thelightOfTheFloorBePressed = GetComponentInChildren<Light> ();
		thelightOfTheFloorBePressed = transform.FindChild("ButtonLight").gameObject;
		theNumberBePressed = GetComponentInChildren<TextMesh> ().text;

		countdownImage = GameObject.Find ("CanvasForFPS").GetComponentInChildren<Image> ();
		countdownImage.fillAmount = 0;
		countdonwTimer = 0;
		countdownDelay = 1.0f;
	}


	// ADD YOUR CODE AND BEHAVIORS IN THESE FOUR FUNCTIONS BELOW ===============================

	// happen the first frame when I start looking at something
	void OnStartLook () { 
		// Debug.Log("user STARTED looking at object " + name);
		countdonwTimerStart = true;
		countdonwTimer = 0;
		countdownImage.fillAmount = 0;

	}

	// will happen the first frame when I STOP looking at something
	void OnStopLook () { 
		//Debug.Log("user STOPPED looking at object " + name);
		countdonwTimerStart = false;
		countdonwTimer = 0;
		countdownImage.fillAmount = 0;
	}

	// will constantly happen every frame, while thing is NOT being looked at
	void OnNotLooking () { 
		// Debug.Log("user is still NOT looking at object " + name );
	}

	// will constantly happen every Update, while thing is being looked at
	void OnLooking () { 
		// Debug.Log("user is still looking at object " + name );

		int.TryParse (theNumberBePressed, out theNumberBePressedToInt);
		//thelightOfTheFloorBePressed.enabled = true;
		countdownTrigger ();
		if(timeToTurnLight == true){
			thelightOfTheFloorBePressed.SetActive(true);
			startFloorChecker = true;
		}
	}

	void countdownTrigger () {
		if (countdonwTimerStart == true) {
			countdonwTimer += Time.deltaTime;
			countdownImage.fillAmount = Mathf.Lerp (0, 1, countdonwTimer / countdownDelay);
		}

		if (countdownImage.fillAmount == 1 && countdonwTimerStart == true) {
			countdonwTimerStart = false;
			countdonwTimer = 0;
			countdownImage.fillAmount = 0;
			timeToTurnLight = true;
		}
	}



	// =========================================================================================
	// TRY NOT TO EDIT BELOW HERE ==============================================================
	// =========================================================================================

	// Update is called once per frame
	void Update () {
		
		// STEP 1: setup a Ray variable before we fire a Raycast
		Vector3 rayOrigin = Camera.main.transform.position;
		Vector3 rayDirection = Camera.main.transform.forward;

		// if we are using VR, we can get more accurate info by reading the tracking info directly
		// (this also fixes a bug in OSX when using Unity's Standard Assets FPSController and MouseLook, where Camera forward is based on mouse)
		if ( VRSettings.loadedDevice != VRDeviceType.None ) {
			// shoot a ray based on the HMD's reported rotation
			rayDirection = InputTracking.GetLocalRotation(VRNode.CenterEye) * Vector3.forward; 
			// do extra correction pass if Main Camera is parented to something
			if ( Camera.main.transform.parent != null ) { 
				rayDirection = Camera.main.transform.parent.TransformDirection( rayDirection ); 
			}
		}

		// actually construct the ray
		Ray ray = new Ray( rayOrigin, rayDirection );
		// visualize all this stuff in Scene View
		Debug.DrawRay( ray.origin, ray.direction * maximumDistance, Color.yellow); 

		// STEP 2: setting up a blank var to know where we hit something
		RaycastHit rayHitInfo = new RaycastHit(); 

		// note that a Raycast is an infinitely thin line... which means it only detects things that
		// are in the exact middle of the screen... if you want a "thick Raycast", look up "Physics.Spherecast"

		// STEP 3: actually shoot the raycast now
		if ( Physics.Raycast( ray, out rayHitInfo, maximumDistance ) && rayHitInfo.collider == GetComponent<Collider>() ) {
			// visualize the successful raycast as a red line, using the actual distance from impact
			Debug.DrawRay( ray.origin, ray.direction * rayHitInfo.distance, Color.red ); 
			// visualize the impact point as a small magenta line, pointing based on the surface's curvature
			Debug.DrawRay( rayHitInfo.point, rayHitInfo.normal, Color.magenta ); 
			// ok do logic stuff now...
			if (amIBeingLookedAt == false ) {
				OnStartLook();
				amIBeingLookedAt = true;
			}
			OnLooking();
		} 

		else { // what if the user is NOT looking at this thing?...
			if (amIBeingLookedAt == true ) {
				OnStopLook();
				amIBeingLookedAt = false;
			}
			OnNotLooking();
		}
	}

}
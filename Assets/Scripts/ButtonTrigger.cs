using UnityEngine;
using UnityEngine.VR;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonTrigger : MonoBehaviour {

	public Image countdownImg;
	Button gazeTarget;
	//GameObject gazeTarget;

	float timer;

	public LayerMask triggerLayer;
	// float rayRadius = 2;
	void Start () {
		timer = 0;
		gazeTarget = null;
		if (countdownImg != null) {
			countdownImg.enabled = false;
			countdownImg.fillAmount = 0;
		}
	}

	float delay = 0.75f;


	// Update is called once per frame
	void Update () {	
		//		bool zooming = Input.GetKey (KeyCode.Space);
		//		Camera.main.fieldOfView = Mathf.MoveTowards (Camera.main.fieldOfView, (zooming ? 15 : 60), 30 * Time.deltaTime);	

		RaycastHit rayHit;
		// bool hit = Physics.SphereCast (Camera.main.transform.position, rayRadius, Camera.main.transform.forward, out rayHit, 200f, triggerLayer);

		Vector3 rayOrigin = Camera.main.transform.position;
		Vector3 rayDirection = Camera.main.transform.forward;
		 //if we are using VR, we can get more accurate info by reading the tracking info directly
		 //(this also fixes a bug in OSX when using Unity's Standard Assets FPSController and MouseLook, where Camera forward is based on mouse)
		 if ( VRSettings.loadedDevice != VRDeviceType.None ) {
		 	// shoot a ray based on the HMD's reported rotation
		 	rayDirection = InputTracking.GetLocalRotation(VRNode.CenterEye) * Vector3.forward; 
		 	// do extra correction pass if Main Camera is parented to something
		 	if ( Camera.main.transform.parent != null ) { 
		 		rayDirection = Camera.main.transform.parent.TransformDirection( rayDirection ); 
		 	}
		 }

		bool hit = Physics.Raycast (rayOrigin, rayDirection, out rayHit, 200f, triggerLayer);
		//		bool hit = Physics2D.Raycast (rayOrigin, rayDirection, out rayHit, 200f, triggerLayer);
		//		Debug.DrawRay (rayOrigin, rayDirection * 200, Color.red, 0);
		Debug.Log (rayHit.transform);
		if (hit) {
			Debug.Log ("hit");
			if (gazeTarget == null) {	//new timer
				timer = 0;
				gazeTarget = rayHit.transform.GetComponent<Button> ();
				if (countdownImg != null) {
					countdownImg.enabled = true;
					countdownImg.fillAmount = 0;
				}

			}

			if (gazeTarget != null) {
				//ExecuteEvents.Execute<IPointerEnterHandler>(gazeTarget.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
				timer += Time.deltaTime;
				if (countdownImg != null) {
					countdownImg.fillAmount = Mathf.Lerp (0, 1, timer / delay);
				}
				if (timer > delay) {
					if (countdownImg != null) {
						countdownImg.fillAmount = 1;
					}

					//					EventSystem.current.SetSelectedGameObject (gazeTarget);

					//					BaseInputModule module = EventSystem.current.currentInputModule;
					//ExecuteEvents.Execute<IPointerClickHandler>(gazeTarget.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
					//					module.cli
					//					gazeTarget.TriggerEffect ();	

					//					if (gazeTarget.delay == 0) {
					//						countdownImg.fillAmount = 0;
					//					}
				}
			}
		} else if (gazeTarget != null) {
			//			gazeTarget.CancelEffect ();
			//ExecuteEvents.Execute<IPointerExitHandler>(gazeTarget.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
			gazeTarget = null;
			if (countdownImg != null) {
				countdownImg.enabled = false;
			}
		}

	}
}
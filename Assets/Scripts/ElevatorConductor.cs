using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class ElevatorConductor : MonoBehaviour {

	[HideInInspector] public Text floorNumber;
	[HideInInspector] public int startFloorNumber;
	[HideInInspector] public string startFloorNumberToString;
	public int elevatorStopSign;
	public int stopSignCycle;
	public int waitTime;

	[HideInInspector] public float holdTimer;
	[HideInInspector] bool elevatorMove;

	[HideInInspector] GameObject passenger;
	[HideInInspector] int _numberOfPassengrs;
	[HideInInspector] int passengerIndex;
	[HideInInspector] float passengerComeInTime;
	[HideInInspector] GameObject [] passengerArray;
	SceneFading _SceneFading;


	AudioSource elevatorSound;
	AudioClip dingSFX;

//	Image _FadeBlack;
//	Color imageColor;
//	float fadeSpeed;
//	float fadeTime;
//	float alpha;
//	bool valueGet = false;

	int sceneIndex;

	void Start () {
		_SceneFading = GameObject.Find ("SceneFading").GetComponent<SceneFading> ();

		sceneIndex = SceneManager.GetActiveScene ().buildIndex;

		holdTimer = 0f;
		floorNumber = GameObject.Find ("MainElevator").GetComponentInChildren<Text> ();
		startFloorNumber = Random.Range (10, 20);
		startFloorNumberToString = startFloorNumber.ToString ();
		floorNumber.text = startFloorNumberToString;

		elevatorStopSign = Random.Range (3, 8);
		elevatorMove = true;

		if (sceneIndex == 1) {
			_numberOfPassengrs = 3;
		}
		else if (sceneIndex == 3) {
			_numberOfPassengrs = 5;
		}
		else if (sceneIndex == 5) {
			_numberOfPassengrs = 7;
		}

		passengerIndex = 0;
		passengerArray = new GameObject[_numberOfPassengrs];

		for (int i = 0; i < _numberOfPassengrs; i++) {
			string passengerName = "Audience" + i;
			passenger = GameObject.Find (passengerName);
			passenger.GetComponent<Animation> ().wrapMode = WrapMode.Loop;
			passengerArray [i] = passenger;
			passengerArray [i].SetActive(false);
		}

		elevatorSound = GetComponent<AudioSource> (); 

		_SceneFading.FadeSetup ();
		//FadeSetup ();
	}
	
	void Update () {

		holdTimer += Time.deltaTime;
		passengerComeInTime += Time.deltaTime;

		if (passengerIndex <= _numberOfPassengrs-1) {
			if (passengerComeInTime >= 5.0f) {
				passengerArray [passengerIndex].SetActive (true);
				passengerIndex++;
				passengerComeInTime = 0f;
			}
		}

		if (elevatorMove == true) {
			if (holdTimer >= 1.5f) {
				startFloorNumber -= 1;
				startFloorNumberToString = startFloorNumber.ToString ();
				floorNumber.text = startFloorNumberToString;
				stopSignCycle++;
				holdTimer = 0f;
			}
		}

		if (stopSignCycle == elevatorStopSign) {
			stopSignCycle = 0;
			elevatorMove = false;
			waitTime = Random.Range (5, 8);
			StartCoroutine (StopElevator (waitTime));
		}

		if (startFloorNumber == 1) {

			if (elevatorSound.isPlaying == false) {
				elevatorSound.PlayOneShot (elevatorSound.clip);
			}

			_SceneFading.FadeStart();
//			if (valueGet) {
			if (_SceneFading.valueGet) {
				SceneManager.LoadScene (sceneIndex + 1);
			}
		}
	}

	IEnumerator StopElevator(int waitTime){
		elevatorStopSign = Random.Range (3, 8);
		yield return new WaitForSeconds (5);
		elevatorMove = true;
	}

	IEnumerator PlaSFX () {
		elevatorSound = GetComponent<AudioSource> ();
		elevatorSound.Play ();
		yield return new WaitForSeconds (2);
	}

//	void FadeSetup () {
//		_FadeBlack = GameObject.Find ("FadeInOut").GetComponent<Image> ();
//		imageColor = _FadeBlack.color;
//		fadeSpeed = 0.9f;
//	}
//
//	void FadeStart () {
//		fadeTime += Time.deltaTime;
//
//		if (alpha < 1) {
//			alpha += (float)fadeSpeed * Time.deltaTime;
//			imageColor.a = alpha;
//			_FadeBlack.color = imageColor;
//		}
//
//		if (alpha > 1) {
//			if (valueGet == false) {
//				valueGet = true;
//			}
//		}
//	}

}

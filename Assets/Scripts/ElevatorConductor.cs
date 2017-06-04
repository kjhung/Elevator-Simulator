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

	GameObject passenger;
	public int _numberOfPassengrs;
	int passengerIndex;
	float passengerComeInTime;
	GameObject [] passengerArray;


	void Start () {

		holdTimer = 0f;

		floorNumber = GameObject.Find ("MainElevator").GetComponentInChildren<Text> ();
		startFloorNumber = Random.Range (30, 50);
		startFloorNumberToString = startFloorNumber.ToString ();
		floorNumber.text = startFloorNumberToString;

		elevatorStopSign = Random.Range (3, 8);
		elevatorMove = true;

		_numberOfPassengrs = 7;
		passengerIndex = 0;
		passengerArray = new GameObject[_numberOfPassengrs];

		for (int i = 0; i < _numberOfPassengrs; i++) {
			string passengerName = "Audience" + i;
			passenger = GameObject.Find (passengerName);
			passengerArray [i] = passenger;
			passengerArray [i].SetActive(false);
		}
	}
	
	void Update () {

		holdTimer += Time.deltaTime;
		passengerComeInTime += Time.deltaTime;

		if (passengerComeInTime >= 5.0f) {
			
			passengerArray [passengerIndex].SetActive (true);
			passengerIndex++;
			passengerComeInTime = 0f;
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
			
		}
			
	}

	IEnumerator StopElevator(int waitTime){
		elevatorStopSign = Random.Range (3, 8);
		yield return new WaitForSeconds (5);
		elevatorMove = true;
	}
}

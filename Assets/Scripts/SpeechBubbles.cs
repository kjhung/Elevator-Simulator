using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeechBubbles : MonoBehaviour {

	[HideInInspector] public GameController _GameController;

	[HideInInspector] public string passengerLine;
	[HideInInspector] public Text lineToText;

	public int numberOfPassengers;
	[HideInInspector] public int [] _targetFloors;

	[HideInInspector] public int howManyPeopleSpeak;
	public bool isEveryoneSpeak;
	public bool isTimeStart;

	void Start () {
		StartCoroutine (SpeechDisplay ());
	}

	void Update () {
		StartCoroutine (RepeatSpeech ());
	}

	IEnumerator SpeechDisplay () {
		_GameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		lineToText = null;
		numberOfPassengers = _GameController.numberOfTargets;
		_targetFloors = new int [_GameController.numberOfTargets];
		_targetFloors = _GameController.GetComponent<GameController> ().targetFloors;
		howManyPeopleSpeak = 0;
		isEveryoneSpeak = false;
		isTimeStart = false;

		yield return new WaitForSeconds (2);

		for (int i = 0; i < numberOfPassengers; i++) {

			string passengerName = "Audience" + i;
			passengerLine = " floor, please!";
			passengerLine = _targetFloors [i] + passengerLine;
			
			lineToText = GameObject.Find (passengerName).GetComponentInChildren<Text> ();
			lineToText.text = passengerLine;

			// After everyone speak, then Timer start countdown.
			howManyPeopleSpeak++;
			if (howManyPeopleSpeak == numberOfPassengers) {
				isTimeStart = true;
				isEveryoneSpeak = true;
			}
			// Make a delay so Passengers speak one by one.
			yield return new WaitForSeconds (2);
			lineToText.enabled = false;
		}
	}

	IEnumerator RepeatSpeech () {
		yield return new WaitForSeconds (10);
		for (int i = 0; i < numberOfPassengers; i++) {
			string passengerName = "Audience" + i;
			lineToText = GameObject.Find (passengerName).GetComponentInChildren<Text> ();

			if (lineToText.enabled == false) {
				yield return new WaitForSeconds (2);
				lineToText.enabled = true;
			}else if (lineToText.enabled == true) {
				yield return new WaitForSeconds (2);
				lineToText.enabled = false;
			}
		}
	}
}

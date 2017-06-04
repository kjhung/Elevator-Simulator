using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour {

	[HideInInspector] GameObject _GameController;
	[HideInInspector] bool _amIWinTheGame;
	[HideInInspector] public string showTime;
	public float _timeLeft;
	Text timeText;

	void Start () {
		_GameController = GameObject.Find ("GameController");
		timeText = GetComponent<Text> ();
	}
	
	void Update () {
		_timeLeft = _GameController.GetComponent<GameController> ().timeLeft;
		_amIWinTheGame = _GameController.GetComponent<GameController> ().amIWinTheGame;

		showTime = _timeLeft.ToString ("f2");

		if (_timeLeft > 0 && _amIWinTheGame == false) {
			timeText.text = showTime;
		} else if (_timeLeft < 0 && _amIWinTheGame == false) {
			timeText.text = "0.00";
		} else if (_amIWinTheGame == true) {
			timeText.text = "--.--";
		}

	}
}

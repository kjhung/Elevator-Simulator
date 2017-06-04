using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStatusDisplay : MonoBehaviour {

	bool _amIWinTheGame;
	bool _gameOver;
	float timeLeftToSwitch;
	bool isTimeToSwitch;
	int _sceneIndex;
	int nextSceneIndext;

	Text gameStatusText;
	string gameStatus;
	string timeText;

	void Start () {
		gameStatusText = GetComponent<Text> ();
		timeLeftToSwitch = 7.0f;
		isTimeToSwitch = false;
	}
	
	void Update () {
		_amIWinTheGame = GameObject.Find ("GameController").GetComponent<GameController> ().amIWinTheGame;
		_gameOver = GameObject.Find ("GameController").GetComponent<GameController> ().gameOver;
		_sceneIndex = GameObject.Find ("GameController").GetComponent<GameController> ().sceneIndex;
		nextSceneIndext = _sceneIndex + 1;

		if (_amIWinTheGame == true) {
			SceneSwithTimer ();

			if (_sceneIndex == 3) {
				timeText = timeLeftToSwitch.ToString ("f0");
				gameStatus = "Wow! You made it!!!!!" + "\n" + "Go back to Start in " + timeText + " sec";
				gameStatusText.text = gameStatus;
			} else {
				timeText = timeLeftToSwitch.ToString ("f0");
				gameStatus = "Nice Job! Get ready for the next challenge!" + "\n" + "Scene will change in " + timeText + " sec";
				gameStatusText.text = gameStatus;
			}
		} else if (_gameOver == true) {
			SceneSwithTimer ();
			timeText = timeLeftToSwitch.ToString ("f0");
			gameStatus = "EVERYONE IS LATE! THAT'S YOUR FAULT!!!" + "\n" + "Scene will change in " + timeText + " sec";
			gameStatusText.text = gameStatus;
		}

		if (_amIWinTheGame == true && isTimeToSwitch == true) {
			if (nextSceneIndext > 3) {
				SceneManager.LoadScene (0);
			} else {
				SceneManager.LoadScene (nextSceneIndext);
			}
		}
	}

	void SceneSwithTimer () {
		timeLeftToSwitch -= Time.deltaTime;
		if (timeLeftToSwitch < 0) {
			isTimeToSwitch = true;
		} else {
			isTimeToSwitch = false;
		}
	}
}

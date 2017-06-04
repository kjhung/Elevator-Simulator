using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	[HideInInspector] public int numberOfTargets;	// Total number of targets.
	public bool [] isTargetFloorPressed;			// The correct target floor being pressed.
	public int [] targetFloors;						// The Floor tends to go.
	int randomNumber;

	[HideInInspector] public bool isAllAnswerCorrect;
	[HideInInspector] public float timeLeft;		// Use for CountdownTimer().
	[HideInInspector] public bool isTimeUp;			// Is time up or not.

	[HideInInspector] bool _isTimeStart;

	[HideInInspector] public bool amIWinTheGame;
	[HideInInspector] public bool gameOver;

	[HideInInspector] public int sceneIndex;


	void Start () {
		sceneIndex = SceneManager.GetActiveScene ().buildIndex;

		if (sceneIndex == 1) {
			numberOfTargets = 3;
		}else if(sceneIndex == 2){
			numberOfTargets = 5;
		}else if(sceneIndex == 3){
			numberOfTargets = 7;
		}

		targetFloors = new int[numberOfTargets];
		isTargetFloorPressed = new bool[numberOfTargets];
		timeLeft = 30.0f;
		isTimeUp = false;
		isAllAnswerCorrect = false;
		amIWinTheGame = false;
		gameOver = false;

		for (int i = 0; i < numberOfTargets; i++) {
			// 1 and 2 are quite hard to press, so disable them intentionally.
			// 6 and 49 don't work for no reason.
			randomNumber = Random.Range (6,51);

//			if(randomNumber == 6 || randomNumber == 34 || randomNumber == 23 ||randomNumber == 49){
//				randomNumber = Random.Range (1,51);	
//			}
			targetFloors [i] = randomNumber;
		}
	}
	
	void Update () {
		_isTimeStart = GameObject.Find ("SpeechBubbles").GetComponent<SpeechBubbles> ().isTimeStart;

		// Check if all answer is correct ([i] == true).
		if (isTargetFloorPressed.All (n => n == true)) {
			isAllAnswerCorrect = true;
		}

		// Check if win the game.
		if (isTimeUp != true && isAllAnswerCorrect == true) {
			amIWinTheGame = true;

		} else if (isTimeUp == true && isAllAnswerCorrect != true) {
			amIWinTheGame = false;
			gameOver = true;

		} else {
			if (_isTimeStart == true) {
				CountdownTimer ();
			}
		}

		if (gameOver == true) {
			StartCoroutine (BackToIntro ());
		}
	}

	// Countdown Timer.
	void CountdownTimer () {
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0) {
			isTimeUp = true;
		} else {
			isTimeUp = false;
		}
	}

	IEnumerator BackToIntro () {
		yield return new WaitForSeconds (7);
		SceneManager.LoadScene (0);
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFading : MonoBehaviour {

	Image _FadeBlack;
	Color imageColor;
	float fadeSpeed;
	float fadeTime;
	float alpha = 0;
	public bool valueGet = false;

	public void FadeSetup () {
		_FadeBlack = GameObject.Find ("FadeInOut").GetComponent<Image> ();
		imageColor = _FadeBlack.color;
		fadeSpeed = 0.9f;
		alpha = 0;
	}

	public void FadeStart () {
		

		fadeTime += Time.deltaTime;

		if (alpha < 1) {
			alpha += (float)fadeSpeed * Time.deltaTime;
			imageColor.a = alpha;
			_FadeBlack.color = imageColor;
			valueGet = false;
		}

		if (alpha > 1) {
			if (valueGet == false) {
				valueGet = true;
			}
		}
		Debug.Log (alpha);
	}
}

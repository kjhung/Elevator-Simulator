using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFading : MonoBehaviour {

	Image _FadeBlack;
	Color imageColor;
	float fadeSpeed;
	float fadeTime;
	float alpha;
	public bool valueGet = false;

	public void FadeSetup () {
		_FadeBlack = GameObject.Find ("FadeInOut").GetComponent<Image> ();
		imageColor = _FadeBlack.color;
		fadeSpeed = 0.8f;
	}

	public void FadeStart () {
		fadeTime += Time.deltaTime;

		if (alpha < 1) {
			alpha += (float)fadeSpeed * Time.deltaTime;
			imageColor.a = alpha;
			_FadeBlack.color = imageColor;
		}

		if (alpha > 1) {
			if (valueGet == false) {
				valueGet = true;
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFadeInOut : MonoBehaviour
{
	public float fadeSpeed = 1.5f;

	private bool sceneStarting = true;
	private Image blackScreen;

	void Awake()
	{
		blackScreen = GetComponent<Image> ();
	}

	void Update()
	{
		if (sceneStarting)
		{
			StartScene();
		}
	}

	void FadeToClear()
	{
		blackScreen.color = Color.Lerp (blackScreen.color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	void FadeToBlack()
	{
		blackScreen.color = Color.Lerp (blackScreen.color, Color.black, fadeSpeed * Time.deltaTime);
	}

	void StartScene()
	{
		FadeToClear ();

		if (blackScreen.color.a <= 0.05f)
		{
			blackScreen.color = Color.clear;
			blackScreen.enabled = false;
			sceneStarting = false;
		}
	}

	public void EndScene()
	{
		blackScreen.enabled = true;
		FadeToBlack ();
		if (blackScreen.color.a >= 0.95f)
		{
			Application.LoadLevel(1);
		}
	}
}

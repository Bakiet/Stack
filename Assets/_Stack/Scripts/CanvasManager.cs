using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

#if UNITY_5_3
using UnityEngine.SceneManagement;
#endif

/// <summary>
/// Attached to the "Canvas" Game Object in the hierarchy. In charge to all the logic of the UI
/// </summary>
public class CanvasManager : AppAdvisoryHelper 
{
	public Image imageTransition;

	public CanvasGroup uiMenu;

	public Text STACK;
	public Text scoreText;
	public Text bestText;


	void Awake()
	{
		Application.targetFrameRate = 60;
		GC.Collect();
		Resources.UnloadUnusedAssets();

		imageTransition.gameObject.SetActive(true);

		imageTransition.color = new Color(0,0,0,1);

		imageTransition.DOFade(0,0.5f).OnComplete(() => {
			imageTransition.gameObject.SetActive(false);
		}).SetDelay(0.1f);

		uiMenu.alpha = 1;
		uiMenu.gameObject.SetActive(true);

		scoreText.gameObject.SetActive(false);
		bestText.gameObject.SetActive(false);
		bestText.text = "Best: " + PlayerPrefs.GetInt("BEST_SCORE",0).ToString();
		bestText.gameObject.SetActive(false);
	}

	public void SetScore(int score)
	{
		scoreText.text = score.ToString();


		int bestScore = PlayerPrefs.GetInt("BEST_SCORE",0);

		if(bestScore < score)
		{
			PlayerPrefs.SetInt("BEST_SCORE", score);
			PlayerPrefs.Save();

			bestText.text = "Best: " + score.ToString();
		}
	}

	public void OnStart()
	{

		imageTransition.gameObject.SetActive(false);

		var b = uiMenu.GetComponentsInChildren<Button>();

		foreach(var but in b)
		{
			but.interactable = false;
		}

		uiMenu.DOFade(0,0.5f).OnComplete(() => {
			imageTransition.gameObject.SetActive(false);
			STACK.gameObject.SetActive(false);

			scoreText.gameObject.SetActive(true);

			scoreText.color = new Color(1,1,1,0);

			scoreText.DOFade(1,0.5f);
		});

		gameManager.DoStart();
	}

	public void OnGameOver()
	{
		imageTransition.gameObject.SetActive(false);

		scoreText.gameObject.SetActive(true);

		bestText.gameObject.SetActive(true);
	

		uiMenu.DOFade(1,1).OnComplete(() => {
		
			var b = uiMenu.GetComponentsInChildren<Button>();

			foreach(var but in b)
			{
				but.interactable = true;
			}

			imageTransition.gameObject.SetActive(false);

		});
	}

	public void DoRestart()
	{
		imageTransition.gameObject.SetActive(true);
		imageTransition.color = new Color(0,0,0,0);

		DOVirtual.DelayedCall(0.1f,() => {
			#if UNITY_5_3
			SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name,LoadSceneMode.Single);
			#else
			Application.LoadLevel(0);
			#endif
		});
	}
}

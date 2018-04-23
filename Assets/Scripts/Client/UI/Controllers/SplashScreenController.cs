using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenController : MonoBehaviour
{
	public Text CompanySplash;

	private void Awake()
	{
		//PublisherSplash?.SetActive(false);
		CompanySplash.gameObject.SetActive(false);
	}

	public void OnVisualStateChange(UIController inController, UIController.VisualState inState, bool inValue)
	{
		if (inState == UIController.VisualState.Shown)
		{
			InitSplashScreen();
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	public void InitSplashScreen()
	{
		gameObject.SetActive(true);

		CompanySplash.gameObject.SetActive(true);

		PumpStateFunction();
	}

	public void PumpStateFunction()
	{
		CompanySplash.DOFade(0.0f, 4).OnComplete(() =>
		{
			// We should figure out how to make this work a little better ? 
			UIManager.Instance.PopUIController(UIManager.UIControllerID.Splash);
			GameManager.Instance.CurrentApplicationState = GameManager.ApplicationState.Loading;
		});
	}
}

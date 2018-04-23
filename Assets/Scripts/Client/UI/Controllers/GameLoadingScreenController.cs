using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameLoadingScreenController : MonoBehaviour
{
	public Image Background;
	public Sprite InGameImage;
	public Sprite EndGameImage;

	public void OnVisualStateChange(UIController inController, UIController.VisualState inState, bool inValu)
	{
		if (inState == UIController.VisualState.Shown)
		{
			InitLoading();
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	private void InitLoading()
	{
		gameObject.SetActive(true);

		AsyncSceneLoader levelLoader = null;
		if (GameManager.Instance.CurrentApplicationState == GameManager.ApplicationState.LoadingGame)
		{
			// setup the InGame loading screen!
			if (Background != null && InGameImage != null)
			{
				Background.sprite = InGameImage;
			}

			levelLoader = new AsyncSceneLoader("WorldMap");
		}
		else
		{
			// we just set it to a white screen 
			if (Background != null)
			{
				Background.color = Color.white;
			}

			levelLoader = new AsyncSceneLoader("Main");
		}

		gameObject.SetActive(true);

		if (levelLoader != null)
		{
			levelLoader.OnCompleteLoading += OnLevelLoaderComplete;
			AssetManager.Instance.RequestAssetLoad(levelLoader);
		}

	}

	private void OnLevelLoaderComplete(AssetLoadRequest inResult)
	{
		// TODO: I would assume here would be a good time to do the grid generation! 
		// we also will need to do any of the player level loading ? but for now we can jump the UI and move to the next phase
		// of the game until we have those values!
		switch (GameManager.Instance.CurrentApplicationState)
		{
			case GameManager.ApplicationState.MainMenu:
				break;
			case GameManager.ApplicationState.LoadingGame:
				StartCoroutine(LoadSetupWorld()); // start the initialization!
				break;
		}
	}

	private IEnumerator LoadSetupWorld()
	{
		// TODO: Do world setup shit in here!
		yield return null;

		if (GameManager.Instance.CurrentApplicationState == GameManager.ApplicationState.LoadingGame)
		{
			GameManager.Instance.CurrentApplicationState = GameManager.ApplicationState.OverworldGame;
			UIManager.Instance.PopUIController(UIManager.UIControllerID.LoadingGame);
		}
	}
}

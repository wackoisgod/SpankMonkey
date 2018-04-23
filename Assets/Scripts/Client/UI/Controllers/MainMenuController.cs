using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
	public void OnVisualStateChange(UIController inController, UIController.VisualState inState, bool inValue)
	{
		if (inState == UIController.VisualState.Shown)
		{
			InitMainMenu();
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	public void InitMainMenu()
	{
		gameObject.SetActive(true);
	}

	public void OnNewGameClick()
	{
		UIManager.Instance.PopUIController(UIManager.UIControllerID.MainMenu);
		GameManager.Instance.CurrentApplicationState = GameManager.ApplicationState.LoadingGame;
	}

	public void OnExitClick()
	{
		Application.Quit();
	}
}

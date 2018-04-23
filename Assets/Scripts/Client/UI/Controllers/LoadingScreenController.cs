using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LoadingScreenController : MonoBehaviour
{
	public void OnVisualStateChange(UIController inController, UIController.VisualState inState, bool inValue)
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

	public void InitLoading()
	{
		gameObject.SetActive(true);

		OfflinePhaseOneLoad();
	}

	private void OfflinePhaseOneLoad()
	{
		// lets load it from disk!
		string dataPath = Application.persistentDataPath + "/Data/AssetManifest.xml";
		if (File.Exists(dataPath))
		{
			//var manifestData = File.ReadAllBytes(dataPath);
			//AssetManager.Instance.LoadManifestFile(manifestData);
		}

		PhaseTwoLoad();
	}

	private void PhaseTwoLoad()
	{
		// load the data store with the downloaded data ? 
		ClientDataLoader ccDataLoad = new ClientDataLoader();
		ccDataLoad.OnDataLoadComplete += errors => { PhaseThreeLoad(); };
		ccDataLoad.PopulateDataStore();
	}

	private void PhaseThreeLoad()
	{
		PhaseFourLoad();
	}

	private void PhaseFourLoad()
	{
		// we loaded the data and no we should load the MainMenu scene ? 
		AsyncSceneLoader loadMainMenu = new AsyncSceneLoader("Main");
		loadMainMenu.OnCompleteLoading += asset =>
		{
			// we now have loaded everything we should have we can now move to the next
			// phase of the game!
			FinishedLoading(0);
		};
		AssetManager.Instance.RequestAssetLoad(loadMainMenu);
	}

	private void FinishedLoading(int inErrors)
	{
		Debug.Log("FinishedLoading");

		if (inErrors != 0)
		{
			Debug.Log("We have thrown a warning while loading " + inErrors);
		}

		UIManager.Instance.PopUIController(UIManager.UIControllerID.Loading);
		GameManager.Instance.CurrentApplicationState = GameManager.ApplicationState.MainMenu;
	}
}

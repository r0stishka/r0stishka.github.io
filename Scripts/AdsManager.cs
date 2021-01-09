using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
	private string GooglePlay_ID = "3548716";

	private bool TestMode = true;

	private string myPlacementId = "rewardedVideo";

	private void Start()
	{
		Advertisement.AddListener(this);
		Advertisement.Initialize(GooglePlay_ID, TestMode);
	}

	public void DisplayVideoAD()
	{
		Advertisement.Show(myPlacementId);
	}

	public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
	{
		switch (showResult)
		{
		case ShowResult.Finished:
			Debug.LogWarning("You Get A Reward");
			GameplayController.instance.WatchVideoAD();
			break;
		case ShowResult.Skipped:
			Debug.LogWarning("You DONT Get A Reward");
			break;
		case ShowResult.Failed:
			Debug.LogWarning("The ad did not finish due to an error.");
			break;
		}
	}

	public void OnUnityAdsReady(string placementId)
	{
		bool flag = placementId == myPlacementId;
	}

	public void OnUnityAdsDidError(string message)
	{
	}

	public void OnUnityAdsDidStart(string placementId)
	{
	}
}

using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class Admob : MonoBehaviour
{
	public GameObject loadingPanel;

	private RewardBasedVideoAd adReward;

	private string idApp;

	private string idReward;

	private void Start()
	{
		idApp = "ca-app-pub-8585862044525763~2540494430";
		idReward = "ca-app-pub-3940256099942544/5224354917";
		adReward = RewardBasedVideoAd.Instance;
		MobileAds.Initialize(idApp);
	}

	public void RequestRewardAd()
	{
		AdRequest request = AdRequestBuild();
		adReward.LoadAd(request, idReward);
		adReward.OnAdLoaded += HandleOnRewardedAdLoaded;
		adReward.OnAdRewarded += HandleOnAdRewarded;
		adReward.OnAdClosed += HandleOnRewardedAdClosed;
	}

	public void ShowRewardAd()
	{
		if (adReward.IsLoaded())
		{
			adReward.Show();
		}
	}

	public void HandleOnRewardedAdLoaded(object sender, EventArgs args)
	{
		ShowRewardAd();
	}

	public void HandleOnAdRewarded(object sender, EventArgs args)
	{
		loadingPanel.SetActive(value: false);
		GameController.instance.coins += 50;
	}

	public void HandleOnRewardedAdClosed(object sender, EventArgs args)
	{
		adReward.OnAdLoaded -= HandleOnRewardedAdLoaded;
		adReward.OnAdRewarded -= HandleOnAdRewarded;
		adReward.OnAdClosed -= HandleOnRewardedAdClosed;
	}

	public void OnGetMorePointsClicked()
	{
		RequestRewardAd();
	}

	private AdRequest AdRequestBuild()
	{
		return new AdRequest.Builder().Build();
	}

	private void OnDestroy()
	{
		adReward.OnAdLoaded -= HandleOnRewardedAdLoaded;
		adReward.OnAdRewarded -= HandleOnAdRewarded;
		adReward.OnAdClosed -= HandleOnRewardedAdClosed;
	}
}

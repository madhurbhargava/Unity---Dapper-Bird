using UnityEngine;
using System.Collections;

public class AdsController : MonoBehaviour {

	public static AdsController instance;
	private const string SDK_KEY = "PiNNTJhekCU6u218wnxF35GXHNEJ4O3U5RsZlXCSlhZclbmfMgvQnAy3XAMcTQ6esyCaUP2D5crqCaTTGN-2Xw";

	void Awake()
	{
		MakeSingleton ();
	}

	// Use this for initialization
	void Start () {
		AppLovin.SetSdkKey (SDK_KEY);
		AppLovin.InitializeSdk ();
		AppLovin.SetUnityAdListener (this.gameObject.name);
		StartCoroutine (CallAds());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void MakeSingleton()
	{
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(instance);
		}
	}

	public void LoadInterstitial()
	{
		Debug.Log("XXXXX Calling AppLovin Preload Interstitial");
		AppLovin.PreloadInterstitial ();
	}

	public void ShowInterstitial()
	{
		Debug.Log("XXXXX Inside Show Interstitial");
		if (AppLovin.HasPreloadedInterstitial ()) {
			Debug.Log("XXXXX Calling AppLoving Show Interstitial");
			AppLovin.ShowInterstitial ();
		} else {
			Debug.Log("XXXXX Calling Load Interstitial");
			LoadInterstitial();
		}
	}

	public void LoadVideo()
	{
		AppLovin.LoadRewardedInterstitial ();
	}

	public void ShowVideo()
	{
		AppLovin.ShowRewardedInterstitial ();
	}

	void onAppLovinEventReceived(string ev){
		if(ev.Contains("DISPLAYEDINTER")) {
			// An ad was shown.  Pause the game.
		}
		else if(ev.Contains("HIDDENINTER")) {
			// Ad ad was closed.  Resume the game.
			// If you're using PreloadInterstitial/HasPreloadedInterstitial, make a preload call here.
			AppLovin.PreloadInterstitial();
		}
		else if(ev.Contains("LOADEDINTER")) {
			// An interstitial ad was successfully loaded.
		}
		else if(string.Equals(ev, "LOADINTERFAILED")) {
			// An interstitial ad failed to load.
			LoadInterstitial();
		}
		else if(ev.Contains("REWARDAPPROVEDINFO")){
			
			// The format would be "REWARDAPPROVEDINFO|AMOUNT|CURRENCY" so "REWARDAPPROVEDINFO|10|Coins" for example
			/*string delimeter = "|";
			
			// Split the string based on the delimeter
			string[] split = ev.Split(delimeter);
			
			// Pull out the currency amount
			double amount = double.Parse(split[1]);
			
			// Pull out the currency name
			string currencyName = split[2];
			
			// Do something with the values from above.  For example, grant the coins to the user.
			updateBalance(amount, currencyName);*/
		}
		else if(ev.Contains("LOADEDREWARDED")) {
			// A rewarded video was successfully loaded.
		}
		else if(ev.Contains("LOADREWARDEDFAILED")) {
			// A rewarded video failed to load.
			LoadVideo();
		}
		else if(ev.Contains("HIDDENREWARDED")) {
			// A rewarded video was closed.  Preload the next rewarded video.
			//AppLovin.LoadRewardedInterstitial();
			LoadVideo();
		}
	}

	IEnumerator CallAds()
	{
		yield return StartCoroutine (MyCoroutine.WaitForRealSeconds(3f));
		LoadInterstitial ();
		LoadVideo ();
		AppLovin.ShowAd (AppLovin.AD_POSITION_TOP, AppLovin.AD_POSITION_CENTER);
	}

	void OnLevelWasLoaded()
	{
		if (Application.loadedLevelName == "MainMenu") {
			int random = Random.Range(0, 10);
			if(random > 5)
			{
				ShowInterstitial();
			}
			else
			{
				ShowVideo ();
			}
		}
	}
}

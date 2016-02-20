using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class LeaderboardController : MonoBehaviour {


	public static LeaderboardController instance;
	private const string LEADERBOARD_SCORE = "CgkIob7Ax4ESEAIQAQ";

	void Awake()
	{
		MakeSingleton ();
	}

	// Use this for initialization
	void Start () {
		PlayGamesPlatform.Activate ();
	}

	void OnLevelWasLoaded()
	{
		ReportScore (GameController.instance.getHighScore ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void MakeSingleton()
	{
		if(instance != null)
		{
			Destroy (gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void ConnectOrDisconnectGooglePlayGames()
	{
		if (Social.localUser.authenticated) {
			PlayGamesPlatform.Instance.SignOut ();
		} else {
			Social.localUser.Authenticate((bool success) =>{
			
			});
		}
	}

	public void OpenLeaderboardScore()
	{
		if (Social.localUser.authenticated) {
			PlayGamesPlatform.Instance.ShowLeaderboardUI(LEADERBOARD_SCORE);
		}
	}

	void ReportScore(int score)
	{
		Social.ReportScore(score, LEADERBOARD_SCORE, (bool scuccess) =>{
		                   });
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public static MenuController instance;

	[SerializeField]
	private Animator notificationAnim;

	[SerializeField]
	private Text notificationText;

	void Awake()
	{
		MakeInstance ();
	}

	void MakeInstance()
	{
		if (instance == null) {
			instance = this;
		}
	}

	public void PlayGame()
	{
		Application.LoadLevel ("Level 1");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ConnectOnTwitter()
	{
		SocialMediaController.instance.LogInOrLogOutTwitter ();
	}

	public void ShareOnTwitter()
	{
		SocialMediaController.instance.ShareOnTwitter ();
	}



	public void ConnectOnGooglePlayGames()
	{
		LeaderboardController.instance.ConnectOrDisconnectGooglePlayGames ();
	}

	public void OpenLeaderboardsScore()
	{
		LeaderboardController.instance.OpenLeaderboardScore ();
	}

	public void NotificationMessage(string message)
	{
		StartCoroutine (AnimateNotificationPanel(message));
	}

	IEnumerator AnimateNotificationPanel(string message)
	{
		notificationAnim.Play ("Panel Animation");
		notificationText.text = message;
		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(2f));
		notificationAnim.Play ("Slide Out");
	}
}

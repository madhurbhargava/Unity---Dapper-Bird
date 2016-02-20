using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Profile;

public class SocialMediaController : MonoBehaviour {

	public static SocialMediaController instance;

	void Awake()
	{
		MakeSingleton ();
	}

	// Use this for initialization
	void Start () {
		SoomlaProfile.Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnEnable()
	{
		ProfileEvents.OnLoginFinished += onLoginFinished;
		ProfileEvents.OnLoginCancelled += onLoginCancelled;
		ProfileEvents.OnLoginFailed += onLoginFailed;
		ProfileEvents.OnLogoutFinished += onLogoutFinished;
		ProfileEvents.OnLogoutFailed += onLogoutFailed;
		ProfileEvents.OnSocialActionFinished += onSocialActionFinished;
		ProfileEvents.OnSocialActionCancelled += onSocialActionCancelled;
		ProfileEvents.OnSocialActionFailed += onSocialActionFailed;

	}

	public void OnDisable()
	{
		ProfileEvents.OnLoginFinished -= onLoginFinished;
		ProfileEvents.OnLoginCancelled -= onLoginCancelled;
		ProfileEvents.OnLoginFailed -= onLoginFailed;
		ProfileEvents.OnLogoutFinished -= onLogoutFinished;
		ProfileEvents.OnLogoutFailed -= onLogoutFailed;
		ProfileEvents.OnSocialActionFinished -= onSocialActionFinished;
		ProfileEvents.OnSocialActionCancelled -= onSocialActionCancelled;
		ProfileEvents.OnSocialActionFailed -= onSocialActionFailed;
	}

	void MakeSingleton()
	{
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void LogInOrLogOutTwitter()
	{
		if (SoomlaProfile.IsLoggedIn (Provider.TWITTER)) {
			SoomlaProfile.Logout (Provider.TWITTER);
		} else {
			SoomlaProfile.Login(Provider.TWITTER);
		}
	}

	public void ShareOnTwitter()
	{
		if (SoomlaProfile.IsLoggedIn (Provider.TWITTER)) {
			SoomlaProfile.UpdateStory (
				Provider.TWITTER,
				"I am playing this awesome game " + Random.Range (0, 100),
				null,
				null,
				null,
				null,
				"www.link.com",
				null,
				null);
		} else {

			if (Application.loadedLevelName == "MainMenu") {
				MenuController.instance.NotificationMessage("Please Connect Twitter");
			}
		}

	}

	public void onLoginFinished(UserProfile userProfileJson, bool autoLogin, string payload) {
		// userProfileJson is the user's profile from the logged in provider
		// autoLogin will be "true" if the user was logged in using the AutoLogin functionality
		// payload is an identification string that you can give when you initiate the login operation and want to receive back upon its completion
		
		// ... your game specific implementation here ...
		if (Application.loadedLevelName == "MainMenu") {
			MenuController.instance.NotificationMessage("Connected");
		}
	}

	public void onLoginCancelled(Provider provider, bool autoLogin, string payload) {
		// provider is the social provider
		// autoLogin will be "true" if the user was logged in using the AutoLogin functionality
		// payload is an identification string that you can give when you initiate the login operation and want to receive back upon cancellation
		
		// ... your game specific implementation here ...
		if (Application.loadedLevelName == "MainMenu") {
			MenuController.instance.NotificationMessage("Login Cancelled");
		}
	}

	public void onLoginFailed(Provider provider, string message, bool autoLogin, string payload) {
		// provider is the social provider
		// message is the failure message
		// autoLogin will be "true" if the user was logged in using the AutoLogin functionality
		// payload is an identification string that you can give when you initiate the login operation and want to receive back upon failure
		
		// ... your game specific implementation here ...
		if (Application.loadedLevelName == "MainMenu") {
			MenuController.instance.NotificationMessage("Login Failed:"+message);
		}
	}

	public void onLogoutFinished(Provider provider) {
		// provider is the social provider
		
		// ... your game specific implementation here ...
		if (Application.loadedLevelName == "MainMenu") {
			MenuController.instance.NotificationMessage("Disconnected");
		}

	}

	public void onLogoutFailed(Provider provider, string message) {
		// provider is the social provider
		// message is the failure message
		
		// ... your game specific implementation here ...
		if (Application.loadedLevelName == "MainMenu") {
			MenuController.instance.NotificationMessage("Could not Disconnect");
		}
	}

	public void onSocialActionFinished(Provider provider, SocialActionType action, string payload) {
		// provider is the social provider
		// action is the social action (like, post status, etc..) that finished
		// payload is an identification string that you can give when you initiate the social action operation and want to receive back upon its completion
		
		// ... your game specific implementation here ...
		if (provider == Provider.TWITTER) {
			if(action == SocialActionType.UPDATE_STORY)
			{
				if(action == SocialActionType.UPDATE_STORY)
				{
					if (Application.loadedLevelName == "MainMenu") {
						MenuController.instance.NotificationMessage("Thank you for Sharing");
					}
				}
			}
		}
	}

	public void onSocialActionCancelled(Provider provider, SocialActionType action, string payload) {
		// provider is the social provider
		// action is the social action (like, post status, etc..) that has been cancelled
		// payload is an identification string that you can give when you initiate the social action operation and want to receive back upon cancellation
		
		// ... your game specific implementation here ...
		if (provider == Provider.TWITTER) {
			if(action == SocialActionType.UPDATE_STORY)
			{
				if(action == SocialActionType.UPDATE_STORY)
				{
					if (Application.loadedLevelName == "MainMenu") {
						MenuController.instance.NotificationMessage("Could Not Post");
					}
				}
			}
		}
	}

	public void onSocialActionFailed(Provider provider, SocialActionType action, string message, string payload) {
		// provider is the social provider
		// action is the social action (like, post status, etc..) that failed
		// message is the failure message
		// payload is an identification string that you can give when you initiate the social action operation and want to receive back upon failure
		
		// ... your game specific implementation here ...
		if (provider == Provider.TWITTER) {
			if(action == SocialActionType.UPDATE_STORY)
			{
				if(action == SocialActionType.UPDATE_STORY)
				{
					if (Application.loadedLevelName == "MainMenu") {
						MenuController.instance.NotificationMessage("Could Not Post");
					}
				}
			}
		}
	}
}

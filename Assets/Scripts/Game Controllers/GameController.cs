using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController instance;
	private const string HIGH_SCORE = "High Score";

	void Awake()
	{
		MakeSingleton ();
		IsGameStartedForFirstTime ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void MakeSingleton()
	{
		if (instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	void IsGameStartedForFirstTime()
	{
		if(!PlayerPrefs.HasKey("IsGameStartedForFirstTime"))
		{
			PlayerPrefs.SetInt(HIGH_SCORE, 0);
			PlayerPrefs.SetInt("IsGameStartedForFirstTime", 0);
		}

	}

	public void setHighScore(int score)
	{
		PlayerPrefs.SetInt(HIGH_SCORE, score);
	}

	public int getHighScore()
	{
		return PlayerPrefs.GetInt (HIGH_SCORE);
	}
}

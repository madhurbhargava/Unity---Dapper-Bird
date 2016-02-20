using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

	public static GameplayController instance;


	[SerializeField]
	private Text screenScoreText;

	[SerializeField]
	private Text scoreText, endScore, bestScore, gameOverText;

	[SerializeField]
	private Button restartGameButton, instructionButton;

	[SerializeField]
	private GameObject pausePanel;

	[SerializeField]
	private GameObject bird;

	[SerializeField]
	private Sprite medal;

	[SerializeField]
	private Image medalImage;


	void Awake()
	{
		MakeInstance ();
		Time.timeScale = 0f;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void MakeInstance()
	{
		if (instance == null) {
			instance = this;
		}
	}

	public void PauseGame()
	{
		if (BirdScript.instance != null) {
			if(BirdScript.instance.isAlive)
			{
				pausePanel.SetActive(true);
				gameOverText.gameObject.SetActive(false);
				endScore.text = "" + BirdScript.instance.score;
				//bestScore.text = "" + GameController.instance.getHighScore();
				Time.timeScale = 0;
				restartGameButton.onClick.RemoveAllListeners();
				restartGameButton.onClick.AddListener(() => ResumeGame());

			}
		}
	}

	public void GoToMenuButton()
	{
		Application.LoadLevel ("MainMenu");
		//SceneFader.instance
	}

	public void ResumeGame()
	{
		pausePanel.SetActive(false);
		Time.timeScale = 1f;
	}

	public void RestartGame()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void PlayGame()
	{
		scoreText.gameObject.SetActive (false);
		bird.SetActive (true);
		instructionButton.gameObject.SetActive (false);
		Time.timeScale = 1f;
	}

	public void SetScore(int score)
	{
		Debug.Log ("XXXXXXXSet Score geting called!");
		scoreText.text = "" + score;
		screenScoreText.text = "" + score;
	}

	public void PlayerDiedShowScore(int score)
	{
		pausePanel.SetActive (true);
		gameOverText.gameObject.SetActive (true);
		scoreText.gameObject.SetActive (false);

		endScore.text = "" + score;

		if (score > GameController.instance.getHighScore ()) {
			GameController.instance.setHighScore(score);
		}

		bestScore.text = "" + GameController.instance.getHighScore();
		restartGameButton.onClick.RemoveAllListeners ();
		restartGameButton.onClick.AddListener(() => RestartGame());
		//restartGameButton.onClick.AddListener(() = > RestartGame());
	}


}

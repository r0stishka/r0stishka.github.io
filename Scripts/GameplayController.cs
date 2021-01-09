using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
	public static GameplayController instance;

	public GameObject panelBG;

	public GameObject levelFinishedPanel;

	public GameObject playerDiedPanel;

	public GameObject pausePanel;

	private Vector3 coordinates;

	[SerializeField]
	private GameObject[] players;

	public float levelTime;

	public Text liveText;

	public Text scoreText;

	public Text levelTimerText;

	public Text showScoreAtTheEndOfLevelText;

	public Text countDownAndBeginLevelText;

	public Text watchVideoText;

	private float countDownBeforeLevelBegins = 3f;

	public static int smallBallsCount;

	public int playerLives;

	public int playerScore;

	public int coins;

	private bool isGamePaused;

	private bool hasLevelBegan;

	private bool countdownLevel;

	public bool levelInProgress;

	[SerializeField]
	private GameObject[] endOfLevelRewards;

	[SerializeField]
	private Button pauseBtn;

	public GameObject[] lives;

	private bool died;
    private float seconds;
    private float minutes;

    private void Awake()
	{
		CreateInstance();
		InitializeBricksAndPlayer();
	}

	private void Start()
	{
		InitializeGameplayController();
		countdownLevel = true;
	}

	private void Update()
	{
		UpdateGameplayController();
	}

	private void CreateInstance()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void InitializeBricksAndPlayer()
	{
		coordinates = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
		Object.Instantiate(players[GameController.instance.selectedPlayer]);
	}

	private void InitializeGameplayController()
	{
		playerScore = 0;
		playerLives = 3;
		GameController.instance.currentScore = playerScore;
		GameController.instance.currentLives = playerLives;
	//	levelTimerText.text = levelTime.ToString("0f");
		scoreText.text = "SCORE: " + playerScore;
		liveText.text = string.Concat(playerLives);
	}

	public void AddTime(float sec)
	{
		seconds += sec;
		if (seconds >= 60f)
		{
			minutes += Mathf.Round(seconds / 60f);
			seconds %= 60f;
		}
		else if (seconds <= 0f)
		{
			minutes = 0f;
			seconds = 0f;
		}
	}
	private void UpdateGameplayController()
	{
		scoreText.text = "SCORE: " + playerScore;
	}

	public void setHasLevelBegan(bool hasLevelBegan)
	{
		this.hasLevelBegan = hasLevelBegan;
	}

	private void LevelTime()
	{
		if (Time.timeScale != 1f)
		{
			return;
		}
		levelTime -= Time.deltaTime;
		levelTimerText.text = levelTime.ToString("F0");
		if (levelTime <= 0f)
		{
			playerLives--;
			GameController.instance.currentLives = playerLives;
			GameController.instance.currentScore = playerScore;
			if (playerLives == 0)
			{
				StartCoroutine(PromptTheUserToWatchAVideo());
			}
		}
	}

	public void PlayerDied()
	{
		countdownLevel = false;
		pauseBtn.interactable = false;
		levelInProgress = false;
		smallBallsCount = 0;
		if (Player.instance.isShield)
		{
			Player.instance.isShield = false;
			Player.instance.shieldpoweUP.SetActive(value: false);
			return;
		}
		playerLives--;
		lives[playerLives].SetActive(value: false);
		GameController.instance.currentLives = playerLives;
		GameController.instance.currentScore = playerScore;
		if (playerLives < 1 && !died)
		{
			StartCoroutine(PromptTheUserToWatchAVideo());
		}
		else if (playerLives < 1 && died)
		{
			levelFinishedPanel.SetActive(value: true);
			Spawner.Instance.StopSpawning();
		}
	}

	private IEnumerator PromptTheUserToWatchAVideo()
	{
		levelInProgress = false;
		countdownLevel = false;
		pauseBtn.interactable = false;
		Time.timeScale = 0f;
		Spawner.Instance.StopSpawning();
		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(0.7f));
		playerDiedPanel.SetActive(value: true);
	}

	private IEnumerator LevelCompleted()
	{
		countdownLevel = false;
		pauseBtn.interactable = false;
		int currentLevel = GameController.instance.currentLevel;
		currentLevel++;
		if (currentLevel < GameController.instance.levels.Length)
		{
			GameController.instance.levels[currentLevel] = true;
		}
		Object.Instantiate(endOfLevelRewards[GameController.instance.currentLevel], new Vector3(0f, Camera.main.orthographicSize, 0f), Quaternion.identity);
		if (GameController.instance.doubleCoins)
		{
			coins *= 2;
		}
		GameController.instance.coins = coins;
		GameController.instance.Save();
		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(4f));
		levelInProgress = true;
		Player.instance.StopMoving();
		Time.timeScale = 0f;
		levelFinishedPanel.SetActive(value: true);
		showScoreAtTheEndOfLevelText.text = string.Concat(playerScore);
	}

	public void CountSmallBalls()
	{
		smallBallsCount--;
		if (smallBallsCount == 0)
		{
			StartCoroutine(LevelCompleted());
		}
	}

	public void GoToMapButton()
	{
		GameController.instance.currentScore = playerScore;
		if (GameController.instance.highScore < GameController.instance.currentScore)
		{
			GameController.instance.highScore = GameController.instance.currentScore;
			GameController.instance.Save();
		}
		if (Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
		}
		SceneManager.LoadScene(0);
	}

	public void RestartCurrentLevelButton()
	{
		smallBallsCount = 0;
		coins = 0;
		GameController.instance.currentLives = playerLives;
		GameController.instance.currentScore = playerScore;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Time.timeScale = 1f;
	}

	public void NextLevel()
	{
		GameController.instance.currentScore = playerScore;
		GameController.instance.currentLives = playerLives;
		if (GameController.instance.highScore < GameController.instance.currentScore)
		{
			GameController.instance.highScore = GameController.instance.currentScore;
			GameController.instance.Save();
		}
		int currentLevel = GameController.instance.currentLevel;
		currentLevel++;
		if (currentLevel < GameController.instance.levels.Length)
		{
			GameController.instance.currentLevel = currentLevel;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			if (LoadingScreen.instance != null)
			{
				LoadingScreen.instance.PlayLoadingScreen();
			}
		}
	}

	public void PauseGame()
	{
		pausePanel.SetActive(value: true);
		countdownLevel = false;
		levelInProgress = false;
		isGamePaused = true;
		Time.timeScale = 0f;
	}

	public void ResumeGame()
	{
		countdownLevel = true;
		levelInProgress = true;
		isGamePaused = false;
		pausePanel.SetActive(value: false);
		Time.timeScale = 1f;
	}

	public void WatchVideoAD()
	{
		StartCoroutine(GivePlayerLivesRewardAfterWatchingVideo());
	}

	private IEnumerator GivePlayerLivesRewardAfterWatchingVideo()
	{
		Debug.Log("Showing ADS");
		playerDiedPanel.SetActive(value: false);
		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(2f));
		playerLives = 3;
		smallBallsCount = 0;
		died = true;
		GameController.instance.currentLives = playerLives;
		for (int i = 0; i < lives.Length; i++)
		{
			lives[i].SetActive(value: true);
		}
		Time.timeScale = 1f;
		Spawner.Instance.StartSpawning();
	}

	public void DontWatchVideoInsteadQuit()
	{
		GameController.instance.currentScore = playerScore;
		if (GameController.instance.highScore < GameController.instance.currentScore)
		{
			GameController.instance.highScore = GameController.instance.currentScore;
			GameController.instance.Save();
		}
		Time.timeScale = 1f;
		SceneManager.LoadScene(0);
		if (LoadingScreen.instance != null)
		{
			LoadingScreen.instance.PlayLoadingScreen();
		}
	}
}

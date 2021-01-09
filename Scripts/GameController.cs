using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController instance;

	private GameData data;

	public int currentLevel = 0;

	public int currentScore;

	public int currentLives;

	public bool isGameStartedFromLevelMenu;

	public bool isGameStartedFirstTime;

	public bool isMusicOn;

	public bool doubleCoins;

	public int selectedPlayer;

	public int selectedWeapon;

	public int coins;

	public int highScore;

	public bool[] players;

	public bool[] levels;

	public bool[] weapons;

	public bool[] achievements;

	public bool[] collectedItems;

	private void Awake()
	{
		MakeSingleton();
		InitializeGameVariables();
	}

	private void Start()
	{
	}

	private void MakeSingleton()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		instance = this;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void InitializeGameVariables()
	{
		Load();
		if (data != null)
		{
			isGameStartedFirstTime = data.getIsGameStartedFirstTime();
		}
		else
		{
			isGameStartedFirstTime = true;
		}
		if (isGameStartedFirstTime)
		{
			highScore = 0;
			coins = 0;
			selectedPlayer = 0;
			selectedWeapon = 0;
			isGameStartedFirstTime = false;
			isMusicOn = false;
			players = new bool[4];
			levels = new bool[40];
			weapons = new bool[4];
			achievements = new bool[8];
			collectedItems = new bool[40];
			players[0] = true;
			for (int i = 1; i < players.Length; i++)
			{
				players[i] = false;
			}
			levels[0] = true;
			for (int j = 1; j < levels.Length; j++)
			{
				levels[j] = false;
			}
			weapons[0] = true;
			for (int k = 1; k < weapons.Length; k++)
			{
				weapons[k] = false;
			}
			for (int l = 0; l < achievements.Length; l++)
			{
				achievements[l] = false;
			}
			for (int m = 0; m < collectedItems.Length; m++)
			{
				collectedItems[m] = false;
			}
			data = new GameData();
			data.setHighScore(highScore);
			data.setCoins(coins);
			data.setSelectedPlayer(selectedPlayer);
			data.setSelectedWeapon(selectedWeapon);
			data.setIsGameStartedFirstTime(isGameStartedFirstTime);
			data.setIsMusicOn(isMusicOn);
			data.setPlayers(players);
			data.setLevels(levels);
			data.setWeapons(weapons);
			data.setAchievements(achievements);
			data.setCollectedItems(collectedItems);
			Save();
			Load();
		}
		else
		{
			highScore = data.getHighScore();
			coins = data.getCoins();
			selectedPlayer = data.getSelectedPlayer();
			selectedWeapon = data.getSelectedWeapon();
			isGameStartedFirstTime = data.getIsGameStartedFirstTime();
			isMusicOn = data.getIsMusicOn();
			players = data.getPlayers();
			levels = data.getLevels();
			weapons = data.getWeapons();
			achievements = data.getAchievements();
			collectedItems = data.getCollectedItems();
		}
	}

	public void Save()
	{
		FileStream fileStream = null;
		try
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			fileStream = File.Create(Application.persistentDataPath + "/GameData.dat");
			if (data != null)
			{
				data.setHighScore(highScore);
				data.setCoins(coins);
				data.setIsGameStartedFirstTime(isGameStartedFirstTime);
				data.setPlayers(players);
				data.setLevels(levels);
				data.setWeapons(weapons);
				data.setSelectedPlayer(selectedPlayer);
				data.setSelectedWeapon(selectedWeapon);
				data.setIsMusicOn(isMusicOn);
				data.setAchievements(achievements);
				data.setCollectedItems(collectedItems);
				binaryFormatter.Serialize(fileStream, data);
			}
		}
		catch (Exception)
		{
		}
		finally
		{
			fileStream?.Close();
		}
	}

	public void Load()
	{
		FileStream fileStream = null;
		try
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			fileStream = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);
			data = (GameData)binaryFormatter.Deserialize(fileStream);
		}
		catch (Exception)
		{
		}
		finally
		{
			fileStream?.Close();
		}
	}
}

using System;

[Serializable]
internal class GameData
{
	private bool isGameStartedFirstTime;

	private bool isMusicOn;

	private bool doubleCoins;

	private int selectedPlayer;

	private int selectedWeapon;

	private int coins;

	private int highScore;

	private bool[] players;

	private bool[] levels;

	private bool[] weapons;

	private bool[] achievements;

	private bool[] collectedItems;

	public void setDoubleCoins(bool doubleCoins)
	{
		this.doubleCoins = doubleCoins;
	}

	public bool getDoubleCoins()
	{
		return doubleCoins;
	}

	public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
	{
		this.isGameStartedFirstTime = isGameStartedFirstTime;
	}

	public bool getIsGameStartedFirstTime()
	{
		return isGameStartedFirstTime;
	}

	public void setIsMusicOn(bool isMusicOn)
	{
		this.isMusicOn = isMusicOn;
	}

	public bool getIsMusicOn()
	{
		return isMusicOn;
	}

	public void setSelectedPlayer(int selectedPlayer)
	{
		this.selectedPlayer = selectedPlayer;
	}

	public int getSelectedPlayer()
	{
		return selectedPlayer;
	}

	public void setSelectedWeapon(int selectedWeapon)
	{
		this.selectedWeapon = selectedWeapon;
	}

	public int getSelectedWeapon()
	{
		return selectedWeapon;
	}

	public void setCoins(int coins)
	{
		this.coins = coins;
	}

	public int getCoins()
	{
		return coins;
	}

	public void setHighScore(int highScore)
	{
		this.highScore = highScore;
	}

	public int getHighScore()
	{
		return highScore;
	}

	public void setPlayers(bool[] players)
	{
		this.players = players;
	}

	public bool[] getPlayers()
	{
		return players;
	}

	public void setLevels(bool[] levels)
	{
		this.levels = levels;
	}

	public bool[] getLevels()
	{
		return levels;
	}

	public void setWeapons(bool[] weapons)
	{
		this.weapons = weapons;
	}

	public bool[] getWeapons()
	{
		return weapons;
	}

	public void setAchievements(bool[] achievements)
	{
		this.achievements = achievements;
	}

	public bool[] getAchievements()
	{
		return achievements;
	}

	public void setCollectedItems(bool[] collectedItems)
	{
		this.collectedItems = collectedItems;
	}

	public bool[] getCollectedItems()
	{
		return collectedItems;
	}
}

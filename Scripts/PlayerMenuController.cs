using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuController : MonoBehaviour
{
	public Text scoreText;

	public Text coinText;

	public bool[] players;

	public bool[] weapons;

	public Image[] priceTags;

	public Image[] weaponIcons;

	public Sprite[] weaponArrows;

	public int selectedWeapon;

	public int selectedPlayer;

	public GameObject buyPlayerPanel;

	public Button yesBtn;

	public Text buyPlayerText;

	public GameObject coinShop;

	public AudioSource buySound;

	private void Start()
	{
		InitializePlayerMenuController();
	}

	private void InitializePlayerMenuController()
	{
		scoreText.text = string.Concat(GameController.instance.highScore);
		coinText.text = string.Concat(GameController.instance.coins);
		players = GameController.instance.players;
		weapons = GameController.instance.weapons;
		selectedPlayer = GameController.instance.selectedPlayer;
		selectedWeapon = GameController.instance.selectedWeapon;
		for (int i = 0; i < weaponIcons.Length; i++)
		{
			weaponIcons[i].gameObject.SetActive(value: false);
		}
		for (int j = 0; j < players.Length; j++)
		{
			if (players[j])
			{
				priceTags[j - 0].gameObject.SetActive(value: false);
			}
		}
		weaponIcons[selectedPlayer].gameObject.SetActive(value: true);
		weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
	}

	private void Update()
	{
		coinText.text = string.Concat(GameController.instance.coins);
	}

	public void FirstPlayerButton()
	{
		if (players[1])
		{
			if (selectedPlayer != 1)
			{
				selectedPlayer = 1;
				GameController.instance.selectedPlayer = selectedPlayer;
				GameController.instance.selectedWeapon = selectedWeapon;
				GameController.instance.Save();
			}
		}
		else if (GameController.instance.coins >= 1000)
		{
			buyPlayerPanel.SetActive(value: true);
			buyPlayerText.text = "Do You Want To Purchase The Player?";
			yesBtn.onClick.RemoveAllListeners();
			yesBtn.onClick.AddListener(delegate
			{
				BuyPlayer1();
			});
		}
		else
		{
			buyPlayerPanel.SetActive(value: true);
			buyPlayerText.text = "You Dont Have Enough Coins. Do You Want To Purchase Coins?";
			yesBtn.onClick.RemoveAllListeners();
			yesBtn.onClick.AddListener(delegate
			{
				OpenCoinShop();
			});
		}
	}

	public void SecondPlayerButton()
	{
		if (players[2])
		{
			if (selectedPlayer != 2)
			{
				selectedPlayer = 2;
				GameController.instance.selectedPlayer = selectedPlayer;
				GameController.instance.selectedWeapon = selectedWeapon;
				GameController.instance.Save();
			}
		}
		else if (GameController.instance.coins >= 2000)
		{
			buyPlayerPanel.SetActive(value: true);
			buyPlayerText.text = "Do You Want To Purchase The Player?";
			yesBtn.onClick.RemoveAllListeners();
			yesBtn.onClick.AddListener(delegate
			{
				BuyPlayer2();
			});
		}
		else
		{
			buyPlayerPanel.SetActive(value: true);
			buyPlayerText.text = "You Dont Have Enough Coins. Do You Want To Purchase Coins?";
			yesBtn.onClick.RemoveAllListeners();
			yesBtn.onClick.AddListener(delegate
			{
				OpenCoinShop();
			});
		}
	}

	public void ThirdPlayerButton()
	{
		if (players[3])
		{
			if (selectedPlayer != 3)
			{
				selectedPlayer = 3;
				GameController.instance.selectedPlayer = selectedPlayer;
				GameController.instance.selectedWeapon = selectedWeapon;
				GameController.instance.Save();
			}
		}
		else if (GameController.instance.coins >= 5000)
		{
			buyPlayerPanel.SetActive(value: true);
			buyPlayerText.text = "Do You Want To Purchase The Player?";
			yesBtn.onClick.RemoveAllListeners();
			yesBtn.onClick.AddListener(delegate
			{
				BuyPlayer3();
			});
		}
		else
		{
			buyPlayerPanel.SetActive(value: true);
			buyPlayerText.text = "You Dont Have Enough Coins. Do You Want To Purchase Coins?";
			yesBtn.onClick.RemoveAllListeners();
			yesBtn.onClick.AddListener(delegate
			{
				OpenCoinShop();
			});
		}
	}

	public void BuyPlayer1()
	{
		GameController.instance.players[1] = true;
		if (selectedPlayer != 1)
		{
			selectedPlayer = 1;
			GameController.instance.selectedPlayer = selectedPlayer;
			GameController.instance.Save();
		}
		GameController.instance.coins -= 1000;
		GameController.instance.Save();
		InitializePlayerMenuController();
		buySound.Play();
		if (buyPlayerPanel != null)
		{
			buyPlayerPanel.SetActive(value: false);
		}
	}

	public void BuyPlayer2()
	{
		GameController.instance.players[2] = true;
		if (selectedPlayer != 2)
		{
			selectedPlayer = 2;
			GameController.instance.selectedPlayer = selectedPlayer;
			GameController.instance.Save();
		}
		GameController.instance.coins -= 2000;
		GameController.instance.Save();
		InitializePlayerMenuController();
		buySound.Play();
		if (buyPlayerPanel != null)
		{
			buyPlayerPanel.SetActive(value: false);
		}
	}

	public void BuyPlayer3()
	{
		GameController.instance.players[3] = true;
		if (selectedPlayer != 3)
		{
			selectedPlayer = 3;
			GameController.instance.selectedPlayer = selectedPlayer;
			GameController.instance.Save();
		}
		GameController.instance.coins -= 5000;
		GameController.instance.Save();
		InitializePlayerMenuController();
		buySound.Play();
		if (buyPlayerPanel != null)
		{
			buyPlayerPanel.SetActive(value: false);
		}
	}

	public void CloseCoinShop()
	{
		coinShop.SetActive(value: false);
	}

	public void OpenCoinShop()
	{
		if (buyPlayerPanel.activeInHierarchy)
		{
			buyPlayerPanel.SetActive(value: false);
		}
		coinShop.SetActive(value: true);
	}

	public void DontBuyPlayerOrCoins()
	{
		buyPlayerPanel.SetActive(value: false);
	}
}

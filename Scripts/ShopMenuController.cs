using UnityEngine;
using UnityEngine.UI;

public class ShopMenuController : MonoBehaviour
{
	public static ShopMenuController instance;

	public Text coinText;

	public Text scoreText;

	public Text buyArrowsText;

	public Text watchVideoText;

	public Button weaponsTabBtn;

	public Button specialTabBtn;

	public Button earnCoinsTabBtn;

	public Button yesBtn;

	public GameObject weaponItemsPanel;

	public GameObject specialItemsPanel;

	public GameObject earnCoinsItemsPanel;

	public GameObject coinShopPanel;

	public GameObject buyArrowsPanel;

	public AudioSource buySound;

	private void Awake()
	{
		MakeInstance();
	}

	private void Start()
	{
		InitializeShopMenuController();
	}

	private void MakeInstance()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void Weapon1()
	{
		if (!GameController.instance.weapons[1])
		{
			if (GameController.instance.coins >= 500)
			{
				buyArrowsPanel.SetActive(value: true);
				buyArrowsText.text = "Do You Want To Purchase FireBall?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(delegate
				{
					BuyWeapon1();
				});
			}
			else
			{
				buyArrowsPanel.SetActive(value: true);
				buyArrowsText.text = "You Dont Have Enough Coins. Do You Want To Buy Coins?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(delegate
				{
					OpenCoinShop();
				});
			}
		}
		if (GameController.instance.weapons[1])
		{
			GameController.instance.selectedWeapon = 1;
		}
	}

	public void Weapon2()
	{
		if (!GameController.instance.weapons[2])
		{
			if (GameController.instance.coins >= 1000)
			{
				buyArrowsPanel.SetActive(value: true);
				buyArrowsText.text = "Do You Want To Purchase IceBAll?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(delegate
				{
					BuyWeapon2();
				});
			}
			else
			{
				buyArrowsPanel.SetActive(value: true);
				buyArrowsText.text = "You Dont Have Enough Coins. Do You Want To Buy Coins?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(delegate
				{
					OpenCoinShop();
				});
			}
		}
		if (GameController.instance.weapons[2])
		{
			GameController.instance.selectedWeapon = 2;
		}
	}

	public void Weapon3()
	{
		if (!GameController.instance.weapons[3])
		{
			if (GameController.instance.coins >= 3000)
			{
				buyArrowsPanel.SetActive(value: true);
				buyArrowsText.text = "Do You Want To Purchase Double LavaBall?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(delegate
				{
					BuyWeapon3();
				});
			}
			else
			{
				buyArrowsPanel.SetActive(value: true);
				buyArrowsText.text = "You Dont Have Enough Coins. Do You Want To Buy Coins?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(delegate
				{
					OpenCoinShop();
				});
			}
		}
		if (GameController.instance.weapons[3])
		{
			GameController.instance.selectedWeapon = 3;
		}
	}

	public void BuyWeapon1()
	{
		GameController.instance.weapons[1] = true;
		GameController.instance.selectedWeapon = 1;
		GameController.instance.coins -= 500;
		GameController.instance.Save();
		buySound.Play();
		buyArrowsPanel.SetActive(value: false);
		coinText.text = string.Concat(GameController.instance.coins);
	}

	public void BuyWeapon2()
	{
		GameController.instance.weapons[2] = true;
		GameController.instance.selectedWeapon = 2;
		GameController.instance.coins -= 1000;
		GameController.instance.Save();
		buySound.Play();
		buyArrowsPanel.SetActive(value: false);
		coinText.text = string.Concat(GameController.instance.coins);
	}

	public void BuyWeapon3()
	{
		GameController.instance.weapons[3] = true;
		GameController.instance.selectedWeapon = 3;
		GameController.instance.coins -= 3000;
		GameController.instance.Save();
		buySound.Play();
		buyArrowsPanel.SetActive(value: false);
		coinText.text = string.Concat(GameController.instance.coins);
	}

	private void InitializeShopMenuController()
	{
		coinText.text = string.Concat(GameController.instance.coins);
		scoreText.text = string.Concat(GameController.instance.highScore);
	}

	public void OpenCoinShop()
	{
		if (buyArrowsPanel.activeInHierarchy)
		{
			buyArrowsPanel.SetActive(value: false);
		}
		coinShopPanel.SetActive(value: true);
	}

	public void CloseCoinShop()
	{
		coinShopPanel.SetActive(value: false);
	}

	public void OpenWeaponItemsPanel()
	{
		weaponItemsPanel.SetActive(value: true);
		specialItemsPanel.SetActive(value: false);
		earnCoinsItemsPanel.SetActive(value: false);
	}

	public void OpenSpecialItemsPanel()
	{
		specialItemsPanel.SetActive(value: true);
		weaponItemsPanel.SetActive(value: false);
		earnCoinsItemsPanel.SetActive(value: false);
	}

	public void OpenEarnCoinsItemsPanel()
	{
		earnCoinsItemsPanel.SetActive(value: true);
		specialItemsPanel.SetActive(value: false);
		weaponItemsPanel.SetActive(value: false);
	}

	public void DontBuyArrows()
	{
		buyArrowsPanel.SetActive(value: false);
	}
}

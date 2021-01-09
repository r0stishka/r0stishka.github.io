using UnityEngine;

public class Bomb : MonoBehaviour
{
	private float forceX;

	private float forceY;

	public bool moveLeft;

	public bool moveRight;

	public Rigidbody2D rg;

	public GameObject originalBomb;

	private GameObject bomb1;

	private GameObject bomb2;

	private Bomb bomb1Script;

	private Bomb bomb2Script;

	public Animator an;

	public GameObject coin;

	private ScreenShake shakeAn;

	private Explosion explosionan;

	private void Awake()
	{
		BombSpeed();
		InstantiateBombs();
	}

	private void Start()
	{
		shakeAn = GameObject.Find("Main Camera").GetComponent<ScreenShake>();
		rg = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		BombMovement();
	}

	private void InstantiateBombs()
	{
		if (base.gameObject.tag != "Small Bomb")
		{
			bomb1 = Object.Instantiate(originalBomb);
			bomb2 = Object.Instantiate(originalBomb);
			bomb1Script = bomb1.GetComponent<Bomb>();
			bomb2Script = bomb2.GetComponent<Bomb>();
			bomb1.SetActive(value: false);
			bomb2.SetActive(value: false);
		}
	}

	public void SetMoveLeft(bool moveLeft)
	{
		this.moveLeft = moveLeft;
		moveRight = !moveLeft;
	}

	public void SetMoveRight(bool moveRight)
	{
		this.moveRight = moveRight;
		moveLeft = !moveRight;
	}

	private void InitializeBombsAndTurnOff()
	{
		Vector3 position = base.transform.position;
		bomb1.transform.position = position;
		bomb1Script.SetMoveLeft(moveLeft: true);
		bomb2.transform.position = position;
		bomb2Script.SetMoveRight(moveRight: true);
		bomb1.SetActive(value: true);
		bomb2.SetActive(value: true);
		if (base.gameObject.tag != "Small Bomb")
		{
			if (base.transform.position.y > 1f && base.transform.position.y <= 1.3f)
			{
				bomb1.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 4f);
				bomb2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 4f);
			}
			else if (base.transform.position.y > 1.3f)
			{
				bomb1.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 3f);
				bomb2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 3f);
			}
			else if (base.transform.position.y < 1f)
			{
				bomb1.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 5.5f);
				bomb2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 5.5f);
			}
		}
		base.gameObject.SetActive(value: false);
	}

	private void BombMovement()
	{
		if (moveLeft)
		{
			Vector3 position = base.transform.position;
			position.x -= forceX * Time.deltaTime;
			base.transform.position = position;
		}
		if (moveRight)
		{
			Vector3 position2 = base.transform.position;
			position2.x += forceX * Time.deltaTime;
			base.transform.position = position2;
		}
	}

	private void BombSpeed()
	{
		forceX = 2.5f;
		string tag = base.gameObject.tag;
		switch (tag)
		{
		case "Largest Bomb":
			forceY = 10f;
			return;
		case "Large Bomb":
			forceY = 9f;
			return;
		case "Medium Bomb":
			forceY = 10f;
			return;
		}
		if (tag == "Small Bomb")
		{
			forceY = 10f;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Down")
		{
			rg.velocity = new Vector2(0f, forceY);
		}
		if (other.gameObject.tag == "Right Wall")
		{
			moveLeft = true;
			moveRight = false;
		}
		if (other.gameObject.tag == "Left Wall")
		{
			moveRight = true;
			moveLeft = false;
		}
		if (other.tag == "FireBall1" || other.tag == "FireBall2" || other.tag == "FireBall3" || other.tag == "FireBall4")
		{
			if (base.gameObject.tag != "Small Bomb")
			{
				InitializeBombsAndTurnOff();
			}
			else
			{
				base.gameObject.SetActive(value: false);
			}
			shakeAn.Shake();
			GameplayController.instance.playerScore += 10;
			Object.Instantiate(coin, new Vector3(0f, -3f, 0f), Quaternion.identity);
		}
		if (other.tag == "Player")
		{
			GameplayController.instance.PlayerDied();
		}
	}
}

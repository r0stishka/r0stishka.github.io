using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
	public static Player instance;

	public float speed;

	private Rigidbody2D rg;

	public GameObject[] Arrows;

	private float height;

	public float fireRate;

	private float canFire = -1f;

	private ScreenShake shakeAn;

	public GameObject explosionEffect;

	public bool isShield;

	public GameObject shieldpoweUP;

	public AudioSource fireSound;

	public AudioSource explosionSound;

	public AudioSource powerupSound;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		shakeAn = GameObject.Find("Main Camera").GetComponent<ScreenShake>();
		rg = GetComponent<Rigidbody2D>();
		float orthographicSize = Camera.main.orthographicSize;
		height = 0f - orthographicSize;
	}

	private void Update()
	{
		Shooting();
	}

	public void Shooting()
	{
		if (CrossPlatformInputManager.GetButtonDown("Fire1") && Time.time > canFire)
		{
			canFire = Time.time + fireRate;
			Object.Instantiate(Arrows[GameController.instance.selectedWeapon], new Vector3(base.transform.position.x, height, 0f), Quaternion.Euler(0f, 0f, 90f));
			shakeAn.Shake();
			fireSound.Play();
		}
	}

	public void StopMoving()
	{
		speed = 0f;
	}

	private void FixedUpdate()
	{
		Movement();
	}

	public void Movement()
	{
		float axis = CrossPlatformInputManager.GetAxis("Horizontal");
		rg.velocity = new Vector2(speed * axis, rg.velocity.y);
		base.transform.position = new Vector3(Mathf.Clamp(base.transform.position.x, -8.2f, 8.2f), base.transform.position.y, base.transform.position.z);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Largest Bomb" || other.tag == "Large Bomb" || other.tag == "Medium Bomb" || other.tag == "Small Bomb")
		{
			Object.Instantiate(explosionEffect, new Vector3(base.transform.position.x, height + 1f, 0f), Quaternion.identity);
			explosionSound.Play();
		}
	}

	public void ShieldPowerUP()
	{
		isShield = true;
		shieldpoweUP.SetActive(value: true);
		powerupSound.Play();
	}
}

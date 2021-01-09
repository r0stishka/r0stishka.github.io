using UnityEngine;

public class FireBall : MonoBehaviour
{
	public float speed;

	private bool canShoot;

	private void Start()
	{
	}

	private void Update()
	{
		ShootArrow();
	}

	private void ShootArrow()
	{
		Vector3 position = base.transform.position;
		position.y += speed * Time.unscaledDeltaTime;
		base.transform.position = position;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Largest Bomb" || other.tag == "Large Bomb" || other.tag == "Medium Bomb" || other.tag == "Small Bomb")
		{
			base.gameObject.SetActive(value: false);
		}
		if (other.tag == "Top")
		{
			base.gameObject.SetActive(value: false);
		}
	}
}

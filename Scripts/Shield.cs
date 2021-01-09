using UnityEngine;

public class Shield : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Player.instance.ShieldPowerUP();
			Object.Destroy(base.gameObject);
		}
	}
}

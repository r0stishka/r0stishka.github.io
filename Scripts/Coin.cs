using UnityEngine;

public class Coin : MonoBehaviour
{
	public AudioClip sound;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			AudioSource.PlayClipAtPoint(sound, base.transform.position);
			GameController.instance.coins++;
			Object.Destroy(base.gameObject);
		}
	}
}

using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	private static Spawner instance;

	public GameObject bomb;

	public GameObject shield;

	public bool spawn;

	public static Spawner Instance => instance;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		StartCoroutine(BombSpawner());
		StartCoroutine(ShieldSpawner());
	}

	private IEnumerator BombSpawner()
	{
		while (!spawn)
		{
			yield return new WaitForSecondsRealtime(Random.Range(15, 20));
			Object.Instantiate<GameObject>(position: new Vector3(Random.Range(-6, 6), 2f, 0f), original: bomb, rotation: Quaternion.identity);
		}
	}

	private IEnumerator ShieldSpawner()
	{
		while (!spawn)
		{
			yield return new WaitForSecondsRealtime(20f);
			Object.Instantiate<GameObject>(position: new Vector3(Random.Range(-6, 6), -3.5f, 0f), original: shield, rotation: Quaternion.identity);
		}
	}

	public void StartSpawning()
	{
		spawn = false;
	}

	public void StopSpawning()
	{
		spawn = true;
	}
}

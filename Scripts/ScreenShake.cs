using UnityEngine;

public class ScreenShake : MonoBehaviour
{
	public Animator an;

	private void Start()
	{
	}

	public void Shake()
	{
		an.SetTrigger("Shake");
	}
}

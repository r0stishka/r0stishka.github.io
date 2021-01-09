using UnityEngine;

public class Explosion : MonoBehaviour
{
	public Animator an;

	public void ExplosionAnimation()
	{
		an.SetTrigger("Explode");
	}
}

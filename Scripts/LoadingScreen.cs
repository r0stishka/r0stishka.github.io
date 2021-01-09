using System.Collections;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
	public static LoadingScreen instance;

	[SerializeField]
	private GameObject text;

	private void Awake()
	{
		MakeSingleton();
	//	Hide();
	}

	private void MakeSingleton()
	{
		if (instance != null)
		{
	//		Object.Destroy(base.gameObject);
			return;
		}
		instance = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	public void PlayLoadingScreen()
	{
		StartCoroutine(ShowLoadingScreen());
	}

	public void PlayFadeInAnimation()
	{
		StartCoroutine(FadeIn());
	}

	private IEnumerator FadeIn()
	{
		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(0.4f));
		if (GameplayController.instance != null)
		{
			GameplayController.instance.setHasLevelBegan(hasLevelBegan: true);
		}
	}

	private IEnumerator ShowLoadingScreen()
	{
		Show();
		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(0.5f));
        Hide();
        if (GameplayController.instance != null)
        {
            GameplayController.instance.setHasLevelBegan(hasLevelBegan: true);
        }
    }

    private void Show()
	{
		text.SetActive(value: true);
	}

	private void Hide()
	{
		text.SetActive(value: false);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
	public static ScreenFader instance;
	
	[SerializeField]
	private GameObject fadeCanvas;
	
	[SerializeField]
	private Animator fadeAnim;

    void Awake()
    {
        MakeSingleton();
    }
	
	void MakeSingleton()
	{
		if(instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
	
	public void FadeIn(string levelName)
	{
		StartCoroutine(FadeInAnimation(levelName));
	}
	
	public void FadeOut()
	{
		StartCoroutine(FadeOutAnimation());
	}
	
	IEnumerator FadeInAnimation(string levelName)
	{
		fadeCanvas.SetActive(true);
		fadeAnim.Play("FadeIn");
		yield return new WaitForSeconds(.7f);
		SceneManager.LoadScene(levelName);
		FadeOut();
	}
	
	IEnumerator FadeOutAnimation()
	{
		fadeAnim.Play("FadeOut");
		yield return new WaitForSeconds(1f);
		fadeCanvas.SetActive(false);
	}
}

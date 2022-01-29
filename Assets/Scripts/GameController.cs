using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController instance;
	
	void Awake()
	{
		MakeSingleton();
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

	void MakeSingleton()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else 
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
	
	public void playGame()
	{
		ScreenFader.instance.FadeIn("Optimized");
	}
}

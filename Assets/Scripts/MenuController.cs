using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
	public static MenuController instance;
    
    void Awake()
    {
        MakeInstance();
    }
	
	void MakeInstance()
	{
		if(instance == null)
		{
			instance = this;
		}
	}
	
	public void playGame()
	{
		ScreenFader.instance.FadeIn("Optimized");
	}
	
	void Update()
	{
		// Make sure user is on Android platform
		if (Application.platform == RuntimePlatform.Android) 
		{
        
			// Check if Back was pressed this frame
			if (Input.GetKeyDown(KeyCode.Escape)) 
			{
            
				// Quit the application
				Application.Quit();
			}
		}
	}
	
}
